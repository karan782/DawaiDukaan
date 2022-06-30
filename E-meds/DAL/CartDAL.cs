using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
namespace DAL
{
    public class CartDAL : ICartDAL<Cart>
    {
        public bool SaveInCart(Cart cart)
        {
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    dbContext.Carts.Add(cart);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteFromCart(int orderid)
        {
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    var product = dbContext.Carts.Where(p => p.OrderId == orderid).FirstOrDefault();
                    if (product != null)
                    {
                        dbContext.Carts.Remove(product);
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
        public bool UpdateCart(Cart cart)
        {
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    var existProduct = dbContext.Carts.Where(p => p.OrderId == cart.OrderId).FirstOrDefault();
                    if (existProduct != null)
                    {
                        existProduct.custid = cart.custid;
                        existProduct.OrderId = cart.OrderId;
                        existProduct.ProId = cart.ProId;
                        existProduct.qty = cart.qty;
                        existProduct.mrp = cart.mrp;
                        existProduct.amount = cart.amount;
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
        public Cart GetCartItem(int orderid)
        {
            var cartitem = new Cart();
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    cartitem = dbContext.Carts.Where(p => p.OrderId == orderid).FirstOrDefault();
                    if (cartitem != null)
                    {
                        return cartitem;
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
        public List<Cart> GetAllCartItems()
        {
            var cartlist = new List<Cart>();
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    cartlist = dbContext.Carts.ToList();
                    return cartlist;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        
    }
}
