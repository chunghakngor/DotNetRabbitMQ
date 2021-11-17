using System.CommandLine;
using System.CommandLine.Invocation;
using RabbitMQ.Client;

const short _port = 5672;
const short _delay = 1000;
const string _hostName = "localhost";
const string _queueName = "testQueue";

var rootCommand = new RootCommand {
        new Option<string>( "--host", () => _hostName, "The host url for RabbitMQ"),
        new Option<short>("--port", () => _port, "The port for RabbitMQ"),
        new Option<string>("--queue-name", () => _queueName, "The name of the queue that messages will be sent to"),
        new Option<int>("--delay", () => _delay, "The amount of delay between each message"),
        new Option<bool>("--verbose", () => false, "Enable logging of the message being sent"),
    };
rootCommand.Description = "Test Client for DotNetRabbitMQ. You can use this client to test the message published to client";

rootCommand.Handler = CommandHandler.Create<string, short, string, int, bool>((host, port, queueName, delay, verbose) => {
    TestClient.Init(host, port, queueName, delay, verbose);
});

return rootCommand.InvokeAsync(args).Result;
