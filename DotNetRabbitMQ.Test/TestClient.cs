using System.Text.Json;
using DotNetRabbitMQ.Publisher.DependencyInjection;
using RabbitMQ.Client;

public static class TestClient
{
	/// <summary>
	/// Send a test message to the queue after a delay
	/// </summary>
	/// <param name="channel">The IModel for the RabbitMQ Channel</param>
	/// <param name="queueName">The name of the queue</param>
	/// <param name="delay">The amount of delay between each message (Default = 1000ms)</param>
	public static void Start(IModel channel, string queueName, int delay = 1000)
	{
		Publisher publisher = new();

		int counter = 0;
		while (true) {
			byte[] messageBody = JsonSerializer.SerializeToUtf8Bytes($"Sample test message #{counter++} from DotnetRabbitMQ.Test");
			publisher.SendMessage(channel, queueName, messageBody);
			Thread.Sleep(delay);
		}
	}
}

