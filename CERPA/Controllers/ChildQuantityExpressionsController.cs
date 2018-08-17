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
    public class ChildQuantityExpressionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ChildQuantityExpressions
        public async Task<ActionResult> Index()
        {
            return View(await db.ChildQuantityExpressions.ToListAsync());
        }

        // GET: ChildQuantityExpressions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildQuantityExpression childQuantityExpression = await db.ChildQuantityExpressions.FindAsync(id);
            if (childQuantityExpression == null)
            {
                return HttpNotFound();
            }
            return View(childQuantityExpression);
        }

        // GET: ChildQuantityExpressions/Create
        public ActionResult Create()
        {
            var partID = Session["PartId"].ToString();
            var variables = db.ConfigurableAssemblyVariables.Where(v => v.PartID == partID).Select(v => v).ToList();
            List<SelectListItem> assemblyVariables = new List<SelectListItem>();
            
                foreach (var variable in variables)
                {
                var listitem = new SelectListItem { Text = variable.VariableName, Value = variable.ID.ToString() };
                assemblyVariables.Add(listitem);
                }

            ViewBag.AssemblyVariables = new SelectList(assemblyVariables, "Value", "Text");

            return View();
        }

        // POST: ChildQuantityExpressions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ChildID,Variable,Devisor,Constant")] ChildQuantityExpression childQuantityExpression)
        {
            if (ModelState.IsValid)
            {
                var partID = Session["PartId"].ToString();
               
                db.ChildQuantityExpressions.Add(childQuantityExpression);               
                await db.SaveChangesAsync();
                return RedirectToAction("Index","PartStructures");
              
            }

            return View(childQuantityExpression);
        }

        // GET: ChildQuantityExpressions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildQuantityExpression childQuantityExpression = await db.ChildQuantityExpressions.FindAsync(id);
            if (childQuantityExpression == null)
            {
                return HttpNotFound();
            }
            return View(childQuantityExpression);
        }

        // POST: ChildQuantityExpressions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ChildID,Variable,Devisor,Constant")] ChildQuantityExpression childQuantityExpression)
        {
            if (ModelState.IsValid)
            {
                db.Entry(childQuantityExpression).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(childQuantityExpression);
        }

        // GET: ChildQuantityExpressions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildQuantityExpression childQuantityExpression = await db.ChildQuantityExpressions.FindAsync(id);
            if (childQuantityExpression == null)
            {
                return HttpNotFound();
            }
            return View(childQuantityExpression);
        }

        // POST: ChildQuantityExpressions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChildQuantityExpression childQuantityExpression = await db.ChildQuantityExpressions.FindAsync(id);
            db.ChildQuantityExpressions.Remove(childQuantityExpression);
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
