using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
namespace DAL
{
    public class ProductDAL : IProductsDAL<Product>
    {
        public bool SaveProduct(Product product)
        {
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    dbContext.Products.Add(product);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteProduct(int productCode)
        {
            try
            {
                using(CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    var product = dbContext.Products.Where(p => p.ProId == productCode).FirstOrDefault();
                    if (product != null)
                    {
                        dbContext.Products.Remove(product);
                        dbContext.SaveChanges() ;
                        return true;
                    }
                    else
                    {
                        return false ;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    var existProduct=dbContext.Products.Where(p=>p.ProId == product.ProId).FirstOrDefault();
                    if(existProduct != null)
                    {
                        existProduct.ProId = product.ProId;
                        existProduct.ProName = product.ProName;
                        existProduct.BrandName = product.BrandName;
                        existProduct.mrp=product.mrp;
                        existProduct.Category= product.Category;
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

        public Product GetProduct(int productCode)
        {
            var product = new Product();
            try
            {
                using(CapstoneDatabaseEntities dbContext=new CapstoneDatabaseEntities())
                {
                    product=dbContext.Products.Where(p=>p.ProId==productCode).FirstOrDefault();
                    if(product != null)
                    {
                        return product;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch(Exception ex)
            {
                return null ;
            }
        }

        public List<Product> GetAllProducts()
        {
            var productlist=new List<Product>();
            try
            {
               using(CapstoneDatabaseEntities dbContext=new CapstoneDatabaseEntities())
                {
                    productlist=dbContext.Products.ToList();
                    return productlist;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
}
