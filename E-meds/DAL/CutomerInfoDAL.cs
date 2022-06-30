using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
namespace DAL
{
    public class CutomerInfoDAL : ICustomerInfoDAL<CustomerInfo>
    {
        public bool SaveCustomer(CustomerInfo customerInfo)
        {
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    dbContext.CustomerInfoes.Add(customerInfo);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public bool DeleteCustomer(int customerId)
        {
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    var customer = dbContext.CustomerInfoes.Where(p => p.CustId == customerId).FirstOrDefault();
                    if (customer != null)
                    {
                        dbContext.CustomerInfoes.Remove(customer);
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

        public bool UpdateCustomer(CustomerInfo customerInfo)
        {
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    var existCustomer = dbContext.CustomerInfoes.Where(p => p.CustId == customerInfo.CustId).FirstOrDefault();
                    if (existCustomer != null)
                    {
                        existCustomer.CustId = customerInfo.CustId;
                        existCustomer.CustName = customerInfo.CustName;
                        existCustomer.Email = customerInfo.Email;
                        existCustomer.PinCode = customerInfo.PinCode;
                        existCustomer.Country = customerInfo.Country;
                        existCustomer.Contact = customerInfo.Contact;
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

        public CustomerInfo GetCustomer(int customerId)
        {
            var customer = new CustomerInfo();
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    customer = dbContext.CustomerInfoes.Where(p => p.CustId == customerId).FirstOrDefault();
                    if (customer != null)
                    {
                        return customer;
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


        public List<CustomerInfo> GetAllCustomers()
        {
            var customerlist = new List<CustomerInfo>();
            try
            {
                using (CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities())
                {
                    customerlist = dbContext.CustomerInfoes.ToList();
                    return customerlist;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
