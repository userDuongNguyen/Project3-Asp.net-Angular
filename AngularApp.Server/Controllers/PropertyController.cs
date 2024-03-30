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
    public class PropertyController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private readonly IRepositoryWrapper _repository = repository;
        private readonly ILoggerManager _logger = logger;
        private readonly IMapper _mapper = mapper;

        // GET: api/Propertys
        [HttpGet]
        public async Task<IActionResult> GetAllPropertys(CancellationToken cancellationToken = default)
        {
            try
            {
                var Propertys = await _repository.
                    PropertyRepository.GetAllAsync(cancellationToken);
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
        [HttpGet("{id}", Name = "PropertysById")]
        public async Task<IActionResult> GetPropertysById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var Propertys = await _repository.PropertyRepository
                    .GetByIdAsync(id, cancellationToken);
                if (Propertys == null)
                {
                    _logger.LogError($"Propertys with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Propertys with id: {id}");

                    var PropertysResult = _mapper.Map<PropertyGetDto>(Propertys);
                    return Ok(PropertysResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPropertysById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Propertys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty
            (int id, [FromBody] PropertyPutDto Property, CancellationToken cancellationToken = default)
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

                var PropertyEntity = await _repository.PropertyRepository.GetByIdAsync(id, cancellationToken);
                if (PropertyEntity == null)
                {
                    _logger.LogError($"Property with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(Property, PropertyEntity);

                _repository.PropertyRepository.UpdateDetail(PropertyEntity);
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
        public IActionResult PostProperty([FromBody] PropertyPostDto Property, CancellationToken cancellationToken = default)
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

                _repository.PropertyRepository.CreateDetail(PropertyEntity);
                _repository.SaveAsync();

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
        public async Task<IActionResult> DeleteProperty(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var Property = await _repository.PropertyRepository.GetByIdAsync(id, cancellationToken);
                if (Property == null)
                {
                    _logger.LogError($"Property with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.PropertyRepository.DeleteDetail(Property);
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
