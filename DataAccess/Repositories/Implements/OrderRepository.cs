using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implements
{
    public class OrderRepository : BaseRepository<Order> , IOrderRepository
    {
        public OrderRepository(PRN231_AS1Context context) : base(context)
        {
        }
    }
}
