using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TICRM.DAL;

namespace TICRM.Controllers
{
    public class WorkFlowNodesController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: WorkFlowNodes
        public ActionResult Index()
        {
            return View(db.WorkFlowNodes.ToList());
        }

        // GET: WorkFlowNodes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowNode workFlowNode = db.WorkFlowNodes.Find(id);
            if (workFlowNode == null)
            {
                return HttpNotFound();
            }
            return View(workFlowNode);
        }

        // GET: WorkFlowNodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkFlowNodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NodeDataId,text,key,figure,fill,loc")] WorkFlowNode workFlowNode)
        {
            if (ModelState.IsValid)
            {
                workFlowNode.NodeDataId = Guid.NewGuid();
                db.WorkFlowNodes.Add(workFlowNode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workFlowNode);
        }

        // GET: WorkFlowNodes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowNode workFlowNode = db.WorkFlowNodes.Find(id);
            if (workFlowNode == null)
            {
                return HttpNotFound();
            }
            return View(workFlowNode);
        }

        // POST: WorkFlowNodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NodeDataId,text,key,figure,fill,loc")] WorkFlowNode workFlowNode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workFlowNode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workFlowNode);
        }

        // GET: WorkFlowNodes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowNode workFlowNode = db.WorkFlowNodes.Find(id);
            if (workFlowNode == null)
            {
                return HttpNotFound();
            }
            return View(workFlowNode);
        }

        // POST: WorkFlowNodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            WorkFlowNode workFlowNode = db.WorkFlowNodes.Find(id);
            db.WorkFlowNodes.Remove(workFlowNode);
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
