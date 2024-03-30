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
    public class ContactMessageController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private readonly IRepositoryWrapper _repository = repository;
        private readonly ILoggerManager _logger = logger;
        private readonly IMapper _mapper = mapper;

        // GET: api/ContactMessages
        [HttpGet]
        public async Task<IActionResult> GetAllContactMessages(CancellationToken cancellationToken = default)
        {
            try
            {
                var ContactMessages = await _repository.
                    ContactMessageRepository.GetAllAsync(cancellationToken);
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
        [HttpGet("{id}", Name = "ContactMessagesById")]
        public async Task<IActionResult> GetContactMessagesById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var ContactMessages = await _repository.ContactMessageRepository
                    .GetByIdAsync(id, cancellationToken);
                if (ContactMessages == null)
                {
                    _logger.LogError($"ContactMessages with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned ContactMessages with id: {id}");

                    var ContactMessagesResult = _mapper.Map<ContactMessageGetDto>(ContactMessages);
                    return Ok(ContactMessagesResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetContactMessagesById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/ContactMessages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContactMessage
            (int id, [FromBody] ContactMessagePutDto ContactMessage, CancellationToken cancellationToken = default)
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

                var ContactMessageEntity = await _repository.ContactMessageRepository.GetByIdAsync(id, cancellationToken);
                if (ContactMessageEntity == null)
                {
                    _logger.LogError($"ContactMessage with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(ContactMessage, ContactMessageEntity);

                _repository.ContactMessageRepository.UpdateDetail(ContactMessageEntity);
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
        public IActionResult PostContactMessage([FromBody] ContactMessagePostDto ContactMessage, CancellationToken cancellationToken = default)
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

                _repository.ContactMessageRepository.CreateDetail(ContactMessageEntity);
                _repository.SaveAsync();

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
        public async Task<IActionResult> DeleteContactMessage(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var ContactMessage = await _repository.ContactMessageRepository.GetByIdAsync(id, cancellationToken);
                if (ContactMessage == null)
                {
                    _logger.LogError($"ContactMessage with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.ContactMessageRepository.DeleteDetail(ContactMessage);
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
