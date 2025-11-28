using System.Threading.Tasks;
using Test_System.Data_Acssess;
using Test_System.Models;
using Test_System.Repositories;
namespace Test_System.Repositorie
{

    public class ProductRepository : CategoryRepository<Product>
    {
        private ApplicationDBContext _db = new();

        public async Task AddRenge(IEnumerable<Product> products , CancellationToken cancellationToken = default)
        {
             await _db.Products.AddRangeAsync(products , cancellationToken);
        }

        internal void Delete(IEnumerable<Product> product)
        {
            throw new NotImplementedException();
        }

        internal async Task<IEnumerable<Product>> GetAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        internal async Task GetAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}