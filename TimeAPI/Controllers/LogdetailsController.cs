using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimeAPI.Models;

namespace TimeAPI.Controllers
{
    public class LogdetailsController : Controller
    {
        private QHTEntities db = new QHTEntities();

        // GET: Logdetails
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return View( db.Logdetail.ToList().OrderByDescending(a => a.date));
            }
            else
            {
                return View(db.Logdetail.Where(s => s.busId == id).OrderByDescending(a => a.date).ToList());
            }
        }

        // GET: Logdetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logdetail logdetail = db.Logdetail.Find(id);
            if (logdetail == null)
            {
                return HttpNotFound();
            }
            return View(logdetail);
        }

        // GET: Logdetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logdetails/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,busId,code,date,url,postData,postType,returnData,result,description,mark")] Logdetail logdetail)
        {
            if (ModelState.IsValid)
            {
                db.Logdetail.Add(logdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(logdetail);
        }

        // GET: Logdetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logdetail logdetail = db.Logdetail.Find(id);
            if (logdetail == null)
            {
                return HttpNotFound();
            }
            return View(logdetail);
        }

        // POST: Logdetails/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,busId,code,date,url,postData,postType,returnData,result,description,mark")] Logdetail logdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logdetail);
        }

        // GET: Logdetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logdetail logdetail = db.Logdetail.Find(id);
            if (logdetail == null)
            {
                return HttpNotFound();
            }
            return View(logdetail);
        }

        // POST: Logdetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Logdetail logdetail = db.Logdetail.Find(id);
            db.Logdetail.Remove(logdetail);
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
