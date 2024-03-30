
using Domain.Contracts;
using Domain.SeedWork;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public abstract class RepositoryBase<T>(DataContext Context) : IRepositoryBase<T> where T : class
    {
        protected DataContext Data { get; set; } = Context;

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public IQueryable<T> FindAll()
        {
            return Data.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return Data.Set<T>()
                .Where(expression).AsNoTracking();
        }

        public virtual void Create(T entity) => Data.Set<T>().Add(entity);

        public virtual void Update(T entity) => Data.Set<T>().Update(entity);

        public virtual void Delete(T entity) => Data.Set<T>().Remove(entity);
    }
}
