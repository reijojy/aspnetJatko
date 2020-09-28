using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppFirst.Models;

namespace WebAppFirst.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.LoginError = 0;
            if (Session["LoggedStatus"] != null)
            {
                ViewBag.LoggedStatus = Session["LoggedStatus"];
            }
            else ViewBag.LoggedStatus = "Out";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Northwind harjoitussovellus.";
            ViewBag.Miete = "Rento suoritus paras suoritus";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Tekijän kontaktitiedot.";

            return View();
        }
        public ActionResult Map()
        {
            ViewBag.Message = "Pläsäri";

            return View();
        }
        public ActionResult Login() { 
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Login LoginModel)
        {
            northwindEntities db = new northwindEntities();
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä 
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null) {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                Session["UserName"] = LoggedUser.UserName;
                Session["LoggedStatus"] = "In";
                ViewBag.LoginError = 0;
                //return View("Login", LoginModel);
                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index 
            } else {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                ViewBag.LoginError = 1;
                Session["LoggedStatus"] = "Out";
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana."; 
                return View("Index", LoginModel); } 
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            ViewBag.LoginError = 0;
            return RedirectToAction("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle
        }
        }
} 