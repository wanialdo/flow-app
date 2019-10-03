using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using flow.Context;
using flow.Models.Entities;

namespace flow.Controllers
{
    public class MainFlowController : Controller
    {
        private FlowDbContext db = new FlowDbContext();

        // GET: MainFlow
        public ActionResult Index()
        {
            var mainFlow = db.MainFlow.Include(m => m.FlowEndStatus).Include(m => m.FlowInitStatus);
            return View(mainFlow.ToList());
        }

        // GET: MainFlow/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainFlow mainFlow = db.MainFlow.Find(id);
            if (mainFlow == null)
            {
                return HttpNotFound();
            }
            return View(mainFlow);
        }

        // GET: MainFlow/Create
        public ActionResult Create()
        {
            ViewBag.FlowEndStatusID = new SelectList(db.Status, "ID", "Description");
            ViewBag.FlowInitStatusID = new SelectList(db.Status, "ID", "Description");
            return View();
        }

        // POST: MainFlow/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FlowInitStatusID,FlowEndStatusID,DescricaoGrupo,DescricaoAcao,MaxAnalisysPeriod,StepNumber")] MainFlow mainFlow)
        {
            if (ModelState.IsValid)
            {
                db.MainFlow.Add(mainFlow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FlowEndStatusID = new SelectList(db.Status, "ID", "Description", mainFlow.FlowEndStatusID);
            ViewBag.FlowInitStatusID = new SelectList(db.Status, "ID", "Description", mainFlow.FlowInitStatusID);
            return View(mainFlow);
        }

        // GET: MainFlow/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainFlow mainFlow = db.MainFlow.Find(id);
            if (mainFlow == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlowEndStatusID = new SelectList(db.Status, "ID", "Description", mainFlow.FlowEndStatusID);
            ViewBag.FlowInitStatusID = new SelectList(db.Status, "ID", "Description", mainFlow.FlowInitStatusID);
            return View(mainFlow);
        }

        // POST: MainFlow/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FlowInitStatusID,FlowEndStatusID,DescricaoGrupo,DescricaoAcao,MaxAnalisysPeriod,StepNumber")] MainFlow mainFlow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mainFlow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlowEndStatusID = new SelectList(db.Status, "ID", "Description", mainFlow.FlowEndStatusID);
            ViewBag.FlowInitStatusID = new SelectList(db.Status, "ID", "Description", mainFlow.FlowInitStatusID);
            return View(mainFlow);
        }

        // GET: MainFlow/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainFlow mainFlow = db.MainFlow.Find(id);
            if (mainFlow == null)
            {
                return HttpNotFound();
            }
            return View(mainFlow);
        }

        // POST: MainFlow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MainFlow mainFlow = db.MainFlow.Find(id);
            db.MainFlow.Remove(mainFlow);
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
