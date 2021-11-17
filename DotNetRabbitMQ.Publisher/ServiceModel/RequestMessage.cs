namespace DotNetRabbitMQ.Publisher.ServiceModels;

public class RequestMessage
{
    [Required]
    public string? Message { get; set; }
}
