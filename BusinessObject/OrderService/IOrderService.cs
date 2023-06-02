using Common.DTO.Order;
using Common.DTO.Product;
using DataAccess.Models;

namespace BusinessObject.OrderService
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrderAsync();
        Task<AddOrderResponse> CreateAsync(AddOrderRequest addOrderRequest);
        Task<AddOrderResponse> UpdateAsync(int id, UpdateOrderRequest updateOrderRequest);
        Task<bool> DeleteAsync(int id);
        Task<OrderViewModel> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderDetail>> GetOrderDetailByIdAsync(int id);

    }
}
