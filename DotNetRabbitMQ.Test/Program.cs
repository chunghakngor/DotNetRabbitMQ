using RabbitMQ.Client;

const string _hostName = "localhost";
const int _port = 5672;
const string _queueName = "testQueue";

ConnectionFactory factory = new() { HostName = _hostName, Port = _port };
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

TestClient.Start(channel, _queueName);