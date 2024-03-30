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
    public class WalletController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private readonly IRepositoryWrapper _repository = repository;
        private readonly ILoggerManager _logger = logger;
        private readonly IMapper _mapper = mapper;

        // GET: api/Wallets
        [HttpGet]
        public async Task<IActionResult> GetAllWallets(CancellationToken cancellationToken = default)
        {
            try
            {
                var Wallets = await _repository.
                    WalletRepository.GetAllAsync(cancellationToken);
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
        [HttpGet("{id}", Name = "WalletsById")]
        public async Task<IActionResult> GetWalletsById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var Wallets = await _repository.WalletRepository
                    .GetByIdAsync(id, cancellationToken);
                if (Wallets == null)
                {
                    _logger.LogError($"Wallets with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Wallets with id: {id}");

                    var WalletsResult = _mapper.Map<WalletGetDto>(Wallets);
                    return Ok(WalletsResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetWalletsById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Wallets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWallet
            (int id, [FromBody] WalletPutDto Wallet, CancellationToken cancellationToken = default)
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

                var WalletEntity = await _repository.WalletRepository.GetByIdAsync(id, cancellationToken);
                if (WalletEntity == null)
                {
                    _logger.LogError($"Wallet with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(Wallet, WalletEntity);

                _repository.WalletRepository.UpdateDetail(WalletEntity);
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
        public IActionResult PostWallet([FromBody] WalletPostDto Wallet, CancellationToken cancellationToken = default)
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

                _repository.WalletRepository.CreateDetail(WalletEntity);
                _repository.SaveAsync();

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
        public async Task<IActionResult> DeleteWallet(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var Wallet = await _repository.WalletRepository.GetByIdAsync(id, cancellationToken);
                if (Wallet == null)
                {
                    _logger.LogError($"Wallet with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.WalletRepository.DeleteDetail(Wallet);
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
