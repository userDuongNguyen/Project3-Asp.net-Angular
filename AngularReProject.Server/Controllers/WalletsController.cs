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
    public class WalletsController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private IRepositoryWrapper _repository = repository;
        private ILoggerManager _logger = logger;
        private IMapper _mapper = mapper;

        // GET: api/Wallets
        [HttpGet]
        public async Task<IActionResult> GetAllWallets()
        {
            try
            {
                var Wallets = await _repository.
                    WalletRepository.GetAllWalletsAsync();
                _logger.LogInfo($"Returned all Wallets from database.");

                var WalletsResult = _mapper.Map<IEnumerable<WalletGetDto>>(Wallets);
                return Ok(WalletsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllWallets action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Wallets/5
        [HttpGet("{id}")]
        public IActionResult GetWalletRepositoryById(Guid id)
        {
            try
            {
                var WalletRepository = _repository.WalletRepository.
                    GetWalletByIdAsync(id);

                if (WalletRepository is null)
                {
                    _logger.LogError($"WalletRepository with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned WalletRepository with id: {id}");

                    var WalletRepositoryResult = _mapper.
                        Map<WalletGetDto>(WalletRepository);
                    return Ok(WalletRepositoryResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetWalletRepositoryById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Wallets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWallet(Guid id, [FromBody] WalletPutDto Wallet)
        {
            try
            {
                if (Wallet == null)
                {
                    _logger.LogError("Wallet object sent from client is null.");
                    return BadRequest("Wallet object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Wallet object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var WalletEntity = await _repository.WalletRepository.GetWalletByIdAsync(id);
                if (WalletEntity == null)
                {
                    _logger.LogError($"Wallet with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(Wallet, WalletEntity);

                _repository.WalletRepository.UpdateWallet(WalletEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateWallet action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Wallets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostWallet([FromBody] WalletPostDto Wallet)
        {
            try
            {
                if (Wallet is null)
                {
                    _logger.LogError("Wallet object sent from client is null.");
                    return BadRequest("Wallet object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Wallet object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var WalletEntity = _mapper.Map<Wallet>(Wallet);

                _repository.WalletRepository.CreateWallet(WalletEntity);
                _repository.WalletRepository.Save();

                var createdWallet = _mapper.Map<WalletPostDto>(WalletEntity);

                return CreatedAtRoute("WalletById", new { id = createdWallet.Id }, createdWallet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateWallet action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        // DELETE: api/Wallets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallet(Guid id)
        {
            try
            {
                var Wallet = await _repository.WalletRepository.GetWalletByIdAsync(id);
                if (Wallet == null)
                {
                    _logger.LogError($"Wallet with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.WalletRepository.DeleteWallet(Wallet);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete Wallet action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
