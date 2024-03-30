using AutoMapper;
using Contracts.RepositoryInterfaces;
using Domain.Contracts;
using Domain.DataTransferObjects.GetDto;
using Domain.DataTransferObjects.PostDto;
using Domain.DataTransferObjects.PutDto;
using Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AngularReProject.Server.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private readonly IRepositoryWrapper _repository = repository;
        private readonly ILoggerManager _logger = logger;
        private readonly IMapper _mapper = mapper;

        // GET: api/Agents
        [HttpGet]
        public async Task<IActionResult> GetAllAgents(CancellationToken cancellationToken = default)
        {
            try
            {
                var Agents = await _repository.
                    AgentRepository.GetAllAsync(cancellationToken);
                _logger.LogInfo($"Returned all Agents from database.");

                var AgentsResult = _mapper.Map<IEnumerable<AgentGetDto>>(Agents);
                return Ok(AgentsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAgents action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Agents/5
        [HttpGet("{id}", Name = "AgentsById")]
        public async Task<IActionResult> GetAgentsById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var Agents = await _repository.AgentRepository
                    .GetByIdAsync(id, cancellationToken);
                if (Agents == null)
                {
                    _logger.LogError($"Agents with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agents with id: {id}");

                    var AgentsResult = _mapper.Map<AgentGetDto>(Agents);
                    return Ok(AgentsResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAgentsById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Agents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAgent
            (int id, [FromBody] AgentPutDto Agent, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Agent == null)
                {
                    _logger.LogError("Agent object sent from client is null.");
                    return BadRequest("Agent object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Agent object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var AgentEntity = await _repository.AgentRepository.GetByIdAsync(id, cancellationToken);
                if (AgentEntity == null)
                {
                    _logger.LogError($"Agent with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(Agent, AgentEntity);

                _repository.AgentRepository.UpdateDetail(AgentEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateAgent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Agents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostAgent([FromBody] AgentPostDto Agent, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Agent is null)
                {
                    _logger.LogError("Agent object sent from client is null.");
                    return BadRequest("Agent object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Agent object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var AgentEntity = _mapper.Map<Agent>(Agent);

                _repository.AgentRepository.CreateDetail(AgentEntity);
                _repository.SaveAsync();

                var createdAgent = _mapper.Map<AgentPostDto>(AgentEntity);

                return CreatedAtRoute("AgentById", new { id = createdAgent.Id }, createdAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAgent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        // DELETE: api/Agents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgent(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var Agent = await _repository.AgentRepository.GetByIdAsync(id, cancellationToken);
                if (Agent == null)
                {
                    _logger.LogError($"Agent with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.AgentRepository.DeleteDetail(Agent);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete Agent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
