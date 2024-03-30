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
    public class RentalFeeController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private readonly IRepositoryWrapper _repository = repository;
        private readonly ILoggerManager _logger = logger;
        private readonly IMapper _mapper = mapper;

        // GET: api/RentalFees
        [HttpGet]
        public async Task<IActionResult> GetAllRentalFees(CancellationToken cancellationToken = default)
        {
            try
            {
                var RentalFees = await _repository.
                    RentalFeeRepository.GetAllAsync(cancellationToken);
                _logger.LogInfo($"Returned all RentalFees from database.");

                var RentalFeesResult = _mapper.Map<IEnumerable<RentalFeeGetDto>>(RentalFees);
                return Ok(RentalFeesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllRentalFees action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/RentalFees/5
        [HttpGet("{id}", Name = "RentalFeesById")]
        public async Task<IActionResult> GetRentalFeesById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var RentalFees = await _repository.RentalFeeRepository
                    .GetByIdAsync(id, cancellationToken);
                if (RentalFees == null)
                {
                    _logger.LogError($"RentalFees with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned RentalFees with id: {id}");

                    var RentalFeesResult = _mapper.Map<RentalFeeGetDto>(RentalFees);
                    return Ok(RentalFeesResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetRentalFeesById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/RentalFees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRentalFee
            (int id, [FromBody] RentalFeePutDto RentalFee, CancellationToken cancellationToken = default)
        {
            try
            {
                if (RentalFee == null)
                {
                    _logger.LogError("RentalFee object sent from client is null.");
                    return BadRequest("RentalFee object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid RentalFee object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var RentalFeeEntity = await _repository.RentalFeeRepository.GetByIdAsync(id, cancellationToken);
                if (RentalFeeEntity == null)
                {
                    _logger.LogError($"RentalFee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(RentalFee, RentalFeeEntity);

                _repository.RentalFeeRepository.UpdateDetail(RentalFeeEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateRentalFee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/RentalFees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostRentalFee([FromBody] RentalFeePostDto RentalFee, CancellationToken cancellationToken = default)
        {
            try
            {
                if (RentalFee is null)
                {
                    _logger.LogError("RentalFee object sent from client is null.");
                    return BadRequest("RentalFee object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid RentalFee object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var RentalFeeEntity = _mapper.Map<RentalFee>(RentalFee);

                _repository.RentalFeeRepository.CreateDetail(RentalFeeEntity);
                _repository.SaveAsync();

                var createdRentalFee = _mapper.Map<RentalFeePostDto>(RentalFeeEntity);

                return CreatedAtRoute("RentalFeeById", new { id = createdRentalFee.Id }, createdRentalFee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateRentalFee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        // DELETE: api/RentalFees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalFee(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var RentalFee = await _repository.RentalFeeRepository.GetByIdAsync(id, cancellationToken);
                if (RentalFee == null)
                {
                    _logger.LogError($"RentalFee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.RentalFeeRepository.DeleteDetail(RentalFee);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete RentalFee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
