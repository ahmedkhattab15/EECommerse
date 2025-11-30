using System.Threading.Tasks;
using Test_System.Data_Acssess;
using Test_System.Models;
using Test_System.Repositorie.IRepositorie;
using Test_System.Repositories;
using System.Linq.Expressions;

namespace Test_System.Repositorie
{
    public class ProductRepository : Repository<Product>, IRepository<Product>
    {
        private ApplicationDBContext _db = new();

        public async Task AddRenge(IEnumerable<Product> products , CancellationToken cancellationToken = default)
        {
             await _db.Products.AddRangeAsync(products , cancellationToken);
        }

        internal void commit()
        {
            throw new NotImplementedException();
        }

        internal void Delete(IEnumerable<Product> product)
        {
            throw new NotImplementedException();
        }
    }
}