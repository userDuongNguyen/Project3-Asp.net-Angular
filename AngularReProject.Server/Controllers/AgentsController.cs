using AutoMapper;
using Contracts;
using Contracts.RepositoryInterfaces;
using Domain.DataTransferObjects.GetDto;
using Domain.DataTransferObjects.PostDto;
using Domain.DataTransferObjects.PutDto;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AngularReProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private IRepositoryWrapper _repository = repository;
        private ILoggerManager _logger = logger;
        private IMapper _mapper = mapper;

        // GET: api/Agents
        [HttpGet]
        public async Task<IActionResult> GetAllAgents()
        {
            try
            {
                var Agents = await _repository.
                    AgentRepository.GetAllAgentsAsync();
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
        [HttpGet("{id}")]
        public IActionResult GetAgentRepositoryById(Guid id)
        {
            try
            {
                var AgentRepository = _repository.AgentRepository.
                    GetAgentByIdAsync(id);

                if (AgentRepository is null)
                {
                    _logger.LogError($"AgentRepository with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned AgentRepository with id: {id}");

                    var AgentRepositoryResult = _mapper.
                        Map<AgentGetDto>(AgentRepository);
                    return Ok(AgentRepositoryResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAgentRepositoryById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Agents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAgent(Guid id, [FromBody] AgentPutDto Agent)
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

                var AgentEntity = await _repository.AgentRepository.GetAgentByIdAsync(id);
                if (AgentEntity == null)
                {
                    _logger.LogError($"Agent with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(Agent, AgentEntity);

                _repository.AgentRepository.UpdateAgent(AgentEntity);
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
        public IActionResult PostAgent([FromBody] AgentPostDto Agent)
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

                _repository.AgentRepository.CreateAgent(AgentEntity);
                _repository.AgentRepository.Save();

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
        public async Task<IActionResult> DeleteAgent(Guid id)
        {
            try
            {
                var Agent = await _repository.AgentRepository.GetAgentByIdAsync(id);
                if (Agent == null)
                {
                    _logger.LogError($"Agent with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.AgentRepository.DeleteAgent(Agent);
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
