using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        IEnumerable<Product> GetAllProductInclude();
    }
}
