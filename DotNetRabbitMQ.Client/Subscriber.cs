using RabbitMQ.Client.Events;

public static class Subscriber
{
	public static void Listen<T>(IModel channel, string queueName)
	{
		channel.QueueDeclare(queueName, false, false, false, null);
		EventingBasicConsumer consumer = new(channel);
		consumer.Received += (model, eventArgs) => {
			T? body = JsonSerializer.Deserialize<T>(eventArgs.Body.ToArray());
			Console.WriteLine($"Received Message from {queueName}: {body}");
		};
		channel.BasicConsume(queueName, true, consumer);
	}
}