using Domain.Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository
        (DataContext repositoryContext) : RepositoryBase<User>(repositoryContext),
    IUserRepository, IDisposable
    {
#nullable disable

        private readonly DataContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<User>> GetAllAsync
            (CancellationToken cancellationToken = default)
        {

            return await FindAll().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<User> GetByIdAsync(int UserId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        User => User.Id.Equals(UserId))
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<User> GetWithDetailsAsync(int UserId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(
                User => User.Id.Equals(UserId))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public void CreateDetail(User User)
        {
            Create(User);
        }

        public void UpdateDetail(User User)
        {
            Update(User);
        }

        public void DeleteDetail(User User)
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

