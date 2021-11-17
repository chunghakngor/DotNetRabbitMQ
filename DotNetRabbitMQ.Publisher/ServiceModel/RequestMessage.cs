namespace DotnetRabbitMQ.Publisher.ServiceModels;

public class RequestMessage
{
	[Required]
	public string? Message { get; set; }
}
