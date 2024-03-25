namespace Domain.Exceptions.NotFound
{
    public class AgentNotFound(Guid AgentId) : NotFoundException($"The Agent with the identifier {AgentId} was not found.")
    {
    }
}
