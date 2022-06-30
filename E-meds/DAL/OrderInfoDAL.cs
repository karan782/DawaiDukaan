using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL
{
    public class OrderInfoDAL : IOrderInfoDAL<OrderInfo>
    {
        public bool SaveOrder(OrderInfo order)
        {
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    dbContext.OrderInfoes.Add(order);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteOrder(int orderId)
        {
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    var order = dbContext.OrderInfoes.Where(p => p.OrderId == orderId).FirstOrDefault();
                    if (order != null)
                    {
                        dbContext.OrderInfoes.Remove(order);
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

        public bool UpdateOrder(OrderInfo order)
        {
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    var existOrder = dbContext.OrderInfoes.Where(p => p.OrderId == order.OrderId).FirstOrDefault();
                    if (existOrder != null)
                    {
                        existOrder.OrderId = order.OrderId;
                        existOrder.OrderDate = order.OrderDate;
                        existOrder.ShippDate = order.ShippDate;
                        existOrder.custid = order.custid;
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

        public OrderInfo GetOrder(int orderId)
        {
            var order = new OrderInfo();
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    order = dbContext.OrderInfoes.Where(p => p.OrderId == orderId).FirstOrDefault();
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

        public List<OrderInfo> GetAllOrders()
        {
            var orderlist = new List<OrderInfo>();
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    orderlist = dbContext.OrderInfoes.ToList();
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
