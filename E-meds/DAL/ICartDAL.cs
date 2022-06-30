using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ICartDAL<Cart>
    {
        bool SaveInCart(Cart cart);
        bool DeleteFromCart(int orderid);
        bool UpdateCart(Cart cart);
        Cart GetCartItem(int orderid);
        List<Cart> GetAllCartItems();
    }
}
