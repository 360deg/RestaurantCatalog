namespace Common.Exceptions;

public class BadRequestException : Exception
{
    private static readonly string _defaultMessage = "Bad request";
    public BadRequestException(): base(_defaultMessage) { }
    public BadRequestException(string message): base(message) { }
}
