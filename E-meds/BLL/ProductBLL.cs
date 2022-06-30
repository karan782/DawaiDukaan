using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
namespace BLL
{
    public class ProductBLL
    {
        ProductDAL productDAL= new ProductDAL();
        public bool AddProduct(Product product)
        {
            var result=productDAL.SaveProduct(product);
            return result;
        }
        public bool Removeproduct(int proid)
        {
            var result = productDAL.DeleteProduct(proid);
            return result;
        }
        public bool Editproduct(Product product)
        {
            var result = productDAL.UpdateProduct(product);
            return result;
        }
        public Product GetProductByCode(int proid)
        {
            var product = productDAL.GetProduct(proid);
            if (product != null)
            {
                return product;
            }
            else
            {
                return null;
            }
        }
        public List<Product> GetAllProducts()
        {
            var products = productDAL.GetAllProducts();
            return products;
        }
    }
}
