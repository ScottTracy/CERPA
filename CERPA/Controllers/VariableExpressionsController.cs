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
    public class VariableExpressionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VariableExpressions
        public async Task<ActionResult> Index()
        {
            return View(await db.VariableExpressions.ToListAsync());
        }

        // GET: VariableExpressions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VariableExpression variableExpression = await db.VariableExpressions.FindAsync(id);
            if (variableExpression == null)
            {
                return HttpNotFound();
            }
            return View(variableExpression);
        }

        // GET: VariableExpressions/Create
        public ActionResult Create()
        {
            PartProperty property = (PartProperty)Session["Property"];
            var parents = db.PartStructures.Where(v => v.ChildID == property.PartID).Select(c=>c.PartID).ToList();
            
            foreach(var parent in parents)
            {
                IEnumerable<int> IntList=(db.ConfigurableAssemblyVariables.Where(c => c.PartID == parent).Select(b=>b.ID).ToList());
                ViewBag.Variables = new SelectList(IntList);
            }
            return View();
        }

        // POST: VariableExpressions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,VariableId,Devisor,Constant,PropertyId")] VariableExpression variableExpression)
        {
            PartProperty property = (PartProperty)Session["Property"];
            variableExpression.PropertyId = property.ID;
            
            if (ModelState.IsValid)
            {
            
                db.VariableExpressions.Add(variableExpression);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(variableExpression);
        }

        // GET: VariableExpressions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VariableExpression variableExpression = await db.VariableExpressions.FindAsync(id);
            if (variableExpression == null)
            {
                return HttpNotFound();
            }
            return View(variableExpression);
        }

        // POST: VariableExpressions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,VariableId,Devisor,Constant,PropertyId")] VariableExpression variableExpression)
        {
            if (ModelState.IsValid)
            {
                db.Entry(variableExpression).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(variableExpression);
        }

        // GET: VariableExpressions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VariableExpression variableExpression = await db.VariableExpressions.FindAsync(id);
            if (variableExpression == null)
            {
                return HttpNotFound();
            }
            return View(variableExpression);
        }

        // POST: VariableExpressions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VariableExpression variableExpression = await db.VariableExpressions.FindAsync(id);
            db.VariableExpressions.Remove(variableExpression);
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
