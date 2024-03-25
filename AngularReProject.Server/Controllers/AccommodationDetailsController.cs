using AutoMapper;
using Contracts;
using Contracts.RepositoryInterfaces;
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
    public class AccommodationDetailsController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private readonly IRepositoryWrapper _repository = repository;
        private readonly ILoggerManager _logger = logger;
        private readonly IMapper _mapper = mapper;

        // GET: api/AccommodationDetails
        [HttpGet]
        public async Task<IActionResult> GetAllAccommodationDetails()
        {
            try
            {
                var AccommodationDetails = await _repository.
                    AccommodationDetailRepository.GetAllAccommodationDetailsAsync();
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
        public async Task<IActionResult> GetAccommodationDetailsById(Guid id)
        {
            try
            {
                var AccommodationDetails = await _repository.AccommodationDetailRepository
                    .GetAccommodationDetailByIdAsync(id);
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
            (Guid id, [FromBody] AccommodationDetailPutDto AccommodationDetail)
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

                var AccommodationDetailEntity = await _repository.AccommodationDetailRepository.GetAccommodationDetailByIdAsync(id);
                if (AccommodationDetailEntity == null)
                {
                    _logger.LogError($"AccommodationDetail with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(AccommodationDetail, AccommodationDetailEntity);

                _repository.AccommodationDetailRepository.UpdateAccommodationDetail(AccommodationDetailEntity);
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
        public IActionResult PostAccommodationDetail([FromBody] AccommodationDetailPostDto AccommodationDetail)
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

                _repository.AccommodationDetailRepository.CreateAccommodationDetail(AccommodationDetailEntity);
                _repository.AccommodationDetailRepository.Save();

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
        public async Task<IActionResult> DeleteAccommodationDetail(Guid id)
        {
            try
            {
                var AccommodationDetail = await _repository.AccommodationDetailRepository.GetAccommodationDetailByIdAsync(id);
                if (AccommodationDetail == null)
                {
                    _logger.LogError($"AccommodationDetail with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.AccommodationDetailRepository.DeleteAccommodationDetail(AccommodationDetail);
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
