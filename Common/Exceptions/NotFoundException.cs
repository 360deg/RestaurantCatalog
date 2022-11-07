namespace Common.Exceptions;

public class NotFoundException : Exception
{
    private static readonly string _defaultMessage = "Not found";
    public NotFoundException(): base(_defaultMessage) { }
    public NotFoundException(string message): base(message) { }
}
