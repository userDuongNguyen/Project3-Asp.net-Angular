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
    public class AccommodationDetailController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private readonly IRepositoryWrapper _repository = repository;
        private readonly ILoggerManager _logger = logger;
        private readonly IMapper _mapper = mapper;

        // GET: api/AccommodationDetails
        [HttpGet]
        public async Task<IActionResult> GetAllAccommodationDetails(CancellationToken cancellationToken = default)
        {
            try
            {
                var AccommodationDetails = await _repository.
                    AccommodationDetailRepository.GetAllAsync(cancellationToken);
                _logger.LogInfo($"Returned all AccommodationDetails from database.");

                var AccommodationDetailsResult = _mapper.Map<IEnumerable<AccommodationDetailGetDto>>(AccommodationDetails);
                return Ok(AccommodationDetailsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAccommodationDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/AccommodationDetails/5
        [HttpGet("{id}", Name = "AccommodationDetailsById")]
        public async Task<IActionResult> GetAccommodationDetailsById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var AccommodationDetails = await _repository.AccommodationDetailRepository
                    .GetByIdAsync(id, cancellationToken);
                if (AccommodationDetails == null)
                {
                    _logger.LogError($"AccommodationDetails with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned AccommodationDetails with id: {id}");

                    var AccommodationDetailsResult = _mapper.Map<AccommodationDetailGetDto>(AccommodationDetails);
                    return Ok(AccommodationDetailsResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAccommodationDetailsById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/AccommodationDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccommodationDetail
            (int id, [FromBody] AccommodationDetailPutDto AccommodationDetail, CancellationToken cancellationToken = default)
        {
            try
            {
                if (AccommodationDetail == null)
                {
                    _logger.LogError("AccommodationDetail object sent from client is null.");
                    return BadRequest("AccommodationDetail object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid AccommodationDetail object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var AccommodationDetailEntity = await _repository.AccommodationDetailRepository.GetByIdAsync(id, cancellationToken);
                if (AccommodationDetailEntity == null)
                {
                    _logger.LogError($"AccommodationDetail with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(AccommodationDetail, AccommodationDetailEntity);

                _repository.AccommodationDetailRepository.UpdateDetail(AccommodationDetailEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateAccommodationDetail action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/AccommodationDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostAccommodationDetail([FromBody] AccommodationDetailPostDto AccommodationDetail, CancellationToken cancellationToken = default)
        {
            try
            {
                if (AccommodationDetail is null)
                {
                    _logger.LogError("AccommodationDetail object sent from client is null.");
                    return BadRequest("AccommodationDetail object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid AccommodationDetail object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var AccommodationDetailEntity = _mapper.Map<AccommodationDetail>(AccommodationDetail);

                _repository.AccommodationDetailRepository.CreateDetail(AccommodationDetailEntity);
                _repository.SaveAsync();

                var createdAccommodationDetail = _mapper.Map<AccommodationDetailPostDto>(AccommodationDetailEntity);

                return CreatedAtRoute("AccommodationDetailById", new { id = createdAccommodationDetail.Id }, createdAccommodationDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAccommodationDetail action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        // DELETE: api/AccommodationDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccommodationDetail(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var AccommodationDetail = await _repository.AccommodationDetailRepository.GetByIdAsync(id,cancellationToken);
                if (AccommodationDetail == null)
                {
                    _logger.LogError($"AccommodationDetail with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.AccommodationDetailRepository.DeleteDetail(AccommodationDetail);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete AccommodationDetail action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
