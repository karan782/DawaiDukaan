using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ICustomerInfoDAL<CustomerInfo>
    {
        bool SaveCustomer(CustomerInfo customerInfo);
        bool DeleteCustomer(int customerId);
        bool UpdateCustomer(CustomerInfo customerInfo);
        CustomerInfo GetCustomer(int customerId);
        List<CustomerInfo> GetAllCustomers();
    }
}
