using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using WebTab.Models;

namespace WebTab.Controllers
{
    public class SpendingsController : Controller
    {
        // GET: Spendings
        public ActionResult GetSpendingsTable(int? id)
        {
            if (id == null || id < 1 || id > 12)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Month = id;

            var table = new WebTable((int)id);
            return View(table);
        }
    }
}