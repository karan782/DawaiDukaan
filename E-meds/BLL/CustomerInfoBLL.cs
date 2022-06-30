using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
namespace BLL
{
    public class CustomerInfoBLL
    {
        CutomerInfoDAL customerInfoDAL = new CutomerInfoDAL();
        public bool AddCustomer(CustomerInfo customerInfo)
        {
            var result = customerInfoDAL.SaveCustomer(customerInfo);
            return result;
        }
        public bool RemoveCustomer(int custid)
        {
            var result = customerInfoDAL.DeleteCustomer(custid);
            return result;
        }
        public bool EditCustomer(CustomerInfo customerInfo)
        {
            var result = customerInfoDAL.UpdateCustomer(customerInfo);
            return result;
        }
        public CustomerInfo GetCustByCode(int custid)
        {
            var customer = customerInfoDAL.GetCustomer(custid);
            if (customer != null)
            {
                return customer;
            }
            else
            {
                return null;
            }
        }
        public List<CustomerInfo> GetAllCustomers()
        {
            var customers=customerInfoDAL.GetAllCustomers();
            return customers;
        }

    }
}
