namespace DotnetRabbitMQ.Publisher.DependencyInjection;

public interface IPublisher
{
	void SendMessage(IModel channel, string queueName, byte[] message);
}

public class Publisher : IPublisher
{
	public void SendMessage(IModel channel, string queueName, byte[] message)
	{
		channel.QueueDeclare(queueName, false, false, false, null);
		channel.BasicPublish("", queueName, null, message);
		Console.WriteLine($"A message has been sent to {queueName}!");
	}
}