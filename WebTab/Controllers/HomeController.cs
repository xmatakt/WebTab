using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTab.Controllers
{
    public class HomeController : Controller
    {
         //GET: Home
        public ActionResult Index()
        {
            ViewBag.Ahoj = "Ahoj ty bastard";
            return View();
        }
    }
}