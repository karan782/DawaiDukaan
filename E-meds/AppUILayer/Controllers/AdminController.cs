using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Models;
using BLL;
namespace AppUILayer.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities();
        ProductBLL productBLL = new ProductBLL();
        OrderInfoBLL orderInfoBLL = new OrderInfoBLL();
        OrderItemInfoBLL orderItemInfoBLL = new OrderItemInfoBLL();
        CustomerInfoBLL customerInfoBLL = new CustomerInfoBLL();

        public ActionResult OrderLog()
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "AdminLogin" }); }
            var AllOrders = orderInfoBLL.GetAllOrderInfos();
            return View(AllOrders);
        }
        public ActionResult ViewOrderDetailsById(int id)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "AdminLogin" }); }
            var order = orderItemInfoBLL.GetCustOrderByCode(id);
            return View(order);
        }
        public ActionResult CustomerDetails()
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "AdminLogin" }); }
            var AllCustomers = customerInfoBLL.GetAllCustomers();
            return View(AllCustomers);
        }
        public ActionResult ManageInventory()
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "AdminLogin" }); }
            var AllOrders = productBLL.GetAllProducts();
            return View(AllOrders);
        }
        public ActionResult AddProduct()
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "AdminLogin" }); }
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "AdminLogin" }); }
            if (dbContext.Products.Any(x => x.ProId == product.ProId))
            {
                TempData["WarningMessage"] = "Product ID already exists";
                return View("AddProduct", product);
            }
            else
            {
                var result = productBLL.AddProduct(product);
                if (result)
                {
                    TempData["AlertMessage"] = "Product Added Successfully..!";
                    return RedirectToAction("ManageInventory");
                }
                return View("AddProduct", product);
            }
        }
        public ActionResult GetOrderDetail(int id)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "AdminLogin" }); }
            var AllOrders = dbContext.getorderiteminfobycode(id).ToList();
            return View(AllOrders);
        }

        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "AdminLogin" }); }
            var order = productBLL.GetProductByCode(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult DeleteProduct(Product product)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "AdminLogin" }); }
            var result = productBLL.Removeproduct(product.ProId);
            if (result)
            {
                TempData["AlertMessage"] = "Product Deleted Successfully..!";
                return RedirectToAction("ManageInventory");
            }
            else
            {
                TempData["AlertMessage"] = "Error..!";
                return View("ManageInventory");
            }
        }

        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "AdminLogin" }); }
            var order = productBLL.GetProductByCode(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product product)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "AdminLogin" }); }
            var result = productBLL.Editproduct(product);
            if (result)
            {
                TempData["AlertMessage"] = "Product Updated Successfully..!";
                return RedirectToAction("ManageInventory");
            }
            else
            {
                TempData["AlertMessage"] = "Error..!";
                return View("ManageInventory");
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            TempData["AlertMessage"] = "Logged Out..!";
            return RedirectToRoute(new
            {
                controller = "Common",
                action = "Home"
            });
        }

        [NonAction]
        public bool Authenticate()
        {
            if (Convert.ToString(Session["Email"]) == "admin@gmail.com")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}