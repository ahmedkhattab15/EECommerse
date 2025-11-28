using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Test_System.Data_Acssess;
using Test_System.Models;
namespace Test_System.Repositories
{
    public class CategoryRepository<T> where T : class
    {
        private ApplicationDBContext _db = new();
        private DbSet<T> _dbSet;

        public CategoryRepository()
        {
            _dbSet = _db.Set<T>();
        }


        // CancellationToken ? 
        // توقف تنفيذها لو السيستم طلب كده  Async هو أداة بتخلّي الميثود الـ
        // يعني لو فيه عملية طويلة زي قراءة من قاعدة البيانات، رفع ملف
        // اتلغي request والمستخدم قفل الصفحة أو الـ
        //  علشان نوقف العملية CancellationToken بنستخدم

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        // Read 
        //  ممكن اكون ب قرأ اكتر من ريكورد
        public async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? exception = null,
            Expression<Func<T, object>>[]? includes = null,
            bool tracked = true,
            CancellationToken cancellationToken = default,
            CancellationToken CancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (exception is not null)
            {
                query = query.Where(exception);
            }

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync(cancellationToken);
        }

        //  ممكن اكون ب قرأ ريكورد واحد بس 
        public async Task<T?> GetOneAsync(Expression<Func<T, bool>>? exception = null, Expression<Func<T, object>>[]? includes = null,
           bool tracked = true, CancellationToken cancellationToken = default, CancellationToken CancellationToken = default)
        {
            return (await GetAsync(exception, includes, tracked, cancellationToken)).FirstOrDefault();
        }
        public async Task commitAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _db.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal async Task AddAsync(Brand brand, object cancellationToken)
        {
            throw new NotImplementedException();
        }

        internal async Task<Brand> GetOneAsync(Func<Brand, bool> value, object CancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
