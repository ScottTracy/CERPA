using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CERPA.Models;

namespace CERPA.Controllers
{
    public class PartProcessesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PartProcesses
        public async Task<ActionResult> Index()
        {
            return View(await db.PartProcesses.ToListAsync());
        }

        // GET: PartProcesses/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartProcess partProcess = await db.PartProcesses.FindAsync(id);
            if (partProcess == null)
            {
                return HttpNotFound();
            }
            return View(partProcess);
        }

        // GET: PartProcesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PartProcesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartID,WorkstationID,ProcessTime,UserID")] PartProcess partProcess)
        {
            var PartId = Session["PartId"].ToString();
            partProcess.PartID = PartId;
            if (ModelState.IsValid)
            {
                db.PartProcesses.Add(partProcess);
                db.SaveChangesAsync();
                return RedirectToAction("FullCreate","AssemblyProfiles");
            }

            return View(partProcess);
        }

        // GET: PartProcesses/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartProcess partProcess = await db.PartProcesses.FindAsync(id);
            if (partProcess == null)
            {
                return HttpNotFound();
            }
            return View(partProcess);
        }

        // POST: PartProcesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PartID,WorkstationID,ProcessTime,UserID")] PartProcess partProcess)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partProcess).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(partProcess);
        }

        // GET: PartProcesses/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartProcess partProcess = await db.PartProcesses.FindAsync(id);
            if (partProcess == null)
            {
                return HttpNotFound();
            }
            return View(partProcess);
        }

        // POST: PartProcesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            PartProcess partProcess = await db.PartProcesses.FindAsync(id);
            db.PartProcesses.Remove(partProcess);
            await db.SaveChangesAsync();
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
