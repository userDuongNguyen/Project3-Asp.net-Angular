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
    public class PropertysController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private readonly IRepositoryWrapper _repository = repository;
        private readonly ILoggerManager _logger = logger;
        private readonly IMapper _mapper = mapper;

        // GET: api/Propertys
        [HttpGet]
        public async Task<IActionResult> GetAllPropertys()
        {
            try
            {
                var Propertys = await _repository.
                    PropertyRepository.GetAllPropertysAsync();
                _logger.LogInfo($"Returned all Propertys from database.");

                var PropertysResult = _mapper.Map<IEnumerable<PropertyGetDto>>(Propertys);
                return Ok(PropertysResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllPropertys action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Propertys/5
        [HttpGet("{id}")]
        public IActionResult GetPropertyRepositoryById(Guid id)
        {
            try
            {
                var PropertyRepository = _repository.PropertyRepository.
                    GetPropertyByIdAsync(id);

                if (PropertyRepository is null)
                {
                    _logger.LogError($"PropertyRepository with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned PropertyRepository with id: {id}");

                    var PropertyRepositoryResult = _mapper.
                        Map<PropertyGetDto>(PropertyRepository);
                    return Ok(PropertyRepositoryResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPropertyRepositoryById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Propertys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(Guid id, [FromBody] PropertyPutDto Property)
        {
            try
            {
                if (Property == null)
                {
                    _logger.LogError("Property object sent from client is null.");
                    return BadRequest("Property object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Property object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var PropertyEntity = await _repository.PropertyRepository.GetPropertyByIdAsync(id);
                if (PropertyEntity == null)
                {
                    _logger.LogError($"Property with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(Property, PropertyEntity);

                _repository.PropertyRepository.UpdateProperty(PropertyEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProperty action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Propertys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostProperty([FromBody] PropertyPostDto Property)
        {
            try
            {
                if (Property is null)
                {
                    _logger.LogError("Property object sent from client is null.");
                    return BadRequest("Property object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Property object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var PropertyEntity = _mapper.Map<Property>(Property);

                _repository.PropertyRepository.CreateProperty(PropertyEntity);
                _repository.PropertyRepository.Save();

                var createdProperty = _mapper.Map<PropertyPostDto>(PropertyEntity);

                return CreatedAtRoute("PropertyById", new { id = createdProperty.Id }, createdProperty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateProperty action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        // DELETE: api/Propertys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            try
            {
                var Property = await _repository.PropertyRepository.GetPropertyByIdAsync(id);
                if (Property == null)
                {
                    _logger.LogError($"Property with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.PropertyRepository.DeleteProperty(Property);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete Property action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
