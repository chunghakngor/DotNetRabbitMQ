WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IPublisher, Publisher>();

WebApplication app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

const string _hostName = "localhost";
const int _port = 5672;

app.MapGet("/send/{message}", (IPublisher publisher, string message) => {
	if (string.IsNullOrEmpty(message)) {
		return Results.BadRequest(new BadResponseMessage("Error", "Your message cannot be null or empty"));
	}

	ConnectionFactory factory = new() { HostName = _hostName, Port = _port };
	using IConnection connection = factory.CreateConnection();
	using IModel channel = connection.CreateModel();

	byte[] messageBody = JsonSerializer.SerializeToUtf8Bytes(message);
	publisher.SendMessage(channel, "testQueue", messageBody);

	return Results.Ok(new OkResponseMessage("Ok", "Your message has been sent to the queue!", message));
}).Produces<OkResponseMessage>(200, "application/json").Produces<BadResponseMessage>(400, "application/json");

app.MapPost("/send", (IPublisher publisher, [FromBody] RequestMessage message) => {
	if (message == null || string.IsNullOrEmpty(message.Message)) {
		return Results.BadRequest(new BadResponseMessage("Error", "Your message cannot be null or empty"));
	}

	ConnectionFactory factory = new() { HostName = _hostName, Port = _port };
	using IConnection connection = factory.CreateConnection();
	using IModel channel = connection.CreateModel();

	byte[] messageBody = JsonSerializer.SerializeToUtf8Bytes(message.Message);
	publisher.SendMessage(channel, "testQueue", messageBody);
	return Results.Ok(new OkResponseMessage("Ok", "Your message has been sent to the queue!", message.Message));
}).Produces<OkResponseMessage>(200, "application/json").Produces<BadResponseMessage>(400, "application/json");

await app.RunAsync();