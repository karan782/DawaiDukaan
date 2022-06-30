using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IProductsDAL<Product>
    {
        bool SaveProduct(Product product);
        bool DeleteProduct(int productCode);
        bool UpdateProduct(Product product);
        Product GetProduct(int productCode);
        List<Product> GetAllProducts();
    }
}
