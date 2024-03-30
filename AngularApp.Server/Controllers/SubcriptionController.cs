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
    public class SubcriptionController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private readonly IRepositoryWrapper _repository = repository;
        private readonly ILoggerManager _logger = logger;
        private readonly IMapper _mapper = mapper;

        // GET: api/Subcriptions
        [HttpGet]
        public async Task<IActionResult> GetAllSubcriptions(CancellationToken cancellationToken = default)
        {
            try
            {
                var Subcriptions = await _repository.
                    SubcriptionRepository.GetAllAsync(cancellationToken);
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
        [HttpGet("{id}", Name = "SubcriptionsById")]
        public async Task<IActionResult> GetSubcriptionsById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var Subcriptions = await _repository.SubcriptionRepository
                    .GetByIdAsync(id, cancellationToken);
                if (Subcriptions == null)
                {
                    _logger.LogError($"Subcriptions with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Subcriptions with id: {id}");

                    var SubcriptionsResult = _mapper.Map<SubcriptionGetDto>(Subcriptions);
                    return Ok(SubcriptionsResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetSubcriptionsById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Subcriptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubcription
            (int id, [FromBody] SubcriptionPutDto Subcription, CancellationToken cancellationToken = default)
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

                var SubcriptionEntity = await _repository.SubcriptionRepository.GetByIdAsync(id, cancellationToken);
                if (SubcriptionEntity == null)
                {
                    _logger.LogError($"Subcription with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(Subcription, SubcriptionEntity);

                _repository.SubcriptionRepository.UpdateDetail(SubcriptionEntity);
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
        public IActionResult PostSubcription([FromBody] SubcriptionPostDto Subcription, CancellationToken cancellationToken = default)
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

                _repository.SubcriptionRepository.CreateDetail(SubcriptionEntity);
                _repository.SaveAsync();

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
        public async Task<IActionResult> DeleteSubcription(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var Subcription = await _repository.SubcriptionRepository.GetByIdAsync(id, cancellationToken);
                if (Subcription == null)
                {
                    _logger.LogError($"Subcription with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.SubcriptionRepository.DeleteDetail(Subcription);
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
