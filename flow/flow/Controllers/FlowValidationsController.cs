using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using flow.Context;
using flow.Models.Business;
using flow.Models.Entities;

namespace flow.Controllers
{
    public class FlowValidationsController : Controller
    {
        private FlowDbContext db = new FlowDbContext();

        // GET: FlowValidations
        public ActionResult Index()
        {
            var flowValidation = db.FlowValidation.Include(f => f.Status);
            return View(flowValidation.ToList());
        }

        // GET: FlowValidations/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlowValidation flowValidation = db.FlowValidation.Find(id);
            if (flowValidation == null)
            {
                return HttpNotFound();
            }
            return View(flowValidation);
        }

        // GET: FlowValidations/Create
        public ActionResult Create()
        {
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description");
            return View();
        }

        // POST: FlowValidations/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StatusID,UserCode,DocumentCode,AnalisysDate,Description,ComplementaryInfo")] FlowValidation flowValidation)
        {
            if (ModelState.IsValid)
            {
                db.FlowValidation.Add(flowValidation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description", flowValidation.StatusID);
            return View(flowValidation);
        }

        // GET: FlowValidations/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlowValidation flowValidation = db.FlowValidation.Find(id);
            if (flowValidation == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description", flowValidation.StatusID);
            return View(flowValidation);
        }

        // POST: FlowValidations/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StatusID,UserCode,DocumentCode,AnalisysDate,Description,ComplementaryInfo")] FlowValidation flowValidation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flowValidation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusID = new SelectList(db.Status, "ID", "Description", flowValidation.StatusID);
            return View(flowValidation);
        }

        // GET: FlowValidations/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlowValidation flowValidation = db.FlowValidation.Find(id);
            if (flowValidation == null)
            {
                return HttpNotFound();
            }
            return View(flowValidation);
        }

        // POST: FlowValidations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FlowValidation flowValidation = db.FlowValidation.Find(id);
            db.FlowValidation.Remove(flowValidation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void PerformValidationAction(long id, string action, string details)
        {
            long destino = FlowValidationBusiness.PerformValidation(db, id, usuario, acao, justificativa);
        }

        public IList<SelectListItem> GetBotoes(string responsible, long status)
        {
            IList<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem() { Value = "000", Text = "-- Choose Action to Perform --" });

            IList<SelectListItem> results = new MainFlowBusiness(db)
                                            .FindBy(responsible, status)
                                            .Select(x => new SelectListItem()
                                            {
                                                Text = x.ActionName,
                                                Value = x.ActionName
                                            })
                                            .ToList<SelectListItem>();

            return lista;
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
