using Domain.Exceptions;

namespace Exception.NotFound
{
    public class AgentNotFound(int AgentId) : NotFoundException($"The Agent with the identifier {AgentId} was not found.")
    {
    }
}
