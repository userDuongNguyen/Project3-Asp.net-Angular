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
    public class UserController
        (ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) : ControllerBase
    {
        private readonly IRepositoryWrapper _repository = repository;
        private readonly ILoggerManager _logger = logger;
        private readonly IMapper _mapper = mapper;

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken = default)
        {
            try
            {
                var Users = await _repository.
                    UserRepository.GetAllAsync(cancellationToken);
                _logger.LogInfo($"Returned all Users from database.");

                var UsersResult = _mapper.Map<IEnumerable<UserGetDto>>(Users);
                return Ok(UsersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUsers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "UsersById")]
        public async Task<IActionResult> GetUsersById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var Users = await _repository.UserRepository
                    .GetByIdAsync(id, cancellationToken);
                if (Users == null)
                {
                    _logger.LogError($"Users with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Users with id: {id}");

                    var UsersResult = _mapper.Map<UserGetDto>(Users);
                    return Ok(UsersResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUsersById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser
            (int id, [FromBody] UserPutDto User, CancellationToken cancellationToken = default)
        {
            try
            {
                if (User == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid User object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var UserEntity = await _repository.UserRepository.GetByIdAsync(id, cancellationToken);
                if (UserEntity == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(User, UserEntity);

                _repository.UserRepository.UpdateDetail(UserEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostUser([FromBody] UserPostDto User, CancellationToken cancellationToken = default)
        {
            try
            {
                if (User is null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid User object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var UserEntity = _mapper.Map<User>(User);

                _repository.UserRepository.CreateDetail(UserEntity);
                _repository.SaveAsync();

                var createdUser = _mapper.Map<UserPostDto>(UserEntity);

                return CreatedAtRoute("UserById", new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var User = await _repository.UserRepository.GetByIdAsync(id, cancellationToken);
                if (User == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.UserRepository.DeleteDetail(User);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete User action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
