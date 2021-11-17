namespace DotNetRabbitMQ.Publisher.DependencyInjection;

public interface IPublisher
{
    void SendMessage(IModel channel, string queueName, byte[] message, bool verbose = false);

    void SendMessage(IModel channel, string queueName, string message, bool verbose = false);
}

public class Publisher : IPublisher
{
    /// <summary>
    /// Send a basic message to the queue
    /// </summary>
    /// <param name="channel">The IModel for the RabbitMQ Channel</param>
    /// <param name="queueName">The name of the queue</param>
    /// <param name="message">The message that you want to send as a byte[]</param>
    public void SendMessage(IModel channel, string queueName, byte[] message, bool verbose = false)
    {
        channel.QueueDeclare(queueName, false, false, false, null);
        channel.BasicPublish("", queueName, null, message);
        Console.WriteLine($"[{DateTime.UtcNow}] A message has been sent to {queueName}!");

        if (verbose) {
            Console.WriteLine($"[{DateTime.UtcNow}] Message: {JsonSerializer.Deserialize<string>(message)}");
        }
    }

    /// <summary>
    /// Send a basic message to the queue
    /// </summary>
    /// <param name="channel">The IModel for the RabbitMQ Channel</param>
    /// <param name="queueName">The name of the queue</param>
    /// <param name="message">The message that you want to send</param>
    public void SendMessage(IModel channel, string queueName, string message, bool verbose = false)
    {
        channel.QueueDeclare(queueName, false, false, false, null);
        channel.BasicPublish("", queueName, null, JsonSerializer.SerializeToUtf8Bytes(message));
        Console.WriteLine($"[{DateTime.UtcNow}] Message has been sent to {queueName}!");

        if (verbose) {
            Console.WriteLine($"[{DateTime.UtcNow}] Message: {message}");
        }
    }
}