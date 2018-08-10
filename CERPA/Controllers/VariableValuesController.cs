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
    public class VariableValuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VariableValues
        public async Task<ActionResult> Index()
        {
            return View(await db.VariableValues.ToListAsync());
        }

        // GET: VariableValues/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VariableValue variableValue = await db.VariableValues.FindAsync(id);
            if (variableValue == null)
            {
                return HttpNotFound();
            }
            return View(variableValue);
        }

        // GET: VariableValues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VariableValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,OrderId,ConfigurableAssemblyVariableId,Value")] VariableValue variableValue)
        {
            if (ModelState.IsValid)
            {
                db.VariableValues.Add(variableValue);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(variableValue);
        }

        // GET: VariableValues/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VariableValue variableValue = await db.VariableValues.FindAsync(id);
            if (variableValue == null)
            {
                return HttpNotFound();
            }
            return View(variableValue);
        }

        // POST: VariableValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OrderId,ConfigurableAssemblyVariableId,Value")] VariableValue variableValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(variableValue).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(variableValue);
        }

        // GET: VariableValues/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VariableValue variableValue = await db.VariableValues.FindAsync(id);
            if (variableValue == null)
            {
                return HttpNotFound();
            }
            return View(variableValue);
        }

        // POST: VariableValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VariableValue variableValue = await db.VariableValues.FindAsync(id);
            db.VariableValues.Remove(variableValue);
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
