using System.Text.Json;
using DotNetRabbitMQ.Publisher.DependencyInjection;
using RabbitMQ.Client;

public static class TestClient
{
    /// <summary>
    /// Initilization for Test Client
    /// </summary>
    /// <param name="hostName">The hostname of RabiitMQ</param>
    /// <param name="port">The port of RabbitMQ</param>
    /// <param name="queueName">The queue message are being sent to</param>
    /// <param name="delay">The delay between each message</param>
    /// <param name="verbose">Enable logging of the message being sent (Default = false)</param>
    public static void Init(string hostName, short port, string queueName, int delay, bool verbose = false)
    {
        ConnectionFactory factory = new() { HostName = hostName, Port = port };
        using IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();

        Console.WriteLine($"[{DateTime.UtcNow}] Starting Test Client for {hostName}:{port} to {queueName} with {delay}ms delay");
        Start(channel, queueName, delay, verbose);
    }

    /// <summary>
    /// Send a test message to the queue after a delay
    /// </summary>
    /// <param name="channel">The IModel for the RabbitMQ Channel</param>
    /// <param name="queueName">The name of the queue</param>
    /// <param name="delay">The amount of delay between each message (Default = 1000ms)</param>
    /// <param name="verbose">Enable logging of the message being sent (Default = false)</param>
    public static void Start(IModel channel, string queueName, int delay = 1000, bool verbose = false)
    {
        Publisher publisher = new();

        int counter = 0;
        while (true) {
            publisher.SendMessage(channel, queueName, $"Sample test message #{counter++} from DotnetRabbitMQ.Test", verbose);
            Thread.Sleep(delay);
        }
    }
}

