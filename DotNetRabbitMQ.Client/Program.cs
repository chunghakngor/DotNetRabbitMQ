using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Starting RabbitMQ Client!");

ConnectionFactory factory = new() {HostName = "localhost", Port = 5672};
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string queueName = "testQueue";
channel.QueueDeclare(queueName, false, false, false, null);

EventingBasicConsumer consumer = new(channel);

consumer.Received += (model, eventArgs) => {
    string? body = JsonSerializer.Deserialize<string>(eventArgs.Body.ToArray());
    Console.WriteLine($"Received Message from {queueName}: {body}");
};

channel.BasicConsume(queueName, true, consumer);

Console.ReadLine();