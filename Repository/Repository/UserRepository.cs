using Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProjectInfrastructure.IRepository;

namespace Repository.Repository
{
    public class UserRepository(RepositoryContext repositoryContext) : RepositoryBase<User>(repositoryContext), IUserRepository, IDisposable
    {
#nullable disable

        private readonly RepositoryContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {

            return await FindAll().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<User> GetUserByIdAsync(Guid UserId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        User => User.Id.Equals(UserId)
                        )
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<User> GetUserWithDetailsAsync(Guid UserId)
        {
            return await FindByCondition(
                User => User.Id.Equals(UserId)
                )

                .FirstOrDefaultAsync();
        }

        public void CreateUser(User User)
        {
            Create(User);
        }

        public void UpdateUser(User User)
        {
            Update(User);
        }

        public void DeleteUser(User User)
        {
            Delete(User);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

