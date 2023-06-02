using Common.DTO.Product;
using DataAccess.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace BusinessObject.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<AddProductResponse> CreateAsync(AddProductRequest addProductRequest);
        Task<AddProductResponse> UpdateAsync(int id, UpdateProductRequest updateProductRequest);
        Task<bool> DeleteAsync(int id);
        Task<ProductViewModel> GetProductByIdAsync(int id);
    }
}
