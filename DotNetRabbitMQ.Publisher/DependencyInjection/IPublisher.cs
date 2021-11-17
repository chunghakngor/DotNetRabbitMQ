namespace DotNetRabbitMQ.Publisher.DependencyInjection;

public interface IPublisher
{
	void SendMessage(IModel channel, string queueName, byte[] message);

	void SendMessage(IModel channel, string queueName, string message);
}

public class Publisher : IPublisher
{
	/// <summary>
	/// Send a basic message to the queue
	/// </summary>
	/// <param name="channel">The IModel for the RabbitMQ Channel</param>
	/// <param name="queueName">The name of the queue</param>
	/// <param name="message">The message that you want to send as a byte[]</param>
	public void SendMessage(IModel channel, string queueName, byte[] message)
	{
		channel.QueueDeclare(queueName, false, false, false, null);
		channel.BasicPublish("", queueName, null, message);
		Console.WriteLine($"A message has been sent to {queueName}!");
	}

	/// <summary>
	/// Send a basic message to the queue
	/// </summary>
	/// <param name="channel">The IModel for the RabbitMQ Channel</param>
	/// <param name="queueName">The name of the queue</param>
	/// <param name="message">The message that you want to send</param>
	public void SendMessage(IModel channel, string queueName, string message)
	{
		channel.QueueDeclare(queueName, false, false, false, null);
		channel.BasicPublish("", queueName, null, JsonSerializer.SerializeToUtf8Bytes(message));
		Console.WriteLine($"A message has been sent to {queueName}!");
	}
}