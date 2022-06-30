using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IOrderInfoDAL<OrderInfo>
    {
        bool SaveOrder(OrderInfo order);
        bool DeleteOrder(int orderId);
        bool UpdateOrder(OrderInfo order);
        OrderInfo GetOrder(int orderId);
        List<OrderInfo> GetAllOrders();
    }
}
