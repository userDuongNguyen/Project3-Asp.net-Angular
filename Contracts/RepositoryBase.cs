using Domain.RepositoryInterfaces;
using Domain.SeedWork;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ProjectInfrastructure.IRepository
{
    public abstract class RepositoryBase<T>(RepositoryContext repositoryContext) : IRepository<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; } = repositoryContext;

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public IQueryable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>()
                .Where(expression).AsNoTracking();
        }

        public virtual void Create(T entity) => this.RepositoryContext.Set<T>().Add(entity);

        public virtual void Update(T entity) => this.RepositoryContext.Set<T>().Update(entity);

        public virtual void Delete(T entity) => this.RepositoryContext.Set<T>().Remove(entity);
    }
}
