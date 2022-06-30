using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Models;
using BLL;
using System.Data.SqlClient;
using AppUILayer.Models;
namespace AppUILayer.Controllers

{
    public class CommonController : Controller
    {
        // GET: Common
        
        CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities();
        ProductBLL productBLL = new ProductBLL();
        OrderInfoBLL orderInfoBLL = new OrderInfoBLL();
        CustomerInfoBLL customerInfoBLL = new CustomerInfoBLL();
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ProductList()
        {
            try
            {
                var inventory = productBLL.GetAllProducts();

                return View(inventory);
            }
            catch (SqlException ex)
            {
                return null;
            }
        }
        public ActionResult ProductsCard()
        {
            return View();
        }
        public ActionResult ProductsByCategory(string category)
        {
            var products = dbContext.getproductsbycategory(category).ToList();
            return View(products);
        }
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateAdmin(Credentials credentials)
        {
            if (credentials.EmailId == "admin@gmail.com" && credentials.Password == "admin123")
            {
                Session["Email"] = credentials.EmailId;
                TempData["AlertMessage"] = "Logged in successfully..!";
                return RedirectToRoute(new
                {
                    controller = "Admin",
                    action = "ManageInventory",
                });
            }
            else
            {
                ViewBag.error = "Incorrect Email ID or Password";
                return View("AdminLogin");
            }
        }

        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateUser(Credentials credentials)
        {
            try
            {
                var dr = dbContext.validateuser(credentials.EmailId,credentials.Password).FirstOrDefault();
                if (dr!=null)
                {
                    TempData["AlertMessage"] = "Logged in successfully..!";
                    Session["Email"] = credentials.EmailId;
                    return RedirectToRoute(new
                    {
                        controller = "User",
                        action = "UserProfile"
                    });
                }
                else
                {
                    ViewBag.error = "Incorrect Email ID or Password";
                    return View("UserLogin");
                }
            }
            catch (SqlException ex)
            {
                return View("UserLogin");
            }
           
        }

        [HttpGet]
        public ActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewUser(CustomerInfo customerInfo)
        {
            if (dbContext.CustomerInfoes.Any(x => x.Email == customerInfo.Email))
            {
                TempData["WarningMessage"] = "Email already exists";
                return View("NewUser", customerInfo);
            }
            else
            {
                var result = customerInfoBLL.AddCustomer(customerInfo);
                if (result)
                {
                    Session["Email"] = customerInfo.Email;
                    TempData["AlertMessage"] = "Profile Created successfully..! Logged In..!";
                    return RedirectToRoute(new
                    {
                        controller = "User",
                        action = "UserProfile"
                    });
                }
                return View("NewUser", customerInfo);
            }
        }
    }
}

