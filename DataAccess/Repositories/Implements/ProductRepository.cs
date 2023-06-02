using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implements
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(PRN231_AS1Context context) : base(context)
        {
            
        }

        public IEnumerable<Product> GetAllProductInclude()
        {
            var getAllProduct =  _context.Products.Include(x => x.Category);
            return getAllProduct;
        }
    }
}
