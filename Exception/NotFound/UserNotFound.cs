using Domain.Exceptions;

namespace Exception.NotFound
{
    public class UserNotFound(int UserId) :
        NotFoundException($"The User with the identifier {UserId} was not found.")
    {
    }
}
