using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
namespace BLL
{
    public class OrderItemInfoBLL
    {
        OrderItemInfoDAL orderiteminfoDAL = new OrderItemInfoDAL();

        public bool AddCustOrder(OrderItemInfo order)
        {
            var result = orderiteminfoDAL.SaveOrderItemInfoC(order);
            return result;
        }
        public bool DeleteCustOrder(int orderid)
        {
            var result=orderiteminfoDAL.DeleteOrderItemInfoC(orderid);
            return result;
        }
        public bool EditCustOrder(OrderItemInfo order)
        {
            var result=orderiteminfoDAL.UpdateOrderItemInfoC(order);
            return result;
        }
        public OrderItemInfo GetCustOrderByCode(int code)
        {
            var custorder = orderiteminfoDAL.GetOrderItemInfoC(code);
            if (custorder != null)
            {
                return custorder;
            }
            else
            {
                return null;
            }
        }
        public List<OrderItemInfo> GetAllCustOrders()
        {
            var custorderlist=orderiteminfoDAL.GetAllOrderItemInfoC();
            return custorderlist;
        }
    }
}
