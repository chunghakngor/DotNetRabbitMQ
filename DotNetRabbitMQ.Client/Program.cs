Console.WriteLine("Starting RabbitMQ Client!");

const string _hostName = "localhost";
const short _port = 5672;
const string _queueName = "testQueue";

ConnectionFactory factory = new() { HostName = _hostName, Port = _port };
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

Subscriber.Listen<string>(channel, _queueName);

Console.ReadLine();
