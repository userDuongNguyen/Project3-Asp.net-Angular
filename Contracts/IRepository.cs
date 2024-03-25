using Domain.SeedWork;
using System.Linq.Expressions;

namespace Domain.RepositoryInterfaces
{
    public interface IRepository<T> : IRepositoryContract<T> where T : class
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
