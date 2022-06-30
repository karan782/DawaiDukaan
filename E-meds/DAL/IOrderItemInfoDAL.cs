using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IOrderItemInfoDAL<OrderItemInfo>
    {
        bool SaveOrderItemInfoC(OrderItemInfo orderItemInfo);
        bool DeleteOrderItemInfoC(int orderid);
        bool UpdateOrderItemInfoC(OrderItemInfo orderItemInfo);
        OrderItemInfo GetOrderItemInfoC(int orderid);
        List<OrderItemInfo> GetAllOrderItemInfoC();
    }
}
