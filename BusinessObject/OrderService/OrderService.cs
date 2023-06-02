using Common.DTO.Order;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace BusinessObject.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;


        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<AddOrderResponse> CreateAsync(AddOrderRequest addOrderRequest)
        {
            try
            {
                var order = new Order
                {
                    MemberId = addOrderRequest.MemberId,
                    Freight = addOrderRequest.Freight,
                    OrderDate = addOrderRequest.OrderDate,
                    RequireDate = addOrderRequest.RequireDate,
                    ShippedDate = addOrderRequest.ShippedDate,
                };

                await _orderRepository.CreateAsync(order);
                _orderRepository.SaveChanges();

                foreach (var product in addOrderRequest.ProductId)
                {
                    var pro = await _productRepository.GetAsync(x => x.ProductId == product);
                    var orderDetail = new OrderDetail
                    {
                        Discount = addOrderRequest.Discount,
                        OrderId = order.OrderId,
                        ProductId = product,
                        Quantity = addOrderRequest.Quantity,
                        UnitPrice = pro.UnitPrice,
                    };
                    await _orderDetailRepository.CreateAsync(orderDetail);
                    _orderDetailRepository.SaveChanges();
                }

                return new AddOrderResponse
                {
                    IsSucced = true
                };
            }
            catch (Exception)
            {
                return new AddOrderResponse
                {
                    IsSucced = false,
                };
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var order = await _orderRepository.GetAsync(s => s.OrderId == id);
                if (order == null)
                {
                    return false;
                }
                _orderRepository.DeleteAsync(order);
                _orderRepository.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
            return await _orderRepository.GetAllWithOdata(x => true,x=>x.Member);
        }

        public async Task<OrderViewModel?> GetOrderByIdAsync(int id)
        {
            try
            {
                var result = await _orderRepository.GetAsync(c => c.OrderId == id);

                if (result == null)
                {
                    return null;
                }

                return new OrderViewModel
                {
                    OrderId = result.OrderId,
                    ShippedDate = result.ShippedDate,
                    Freight = result.Freight,
                    RequireDate = result.RequireDate,
                    OrderDate = result.OrderDate,
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<AddOrderResponse> UpdateAsync(int id, UpdateOrderRequest updateOrderRequest)
        {
            try
            {
                var order = await _orderRepository.GetAsync(s => s.OrderId == id);
                if (order == null)
                {
                    return new AddOrderResponse
                    {
                        IsSucced = false,
                    };
                }
                order.MemberId = updateOrderRequest.MemberId;
                order.RequireDate = updateOrderRequest.RequireDate;
                order.OrderDate = updateOrderRequest.OrderDate;
                order.ShippedDate = updateOrderRequest.ShippedDate;
                order.Freight = updateOrderRequest.Freight;

                await _orderRepository.UpdateAsync(order);
                _orderRepository.SaveChanges();

                return new AddOrderResponse
                {
                    IsSucced = true,
                };
            }
            catch (Exception)
            {
                return new AddOrderResponse
                {
                    IsSucced = false,
                };
            }
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailByIdAsync(int id)
        {
            return await _orderDetailRepository.GetAllWithOdata(x => x.OrderId == id,null);
        }
    }
}
