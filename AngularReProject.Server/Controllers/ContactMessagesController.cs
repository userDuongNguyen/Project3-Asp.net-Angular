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
    public class ContactMessagesController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private IRepositoryWrapper _repository = repository;
        private ILoggerManager _logger = logger;
        private IMapper _mapper = mapper;

        // GET: api/ContactMessages
        [HttpGet]
        public async Task<IActionResult> GetAllContactMessages()
        {
            try
            {
                var ContactMessages = await _repository.
                    ContactMessageRepository.GetAllContactMessagesAsync();
                _logger.LogInfo($"Returned all ContactMessages from database.");

                var ContactMessagesResult = _mapper.Map<IEnumerable<ContactMessageGetDto>>(ContactMessages);
                return Ok(ContactMessagesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllContactMessages action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/ContactMessages/5
        [HttpGet("{id}")]
        public IActionResult GetContactMessageRepositoryById(Guid id)
        {
            try
            {
                var ContactMessageRepository = _repository.ContactMessageRepository.
                    GetContactMessageByIdAsync(id);

                if (ContactMessageRepository is null)
                {
                    _logger.LogError($"ContactMessageRepository with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned ContactMessageRepository with id: {id}");

                    var ContactMessageRepositoryResult = _mapper.
                        Map<ContactMessageGetDto>(ContactMessageRepository);
                    return Ok(ContactMessageRepositoryResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetContactMessageRepositoryById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/ContactMessages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContactMessage
            (Guid id, [FromBody] ContactMessagePutDto ContactMessage)
        {
            try
            {
                if (ContactMessage == null)
                {
                    _logger.LogError("ContactMessage object sent from client is null.");
                    return BadRequest("ContactMessage object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid ContactMessage object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var ContactMessageEntity = await _repository.ContactMessageRepository.GetContactMessageByIdAsync(id);
                if (ContactMessageEntity == null)
                {
                    _logger.LogError($"ContactMessage with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(ContactMessage, ContactMessageEntity);

                _repository.ContactMessageRepository.UpdateContactMessage(ContactMessageEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateContactMessage action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/ContactMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostContactMessage([FromBody] ContactMessagePostDto ContactMessage)
        {
            try
            {
                if (ContactMessage is null)
                {
                    _logger.LogError("ContactMessage object sent from client is null.");
                    return BadRequest("ContactMessage object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid ContactMessage object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var ContactMessageEntity = _mapper.Map<ContactMessage>(ContactMessage);

                _repository.ContactMessageRepository.CreateContactMessage(ContactMessageEntity);
                _repository.ContactMessageRepository.Save();

                var createdContactMessage = _mapper.Map<ContactMessagePostDto>(ContactMessageEntity);

                return CreatedAtRoute("ContactMessageById", new { id = createdContactMessage.Id }, createdContactMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateContactMessage action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        // DELETE: api/ContactMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactMessage(Guid id)
        {
            try
            {
                var ContactMessage = await _repository.ContactMessageRepository.GetContactMessageByIdAsync(id);
                if (ContactMessage == null)
                {
                    _logger.LogError($"ContactMessage with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.ContactMessageRepository.DeleteContactMessage(ContactMessage);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete ContactMessage action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
