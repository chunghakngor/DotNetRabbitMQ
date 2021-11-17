# DotNetRabbitMQ Test Client

```
DotNetRabbitMQ.Test
  Test Client for DotNetRabbitMQ. You can use this client to test the message published to client

Usage:
  DotNetRabbitMQ.Test [options]

Options:
  --host <host>              The host url for RabbitMQ [default: localhost]
  --port <port>              The port for RabbitMQ [default: 5672]
  --queue-name <queue-name>  The name of the queue that messages will be sent to [default: testQueue]
  --delay <delay>            The amount of delay between each message [default: 1000]
  --version                  Show version information
  -?, -h, --help             Show help and usage information
```

Start by running `dotnet run` or with optional arguments `dotnet run --host localhost --port 5672 --queue-name testQueue --delay 1000`
