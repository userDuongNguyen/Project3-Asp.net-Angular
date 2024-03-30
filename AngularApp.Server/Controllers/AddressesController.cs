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
    public class AddressController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private readonly IRepositoryWrapper _repository = repository;
        private readonly ILoggerManager _logger = logger;
        private readonly IMapper _mapper = mapper;

        // GET: api/Addresses
        [HttpGet]
        public async Task<IActionResult> GetAllAddresses(CancellationToken cancellationToken = default)
        {
            try
            {
                var Addresses = await _repository.
                    AddressRepository.GetAllAsync(cancellationToken);
                _logger.LogInfo($"Returned all Addresses from database.");

                var AddressesResult = _mapper.Map<IEnumerable<AddressGetDto>>(Addresses);
                return Ok(AddressesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAddresses action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Addresses/5
        [HttpGet("{id}", Name = "AddressesById")]
        public async Task<IActionResult> GetAddressesById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var Addresses = await _repository.AddressRepository
                    .GetByIdAsync(id, cancellationToken);
                if (Addresses == null)
                {
                    _logger.LogError($"Addresses with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Addresses with id: {id}");

                    var AddressesResult = _mapper.Map<AddressGetDto>(Addresses);
                    return Ok(AddressesResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAddressesById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress
            (int id, [FromBody] AddressPutDto Address, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Address == null)
                {
                    _logger.LogError("Address object sent from client is null.");
                    return BadRequest("Address object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Address object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var AddressEntity = await _repository.AddressRepository.GetByIdAsync(id, cancellationToken);
                if (AddressEntity == null)
                {
                    _logger.LogError($"Address with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(Address, AddressEntity);

                _repository.AddressRepository.UpdateDetail(AddressEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateAddress action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostAddress([FromBody] AddressPostDto Address, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Address is null)
                {
                    _logger.LogError("Address object sent from client is null.");
                    return BadRequest("Address object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Address object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var AddressEntity = _mapper.Map<Address>(Address);

                _repository.AddressRepository.CreateDetail(AddressEntity);
                _repository.SaveAsync();

                var createdAddress = _mapper.Map<AddressPostDto>(AddressEntity);

                return CreatedAtRoute("AddressById", new { id = createdAddress.Id }, createdAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAddress action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var Address = await _repository.AddressRepository.GetByIdAsync(id, cancellationToken);
                if (Address == null)
                {
                    _logger.LogError($"Address with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.AddressRepository.DeleteDetail(Address);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete Address action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
