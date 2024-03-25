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
    public class ListingsController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private IRepositoryWrapper _repository = repository;
        private ILoggerManager _logger = logger;
        private IMapper _mapper = mapper;

        // GET: api/Listings
        [HttpGet]
        public async Task<IActionResult> GetAllListings()
        {
            try
            {
                var Listings = await _repository.
                    ListingRepository.GetAllListingsAsync();
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
        [HttpGet("{id}")]
        public IActionResult GetListingRepositoryById(Guid id)
        {
            try
            {
                var ListingRepository = _repository.ListingRepository.
                    GetListingByIdAsync(id);

                if (ListingRepository is null)
                {
                    _logger.LogError($"ListingRepository with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned ListingRepository with id: {id}");

                    var ListingRepositoryResult = _mapper.
                        Map<ListingGetDto>(ListingRepository);
                    return Ok(ListingRepositoryResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetListingRepositoryById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Listings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateListing(Guid id, [FromBody] ListingPutDto Listing)
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

                var ListingEntity = await _repository.ListingRepository.GetListingByIdAsync(id);
                if (ListingEntity == null)
                {
                    _logger.LogError($"Listing with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(Listing, ListingEntity);

                _repository.ListingRepository.UpdateListing(ListingEntity);
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
        public IActionResult PostListing([FromBody] ListingPostDto Listing)
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

                _repository.ListingRepository.CreateListing(ListingEntity);
                _repository.ListingRepository.Save();

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
        public async Task<IActionResult> DeleteListing(Guid id)
        {
            try
            {
                var Listing = await _repository.ListingRepository.GetListingByIdAsync(id);
                if (Listing == null)
                {
                    _logger.LogError($"Listing with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.ListingRepository.DeleteListing(Listing);
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
