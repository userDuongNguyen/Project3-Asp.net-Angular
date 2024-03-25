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
    public class SubcriptionsController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private IRepositoryWrapper _repository = repository;
        private ILoggerManager _logger = logger;
        private IMapper _mapper = mapper;

        // GET: api/Subcriptions
        [HttpGet]
        public async Task<IActionResult> GetAllSubcriptions()
        {
            try
            {
                var Subcriptions = await _repository.
                    SubcriptionRepository.GetAllSubcriptionsAsync();
                _logger.LogInfo($"Returned all Subcriptions from database.");

                var SubcriptionsResult = _mapper.Map<IEnumerable<SubcriptionGetDto>>(Subcriptions);
                return Ok(SubcriptionsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllSubcriptions action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Subcriptions/5
        [HttpGet("{id}")]
        public IActionResult GetSubcriptionRepositoryById(Guid id)
        {
            try
            {
                var SubcriptionRepository = _repository.SubcriptionRepository.
                    GetSubcriptionByIdAsync(id);

                if (SubcriptionRepository is null)
                {
                    _logger.LogError($"SubcriptionRepository with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned SubcriptionRepository with id: {id}");

                    var SubcriptionRepositoryResult = _mapper.
                        Map<SubcriptionGetDto>(SubcriptionRepository);
                    return Ok(SubcriptionRepositoryResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetSubcriptionRepositoryById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Subcriptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubcription(Guid id, [FromBody] SubcriptionPutDto Subcription)
        {
            try
            {
                if (Subcription == null)
                {
                    _logger.LogError("Subcription object sent from client is null.");
                    return BadRequest("Subcription object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Subcription object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var SubcriptionEntity = await _repository.SubcriptionRepository.GetSubcriptionByIdAsync(id);
                if (SubcriptionEntity == null)
                {
                    _logger.LogError($"Subcription with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(Subcription, SubcriptionEntity);

                _repository.SubcriptionRepository.UpdateSubcription(SubcriptionEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateSubcription action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Subcriptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostSubcription([FromBody] SubcriptionPostDto Subcription)
        {
            try
            {
                if (Subcription is null)
                {
                    _logger.LogError("Subcription object sent from client is null.");
                    return BadRequest("Subcription object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Subcription object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var SubcriptionEntity = _mapper.Map<Subcription>(Subcription);

                _repository.SubcriptionRepository.CreateSubcription(SubcriptionEntity);
                _repository.SubcriptionRepository.Save();

                var createdSubcription = _mapper.Map<SubcriptionPostDto>(SubcriptionEntity);

                return CreatedAtRoute("SubcriptionById", new { id = createdSubcription.Id }, createdSubcription);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateSubcription action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        // DELETE: api/Subcriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubcription(Guid id)
        {
            try
            {
                var Subcription = await _repository.SubcriptionRepository.GetSubcriptionByIdAsync(id);
                if (Subcription == null)
                {
                    _logger.LogError($"Subcription with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.SubcriptionRepository.DeleteSubcription(Subcription);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete Subcription action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
