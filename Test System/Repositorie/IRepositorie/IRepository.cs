using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Test_System.Repositorie.IRepositorie
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        void Delete(T entity);

        Task<IEnumerable<T>> GetAsync(
           Expression<Func<T, bool>>? exception = null,
           Expression<Func<T, object>>[]? includes = null,
           bool tracked = true,
           CancellationToken cancellationToken = default,
           CancellationToken CancellationToken = default);

        Task<T?> GetOneAsync(Expression<Func<T, bool>>? exception = null, Expression<Func<T, object>>[]? includes = null,
          bool tracked = true, CancellationToken cancellationToken = default, CancellationToken CancellationToken = default);

        Task commitAsync(CancellationToken cancellationToken);

    }
}
