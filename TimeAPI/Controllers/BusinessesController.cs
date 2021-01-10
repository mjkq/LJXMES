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
    public class BusinessesController : Controller
    {
        private QHTEntities db = new QHTEntities();

        // GET: Businesses
        public ActionResult Index()
        {
            var model = db.Business.OrderBy(T => T.ID).ToList().AsEnumerable() ;
            return View(model);
        }



        // GET: Businesses/Create
        public ActionResult Create()
        {
            ViewBag.hard_value = new List<SelectListItem>() {
                new SelectListItem(){Value="小时",Text="小时"},
                new SelectListItem(){Value="分钟",Text="分钟"},
                new SelectListItem(){Value="天数",Text="天数"}
            };
            return View();
        }

        // POST: Businesses/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CODE,NAME,TYPE,ISAUTO,STARTDATE,ENDDATE,VALUE,FREQUENCY")] Models.Business business)
        {
            if (ModelState.IsValid)
            {
                db.Business.Add(business);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(business);
        }

        // GET: Businesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeAPI.Models.Business business = db.Business.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem>  list = new List<SelectListItem>() {
                new SelectListItem(){Value="小时",Text="小时"},
                new SelectListItem(){Value="分钟",Text="分钟"},
                new SelectListItem(){Value="天数",Text="天数"}
            };
            list.Find(a => a.Value.Equals(business.FREQUENCY)).Selected = true;
            ViewBag.selet = list;
            switch (business.FREQUENCY) {

            }
            return View(business);
        }

        // POST: Businesses/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CODE,NAME,TYPE,ISAUTO,STARTDATE,ENDDATE,VALUE,FREQUENCY")] Models.Business business)
        {
            if (ModelState.IsValid)
            {
                db.Entry(business).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(business);
        }

        // GET: Businesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Business business = db.Business.Find(id);
            db.Business.Remove(business);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.Business business = db.Business.Find(id);
            db.Business.Remove(business);
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
