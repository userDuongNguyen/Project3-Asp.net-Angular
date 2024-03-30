namespace Exceptions.Abstraction
{
    public abstract class NotFoundException(string message): Exception(message)
    {
    }
}
