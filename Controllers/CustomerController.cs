using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppFirst.Models;


namespace WebAppFirst.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            northwindEntities db = new northwindEntities();
            List<Customer> model = db.Customers.ToList();
            db.Dispose();
            return View(model);
        }
    }
}