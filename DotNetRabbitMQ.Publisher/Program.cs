using System.Text.Json;
using RabbitMQ.Client;

Console.WriteLine("Starting RabbitMQ Publisher!");

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapGet("/send",
           httpContext => {
               if (!httpContext.Request.Query.ContainsKey("message")) {
                   return httpContext.Response.WriteAsJsonAsync(new ResponseMessage("Error", "You need to send a message as a query parameter"));
               }

               string message = httpContext.Request.Query["message"];
               if (string.IsNullOrEmpty(message)) {
                   return httpContext.Response.WriteAsJsonAsync(new ResponseMessage("Error", "Your message cannot be null or empty"));
               }

               byte[] messageBody = JsonSerializer.SerializeToUtf8Bytes(message);

               ConnectionFactory factory = new() {HostName = "localhost", Port = 5672};
               using IConnection connection = factory.CreateConnection();
               using IModel channel = connection.CreateModel();

               string queueName = "testQueue";
               channel.QueueDeclare(queueName, false, false, false, null);

               channel.BasicPublish("", queueName, null, messageBody);
               Console.WriteLine($"Send message to {queueName}");

               return httpContext.Response.WriteAsJsonAsync(new ResponseMessage("Ok", "Your message has been sent to the queue!", message));
           });

await app.RunAsync();

public record ResponseMessage(string Status, string Message, string Content = null);