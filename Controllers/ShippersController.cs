using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppFirst.Models;
using System.Net;
using System.Data.Entity;

namespace WebAppFirst.Controllers
{
    
    public class ShippersController : Controller
    {
        // GET: Shippers
        northwindEntities db = new northwindEntities();
        public ActionResult Index()
        {
            //List<Shipper> model = db.Shippers.ToList();
            //return View(model);
            // Ikään kuin joinataan Region taulu
            if (Session["LoggedStatus"] != null) ViewBag.LoggedStatus = Session["LoggedStatus"];
            else ViewBag.LoggedStatus = "Out";
            if (Session["UserName"] == null) return RedirectToAction("login", "home");
            else
            {
                var shippers = db.Shippers.Include(s => s.Region);
                return View(shippers.ToList());
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Shipper shipper = db.Shippers.Find(id);
            if (shipper == null) return HttpNotFound();
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionDescription", shipper.RegionID);
            return View(shipper);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Katso https://go.microsoft.com/fwlink/?LinkId=317598
        public ActionResult Edit([Bind(Include = "ShipperID,CompanyName,Phone,RegionID")] Shipper shipper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shipper).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionDescription", shipper.RegionID);
                return RedirectToAction("Index");
            }
            return View(shipper);
        }
        public ActionResult Create()
        {
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionDescription");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShipperID,CompanyName,Phone,RegionID")] Shipper shipper)
        {
            if (ModelState.IsValid)
            {
                db.Shippers.Add(shipper);
                db.SaveChanges();
                ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionDescription", shipper.RegionID);
                return RedirectToAction("Index");
            }
            return View(shipper);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Shipper shippers = db.Shippers.Find(id);
            if (shippers == null) return HttpNotFound();
            return View(shippers);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Order> orders = db.Orders.Where(o => o.ShipVia == id).ToList();
            if (orders.Count == 0)
            {
                Shipper shippers = db.Shippers.Find(id);
                db.Shippers.Remove(shippers);
                db.SaveChanges();
            } else
            {
                //return "Ei onnistu";
            }
            return RedirectToAction("Index");
        }
    }
}