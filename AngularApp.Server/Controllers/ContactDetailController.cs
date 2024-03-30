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
    public class ContactDetailController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private readonly IRepositoryWrapper _repository = repository;
        private readonly ILoggerManager _logger = logger;
        private readonly IMapper _mapper = mapper;

        // GET: api/ContactDetails
        [HttpGet]
        public async Task<IActionResult> GetAllContactDetails(CancellationToken cancellationToken = default)
        {
            try
            {
                var ContactDetails = await _repository.
                    ContactDetailRepository.GetAllAsync(cancellationToken);
                _logger.LogInfo($"Returned all ContactDetails from database.");

                var ContactDetailsResult = _mapper.Map<IEnumerable<ContactDetailGetDto>>(ContactDetails);
                return Ok(ContactDetailsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllContactDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/ContactDetails/5
        [HttpGet("{id}", Name = "ContactDetailsById")]
        public async Task<IActionResult> GetContactDetailsById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var ContactDetails = await _repository.ContactDetailRepository
                    .GetByIdAsync(id, cancellationToken);
                if (ContactDetails == null)
                {
                    _logger.LogError($"ContactDetails with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned ContactDetails with id: {id}");

                    var ContactDetailsResult = _mapper.Map<ContactDetailGetDto>(ContactDetails);
                    return Ok(ContactDetailsResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetContactDetailsById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/ContactDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContactDetail
            (int id, [FromBody] ContactDetailPutDto ContactDetail, CancellationToken cancellationToken = default)
        {
            try
            {
                if (ContactDetail == null)
                {
                    _logger.LogError("ContactDetail object sent from client is null.");
                    return BadRequest("ContactDetail object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid ContactDetail object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var ContactDetailEntity = await _repository.ContactDetailRepository.GetByIdAsync(id, cancellationToken);
                if (ContactDetailEntity == null)
                {
                    _logger.LogError($"ContactDetail with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(ContactDetail, ContactDetailEntity);

                _repository.ContactDetailRepository.UpdateDetail(ContactDetailEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateContactDetail action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/ContactDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostContactDetail([FromBody] ContactDetailPostDto ContactDetail, CancellationToken cancellationToken = default)
        {
            try
            {
                if (ContactDetail is null)
                {
                    _logger.LogError("ContactDetail object sent from client is null.");
                    return BadRequest("ContactDetail object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid ContactDetail object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var ContactDetailEntity = _mapper.Map<ContactDetail>(ContactDetail);

                _repository.ContactDetailRepository.CreateDetail(ContactDetailEntity);
                _repository.SaveAsync();

                var createdContactDetail = _mapper.Map<ContactDetailPostDto>(ContactDetailEntity);

                return CreatedAtRoute("ContactDetailById", new { id = createdContactDetail.Id }, createdContactDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateContactDetail action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        // DELETE: api/ContactDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactDetail(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var ContactDetail = await _repository.ContactDetailRepository.GetByIdAsync(id, cancellationToken);
                if (ContactDetail == null)
                {
                    _logger.LogError($"ContactDetail with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.ContactDetailRepository.DeleteDetail(ContactDetail);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete ContactDetail action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
