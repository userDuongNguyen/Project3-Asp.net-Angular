namespace Domain.Exceptions.NotFound
{
    public class UserNotFound(Guid UserId) :
        NotFoundException($"The User with the identifier {UserId} was not found.")
    {
    }
}
