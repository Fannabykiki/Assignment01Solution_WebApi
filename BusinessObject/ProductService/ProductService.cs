using Common.DTO.Product;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using static System.Reflection.Metadata.BlobBuilder;

namespace BusinessObject.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<AddProductResponse> CreateAsync(AddProductRequest addProductRequest)
        {
            try
            {
                var product = new Product
                {
                    CategoryId = addProductRequest.CategoryId,
                    ProductName = addProductRequest.ProductName,
                    UnitPrice = addProductRequest.UnitPrice,
                    Weight = addProductRequest.Weight,
                    UnitsInStock = addProductRequest.UnitsInStock
                };
                 await _productRepository.CreateAsync(product);
                _productRepository.SaveChanges();

                return new AddProductResponse
                {
                    IsSucced = true
                };
            }
            catch (Exception)
            {
                return new AddProductResponse
                {
                    IsSucced = false,
                };
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetAsync(s => s.ProductId == id);
                if (product == null)
                {
                    return false;
                }
                _productRepository.DeleteAsync(product);
                _productRepository.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _productRepository.GetAllWithOdata(x => true,x => x.Category);
        }

        public async Task<ProductViewModel> GetProductByIdAsync(int id)
        {
            try
            {
                var result = await _productRepository.GetAsync(c => c.ProductId == id);

                if (result == null)
                {
                    return null;
                }

                return new ProductViewModel
                {
                   ProductId = result.ProductId,
                   ProductName = result.ProductName,
                   UnitsInStock = result.UnitsInStock,
                   Weight = result.Weight,
                   CategoryId = result.CategoryId,
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<AddProductResponse> UpdateAsync(int id, UpdateProductRequest updateProductRequest)
        {
            try
            {
                var product = await _productRepository.GetAsync(s => s.ProductId == id);
                if (product == null)
                {
                    return new AddProductResponse
                    {
                        IsSucced = false,
                    };
                }

                product.ProductName = updateProductRequest.ProductName;
                product.UnitPrice = updateProductRequest.UnitPrice;
                product.UnitsInStock = updateProductRequest.UnitsInStock;
                product.Weight = updateProductRequest.Weight;
                product.CategoryId = updateProductRequest.CategoryId;

                 await _productRepository.UpdateAsync(product);
                _productRepository.SaveChanges();

                return new AddProductResponse
                {
                    IsSucced = true,
                };
            }
            catch (Exception)
            {
                return new AddProductResponse
                {
                    IsSucced = false,
                };
            }
        }
    }
}
