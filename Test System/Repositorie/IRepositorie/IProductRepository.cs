using Test_System.Models;
namespace Test_System.Repositorie.IRepositorie
{
    public interface IProductRepository : IRepository<Product>
    {
        Task AddRenge(IEnumerable<Product> products, CancellationToken cancellationToken = default);

    }
}
