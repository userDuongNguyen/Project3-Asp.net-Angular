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
    public class ContactDetailsController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private IRepositoryWrapper _repository = repository;
        private ILoggerManager _logger = logger;
        private IMapper _mapper = mapper;

        // GET: api/ContactDetails
        [HttpGet]
        public async Task<IActionResult> GetAllContactDetails()
        {
            try
            {
                var ContactDetails = await _repository.
                    ContactDetailRepository.GetAllContactDetailsAsync();
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
        [HttpGet("{id}")]
        public IActionResult GetContactDetailRepositoryById(Guid id)
        {
            try
            {
                var ContactDetailRepository = _repository.ContactDetailRepository.
                    GetContactDetailByIdAsync(id);

                if (ContactDetailRepository is null)
                {
                    _logger.LogError($"ContactDetailRepository with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned ContactDetailRepository with id: {id}");

                    var ContactDetailRepositoryResult = _mapper.
                        Map<ContactDetailGetDto>(ContactDetailRepository);
                    return Ok(ContactDetailRepositoryResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetContactDetailRepositoryById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/ContactDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContactDetail(Guid id, [FromBody] ContactDetailPutDto ContactDetail)
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

                var ContactDetailEntity = await _repository.ContactDetailRepository.GetContactDetailByIdAsync(id);
                if (ContactDetailEntity == null)
                {
                    _logger.LogError($"ContactDetail with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(ContactDetail, ContactDetailEntity);

                _repository.ContactDetailRepository.UpdateContactDetail(ContactDetailEntity);
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
        public IActionResult PostContactDetail([FromBody] ContactDetailPostDto ContactDetail)
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

                _repository.ContactDetailRepository.CreateContactDetail(ContactDetailEntity);
                _repository.ContactDetailRepository.Save();

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
        public async Task<IActionResult> DeleteContactDetail(Guid id)
        {
            try
            {
                var ContactDetail = await _repository.ContactDetailRepository.GetContactDetailByIdAsync(id);
                if (ContactDetail == null)
                {
                    _logger.LogError($"ContactDetail with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.ContactDetailRepository.DeleteContactDetail(ContactDetail);
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
