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
    public class AddressesController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private IRepositoryWrapper _repository = repository;
        private ILoggerManager _logger = logger;
        private IMapper _mapper = mapper;

        // GET: api/Addresses
        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            try
            {
                var Addresses = await _repository.
                    AddressRepository.GetAllAddressAsync();
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
        [HttpGet("{id}")]
        public IActionResult GetAddressRepositoryById(Guid id)
        {
            try
            {
                var AddressRepository = _repository.AddressRepository.
                    GetAddressByIdAsync(id);

                if (AddressRepository is null)
                {
                    _logger.LogError($"AddressRepository with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned AddressRepository with id: {id}");

                    var AddressRepositoryResult = _mapper.
                        Map<AddressGetDto>(AddressRepository);
                    return Ok(AddressRepositoryResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAddressRepositoryById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddresse(Guid id, [FromBody] AddressPutDto Addresse)
        {
            try
            {
                if (Addresse == null)
                {
                    _logger.LogError("Addresse object sent from client is null.");
                    return BadRequest("Addresse object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Addresse object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var AddresseEntity = await _repository.AddressRepository.GetAddressByIdAsync(id);
                if (AddresseEntity == null)
                {
                    _logger.LogError($"Addresse with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(Addresse, AddresseEntity);

                _repository.AddressRepository.UpdateAddress(AddresseEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateAddresse action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostAddresse([FromBody] AddressPostDto Addresse, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Addresse is null)
                {
                    _logger.LogError("Addresse object sent from client is null.");
                    return BadRequest("Addresse object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Addresse object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var AddresseEntity = _mapper.Map<Address>(Addresse);

                _repository.AddressRepository.CreateAddress(AddresseEntity);
                await _repository.SaveAsync();

                var createdAddresse = _mapper.Map<AddressPostDto>(AddresseEntity);

                return CreatedAtRoute("AddresseById", new { id = createdAddresse.AddressId }, createdAddresse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAddresse action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddresse(Guid id)
        {
            try
            {
                var Addresse = await _repository.AddressRepository.GetAddressByIdAsync(id);
                if (Addresse == null)
                {
                    _logger.LogError($"Addresse with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.AddressRepository.DeleteAddress(Addresse);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete Addresse action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
