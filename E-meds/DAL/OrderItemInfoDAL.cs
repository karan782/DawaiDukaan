using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderItemInfoDAL : IOrderItemInfoDAL<OrderItemInfo>
    {
        public bool SaveOrderItemInfoC(OrderItemInfo orderItemInfo)
        {
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    dbContext.OrderItemInfoes.Add(orderItemInfo);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteOrderItemInfoC(int orderid)
        {
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    var orderc = dbContext.OrderItemInfoes.Where(p => p.OrderId == orderid).FirstOrDefault();
                    if (orderc != null)
                    {
                        dbContext.OrderItemInfoes.Remove(orderc);
                        dbContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateOrderItemInfoC(OrderItemInfo orderItemInfo)
        {
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    var existOrder = dbContext.OrderItemInfoes.Where(p => p.OrderId == orderItemInfo.OrderId).FirstOrDefault();
                    if (existOrder != null)
                    {
                        existOrder.custid = orderItemInfo.custid;
                        existOrder.OrderId = orderItemInfo.OrderId;
                        existOrder.ProId = orderItemInfo.ProId;
                        existOrder.qty = orderItemInfo.qty;
                        existOrder.mrp = orderItemInfo.mrp;
                        existOrder.amount = orderItemInfo.amount;
                        dbContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public OrderItemInfo GetOrderItemInfoC(int orderid)
        {
            var order = new OrderItemInfo();
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    order = dbContext.OrderItemInfoes.Where(p => p.OrderId == orderid).FirstOrDefault();
                    if (order != null)
                    {
                        return order;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<OrderItemInfo> GetAllOrderItemInfoC()
        {
            var orderlist = new List<OrderItemInfo>();
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    orderlist = dbContext.OrderItemInfoes.ToList();
                    return orderlist;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
