using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using BusinessObject.OrderService;
using Common.DTO.Member;
using Common.DTO.Order;

namespace eStoreAPI.Controllers
{
    [Route("api/order-management")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var result = await _orderService.GetAllOrderAsync() ;

            if (result == null) return StatusCode(500);

            return Ok(result);
        }

        [HttpGet("orders/{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPut("orders/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderRequest updateOrderRequest)
        {
            var result = await _orderService.UpdateAsync(id, updateOrderRequest);

            if (result == null) return StatusCode(500);

            return Ok(result);
        }

        [HttpPost("orders")]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] AddOrderRequest addOrderRequest)
        {
            var result = await _orderService.CreateAsync(addOrderRequest);

            if (result == null) return StatusCode(500);

            return Ok(result);
        }

        [HttpDelete("orders/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
