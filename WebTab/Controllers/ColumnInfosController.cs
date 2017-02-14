using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebTab.Models;

namespace WebTab.Controllers
{
    public class ColumnInfosController : Controller
    {
        private webtabProjectEntities db = new webtabProjectEntities();

        // GET: ColumnInfos
        public ActionResult Index()
        {
            var columnInfoes = db.ColumnInfoes.Include(c => c.ColumnEffect1);
            return View(columnInfoes.ToList());
        }

        // GET: ColumnInfos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColumnInfo columnInfo = db.ColumnInfoes.Find(id);
            if (columnInfo == null)
            {
                return HttpNotFound();
            }
            return View(columnInfo);
        }

        // GET: ColumnInfos/Create
        public ActionResult Create()
        {
            ViewBag.ColumnEffect = new SelectList(db.ColumnEffects, "EffectID", "Effect");
            return View();
        }

        // POST: ColumnInfos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ColumnName,ColumnOrder,ColumnEffect,IsActive")] ColumnInfo columnInfo)
        {
            if (ModelState.IsValid)
            {
                db.ColumnInfoes.Add(columnInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ColumnEffect = new SelectList(db.ColumnEffects, "EffectID", "Effect", columnInfo.ColumnEffect);
            return View(columnInfo);
        }

        // GET: ColumnInfos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColumnInfo columnInfo = db.ColumnInfoes.Find(id);
            if (columnInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColumnEffect = new SelectList(db.ColumnEffects, "EffectID", "Effect", columnInfo.ColumnEffect);
            return View(columnInfo);
        }

        // POST: ColumnInfos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ColumnName,ColumnOrder,ColumnEffect,IsActive")] ColumnInfo columnInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(columnInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColumnEffect = new SelectList(db.ColumnEffects, "EffectID", "Effect", columnInfo.ColumnEffect);
            return View(columnInfo);
        }

        // GET: ColumnInfos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColumnInfo columnInfo = db.ColumnInfoes.Find(id);
            if (columnInfo == null)
            {
                return HttpNotFound();
            }
            return View(columnInfo);
        }

        // POST: ColumnInfos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ColumnInfo columnInfo = db.ColumnInfoes.Find(id);
            db.ColumnInfoes.Remove(columnInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
