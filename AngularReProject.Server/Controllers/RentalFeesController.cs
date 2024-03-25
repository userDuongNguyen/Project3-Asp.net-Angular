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
    public class RentalFeessController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private IRepositoryWrapper _repository = repository;
        private ILoggerManager _logger = logger;
        private IMapper _mapper = mapper;

        // GET: api/RentalFeess
        [HttpGet]
        public async Task<IActionResult> GetAllRentalFeess()
        {
            try
            {
                var RentalFeess = await _repository.
                    RentalFeesRepository.GetAllRentalFeessAsync();
                _logger.LogInfo($"Returned all RentalFeess from database.");

                var RentalFeessResult = _mapper.Map<IEnumerable<RentalFeesGetDto>>(RentalFeess);
                return Ok(RentalFeessResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllRentalFeess action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/RentalFeess/5
        [HttpGet("{id}")]
        public IActionResult GetRentalFeesRepositoryById(Guid id)
        {
            try
            {
                var RentalFeesRepository = _repository.RentalFeesRepository.
                    GetRentalFeesByIdAsync(id);

                if (RentalFeesRepository is null)
                {
                    _logger.LogError($"RentalFeesRepository with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned RentalFeesRepository with id: {id}");

                    var RentalFeesRepositoryResult = _mapper.
                        Map<RentalFeesGetDto>(RentalFeesRepository);
                    return Ok(RentalFeesRepositoryResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetRentalFeesRepositoryById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/RentalFeess/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRentalFees(Guid id, [FromBody] RentalFeesPutDto RentalFees)
        {
            try
            {
                if (RentalFees == null)
                {
                    _logger.LogError("RentalFees object sent from client is null.");
                    return BadRequest("RentalFees object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid RentalFees object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var RentalFeesEntity = await _repository.RentalFeesRepository.GetRentalFeesByIdAsync(id);
                if (RentalFeesEntity == null)
                {
                    _logger.LogError($"RentalFees with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(RentalFees, RentalFeesEntity);

                _repository.RentalFeesRepository.UpdateRentalFees(RentalFeesEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateRentalFees action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/RentalFeess
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostRentalFees([FromBody] RentalFeesPostDto RentalFees)
        {
            try
            {
                if (RentalFees is null)
                {
                    _logger.LogError("RentalFees object sent from client is null.");
                    return BadRequest("RentalFees object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid RentalFees object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var RentalFeesEntity = _mapper.Map<RentalFees>(RentalFees);

                _repository.RentalFeesRepository.CreateRentalFees(RentalFeesEntity);
                _repository.RentalFeesRepository.Save();

                var createdRentalFees = _mapper.Map<RentalFeesPostDto>(RentalFeesEntity);

                return CreatedAtRoute("RentalFeesById", new { id = createdRentalFees.Id }, createdRentalFees);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateRentalFees action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        // DELETE: api/RentalFeess/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalFees(Guid id)
        {
            try
            {
                var RentalFees = await _repository.RentalFeesRepository.GetRentalFeesByIdAsync(id);
                if (RentalFees == null)
                {
                    _logger.LogError($"RentalFees with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.RentalFeesRepository.DeleteRentalFees(RentalFees);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete RentalFees action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
