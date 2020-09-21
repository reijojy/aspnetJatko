using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAppFirst.Models;
using PagedList;

namespace WebAppFirst.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index(string sortOrder, string currentFilter1, string SearchString1, int? page, int? pagesize)
        {
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.ProductNameSortParm = String.IsNullOrEmpty(sortOrder) ? "prodcutname_desc" : "";
            ViewBag.UnitPriceSortParm = sortOrder == "UnitPrice" ? "UnitPrice_desc" : "UnitPrice";
            if(SearchString1 != null)
            {
                page = 1;
            }
            else
            {
                SearchString1 = currentFilter1;
            }
            ViewBag.CurrentFilter1 = SearchString1;
            if (Session["LoggedStatus"] != null)
            {
                ViewBag.LoggedStatus = Session["LoggedStatus"];
            }
            else ViewBag.LoggedStatus = "Out";
            northwindEntities db = new northwindEntities();
            var tuotteet = from p in db.Products select p;
            if(!String.IsNullOrEmpty(SearchString1))
            {
                tuotteet = tuotteet.Where(p => p.ProductName.Contains(SearchString1));
            }
            switch(sortOrder)
            {
                case "prodcutname_desc":
                    tuotteet = tuotteet.OrderByDescending(p => p.ProductName);
                    break;
                case "UnitPrice":
                    tuotteet = tuotteet.OrderBy(p => p.UnitPrice);
                    break;
                case "UnitPrice_desc":
                    tuotteet = tuotteet.OrderByDescending(p => p.UnitPrice);
                    break;
                default:
                    tuotteet = tuotteet.OrderBy(p => p.ProductName);
                    break;
            }
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return View(tuotteet.ToPagedList(pageNumber, pageSize)); 
        }
      /*  public ActionResult Index2()
        {
            northwindEntities db = new northwindEntities();
            List<Product> model = db.Products.ToList();
            //db.Dispose();
            return View(model);
        }
        public ActionResult ProdCards()
        {
            northwindEntities db = new northwindEntities();
            List<Product> model = db.Products.ToList();
            db.Dispose();
            return View(model);
        } */
    }
}