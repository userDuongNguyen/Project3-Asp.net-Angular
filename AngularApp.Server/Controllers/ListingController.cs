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
    public class ListingController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private readonly IRepositoryWrapper _repository = repository;
        private readonly ILoggerManager _logger = logger;
        private readonly IMapper _mapper = mapper;

        // GET: api/Listings
        [HttpGet]
        public async Task<IActionResult> GetAllListings(CancellationToken cancellationToken = default)
        {
            try
            {
                var Listings = await _repository.
                    ListingRepository.GetAllAsync(cancellationToken);
                _logger.LogInfo($"Returned all Listings from database.");

                var ListingsResult = _mapper.Map<IEnumerable<ListingGetDto>>(Listings);
                return Ok(ListingsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllListings action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Listings/5
        [HttpGet("{id}", Name = "ListingsById")]
        public async Task<IActionResult> GetListingsById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var Listings = await _repository.ListingRepository
                    .GetByIdAsync(id, cancellationToken);
                if (Listings == null)
                {
                    _logger.LogError($"Listings with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Listings with id: {id}");

                    var ListingsResult = _mapper.Map<ListingGetDto>(Listings);
                    return Ok(ListingsResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetListingsById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Listings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateListing
            (int id, [FromBody] ListingPutDto Listing, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Listing == null)
                {
                    _logger.LogError("Listing object sent from client is null.");
                    return BadRequest("Listing object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Listing object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var ListingEntity = await _repository.ListingRepository.GetByIdAsync(id, cancellationToken);
                if (ListingEntity == null)
                {
                    _logger.LogError($"Listing with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(Listing, ListingEntity);

                _repository.ListingRepository.UpdateDetail(ListingEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateListing action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Listings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostListing([FromBody] ListingPostDto Listing, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Listing is null)
                {
                    _logger.LogError("Listing object sent from client is null.");
                    return BadRequest("Listing object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Listing object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var ListingEntity = _mapper.Map<Listing>(Listing);

                _repository.ListingRepository.CreateDetail(ListingEntity);
                _repository.SaveAsync();

                var createdListing = _mapper.Map<ListingPostDto>(ListingEntity);

                return CreatedAtRoute("ListingById", new { id = createdListing.Id }, createdListing);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateListing action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        // DELETE: api/Listings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var Listing = await _repository.ListingRepository.GetByIdAsync(id, cancellationToken);
                if (Listing == null)
                {
                    _logger.LogError($"Listing with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.ListingRepository.DeleteDetail(Listing);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete Listing action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
