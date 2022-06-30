using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
namespace BLL
{
    public class CartBLL
    {
        CartDAL cartDAL = new CartDAL();
        public bool AddtoCart(Cart item)
        {
            var result = cartDAL.SaveInCart(item);
            return result;
        }
        public bool RemovefromCart(int orderid)
        {
            var result = cartDAL.DeleteFromCart(orderid);
            return result;
        }
        public bool Editproduct(Cart item)
        {
            var result = cartDAL.UpdateCart(item);
            return result;
        }
        public Cart GetProductByCode(int orderid)
        {
            var product = cartDAL.GetCartItem(orderid);
            if (product != null)
            {
                return product;
            }
            else
            {
                return null;
            }
        }
        public List<Cart> GetAllItems()
        {
            var products = cartDAL.GetAllCartItems();
            return products;
        }
    }
}
