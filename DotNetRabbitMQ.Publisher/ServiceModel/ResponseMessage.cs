public record OkResponseMessage(string Status, string Message, string Content = null);

public record BadResponseMessage(string Status, string Message);