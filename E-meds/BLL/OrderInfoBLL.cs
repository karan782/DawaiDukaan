using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
namespace BLL
{
    public class OrderInfoBLL
    {
        OrderInfoDAL orderInfoDAL = new OrderInfoDAL();
        public bool AddOrderInfo(OrderInfo orderInfo)
        {
            var result = orderInfoDAL.SaveOrder(orderInfo);
            return result;
        }
        public bool RemoveOrderInfo(int orderid)
        {
            var result = orderInfoDAL.DeleteOrder(orderid);
            return result;
        }
        public bool EditOrderInfo(OrderInfo orderInfo)
        {
            var result = orderInfoDAL.UpdateOrder(orderInfo);
            return result;
        }
        public OrderInfo GetOrderInfoByCode(int orderid)
        {
            var order = orderInfoDAL.GetOrder(orderid);
            if (order != null)
            {
                return order;
            }
            else
            {
                return null;
            }
        }
        public List<OrderInfo> GetAllOrderInfos()
        {
            var orders = orderInfoDAL.GetAllOrders();
            return orders;
        }
    }
}
