using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Models;
using BLL;
using AppUILayer.Models;
namespace AppUILayer.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        CapstoneDatabaseEntities dbContext = new CapstoneDatabaseEntities();
        ProductBLL productBLL = new ProductBLL();
        OrderItemInfoBLL orderItemInfoBLL = new OrderItemInfoBLL();
        OrderInfoBLL orderInfoBLL = new OrderInfoBLL();
        CustomerInfoBLL customerInfoBLL = new CustomerInfoBLL();
        CartBLL cartBLL = new CartBLL();
        public ActionResult UserProfile()
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            string email = Convert.ToString(Session["Email"]);
            var customer = dbContext.getcustomerbyemail(email).FirstOrDefault();
            return View(customer);
        }
        public ActionResult DeleteUser(int id)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            var customer = customerInfoBLL.GetCustByCode(id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult DeleteUser(CustomerInfo customer)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            var result = customerInfoBLL.RemoveCustomer(customer.CustId);
            if (result)
            {
                TempData["AlertMessage"] = "User Deleted Successfully..!";
                Session.Abandon();
                return RedirectToRoute(new
                {
                    controller = "Common",
                    action = "Home"
                });
            }
            else
            {
                TempData["AlertMessage"] = "Error..!";
                return View("UserProfile");
            }
        }
        public ActionResult UpdateUser(int id)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            var customer = customerInfoBLL.GetCustByCode(id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult UpdateUser(CustomerInfo customer)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            if (dbContext.CustomerInfoes.Any(x => x.Email == customer.Email))
            {
                TempData["WarningMessage"] = "Email already exists";
                return View("UpdateUser", customer);
            }
            else
            {
                var result = customerInfoBLL.EditCustomer(customer);
                if (result)
                {
                    TempData["AlertMessage"] = "User Details Updated Successfully..!";
                    Session["Email"] = customer.Email;
                    return RedirectToAction("UserProfile");
                }
                return View("UserProfile");
            }
        }
        
        public ActionResult ProductsCard()
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            return View();
        }

        public ActionResult ProductsByCategory(string category)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            var products = dbContext.getproductsbycategory(category).ToList();
            return View(products);
        }
        public ActionResult Inventory()
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            var AllProducts = productBLL.GetAllProducts();
            return View(AllProducts);
        }
        public ActionResult AllOrders()
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }

            string email = Convert.ToString(Session["Email"]);
            var cust = dbContext.getcustomerbyemail(email).FirstOrDefault();
            var AllOrders = dbContext.getorderiteminfobycode(cust.CustId).ToList();
            return View(AllOrders);
        }
        public ActionResult AddToCart(int id)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            Cart cartitem = new Cart();
            string email = Convert.ToString(Session["Email"]);
            var customer = dbContext.getcustomerbyemail(email).FirstOrDefault();
            var order = productBLL.GetProductByCode(id);
            cartitem.ProId = order.ProId;
            cartitem.mrp = order.mrp;
            cartitem.qty = 1;
            cartitem.custid = customer.CustId;
            return View(cartitem);
        }

        [HttpPost]
        public ActionResult AddToCart(Cart cartitem)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            if (cartitem.qty < 1)
            {
                ViewBag.error = "Quantity must be greater than 0";
                return View("AddToCart", cartitem);
            }
            else
            {
                var result = cartBLL.AddtoCart(cartitem);
                if (result)
                {
                    TempData["AlertMessage"] = "Product Added to cart..!";
                    return RedirectToAction("ViewCart");
                }
                else
                {
                    TempData["AlertMessage"] = "Error..!";
                    return View();
                }
            }
        }
        public ActionResult ViewCart()
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            var Cartitems = cartBLL.GetAllItems();
            if (Cartitems.Count != 0)
            {
                return View(Cartitems);
            }
            else
            {
                TempData["CartEmpty"] = "Cart Empty. No data to display..!";
                return View(Cartitems);
            }
        }
        public ActionResult RemoveFromCart(int id)
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            var result = cartBLL.RemovefromCart(id);
            if (result)
            {
                TempData["AlertMessage"] = "Product Removed from Cart..!";
                return RedirectToAction("ViewCart");
            }
            else
            {
                TempData["AlertMessage"] = "Error..!";
                return View("ViewCart");
            }
        }
        public ActionResult Address()
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            ViewBag.amount = dbContext.cartsum().FirstOrDefault();
            ViewBag.amounttax = Convert.ToInt32(ViewBag.amount) * 1.18;
            return View();
        }
        [HttpPost]
        public ActionResult Address(Address address)
        {
            Session["fulladdress"]= address.addressline +", " +address.state + " ,"+address.country;
            return RedirectToAction("Payment");
        }
        public ActionResult Payment()
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            ViewBag.amount = dbContext.cartsum().FirstOrDefault();
            ViewBag.amounttax = Convert.ToInt32(ViewBag.amount) * 1.18;
            return View();
        }
        public ActionResult Checkout()
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            dbContext.copyintoorderiteminfo();
            TempData["AllOrdersMessage"] = "Order Placed Successfully..!";
            dbContext.insertintoOrderInfo();
            TempData["CardEmptiedMessage"] = "Cart Emptied..!";
            dbContext.deletefromcart();
            return RedirectToAction("ViewCart");
        }
        public ActionResult LogOut()
        {
            if (Authenticate() != true) { return RedirectToRoute(new { controller = "Common", action = "UserLogin" }); }
            Session.Abandon();
            return RedirectToRoute(new
            {
                controller = "Common",
                action = "Home"
            });
        }

        [NonAction]
        public bool Authenticate()
        {
            if (Convert.ToString(Session["Email"]) != null)
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