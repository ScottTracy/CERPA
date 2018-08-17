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
    public class PartStructuresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PartStructures
        public async Task<ActionResult> Index()
        {
            return View(await db.PartStructures.ToListAsync());
        }

        // GET: PartStructures/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartStructure partStructure = await db.PartStructures.FindAsync(id);
            if (partStructure == null)
            {
                return HttpNotFound();
            }
            return View(partStructure);
        }

        // GET: PartStructures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PartStructures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,PartID,ChildID,ISChildQuantityConfigurable,ChildQuantity")] PartStructure partStructure)
        {
            partStructure.PartID = Session["PartId"].ToString();
            Session["ChildId"] = partStructure.ChildID;
            await AutoCreate(partStructure.ChildID);
            if(partStructure.ISChildQuantityConfigurable == true && partStructure.ChildQuantityExpression== null)
            {

                Session["PartStructure"] = partStructure;
                db.PartStructures.Add(partStructure);
                await db.SaveChangesAsync();
                return RedirectToAction("Create", "ChildQuantityExpressions");
            }
            if (ModelState.IsValid)
            {
                db.PartStructures.Add(partStructure);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(partStructure);
        }

        // GET: PartStructures/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartStructure partStructure = await db.PartStructures.FindAsync(id);
            if (partStructure == null)
            {
                return HttpNotFound();
            }
            return View(partStructure);
        }

        // POST: PartStructures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PartID,ChildID,ISChildQuantityConfigurable,ChildQuantity")] PartStructure partStructure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partStructure).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(partStructure);
        }

        // GET: PartStructures/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartStructure partStructure = await db.PartStructures.FindAsync(id);
            if (partStructure == null)
            {
                return HttpNotFound();
            }
            return View(partStructure);
        }

        // POST: PartStructures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PartStructure partStructure = await db.PartStructures.FindAsync(id);
            db.PartStructures.Remove(partStructure);
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
        public async Task AutoCreate(string _PartID)
        {
            if ( db.Inventory.Any(i => i.PartID == _PartID))
            {
                return;
            }
            InventoryItem item = new InventoryItem
            {
                PartID = _PartID,

                LastConfirmed = DateTime.Now,
                Quantity = 0,
                ReorderPoint = 0,
                ReorderQuantity = 0
            };
            if (ModelState.IsValid)
            {
                db.Inventory.Add(item);
                await db.SaveChangesAsync();

            }
        }
    }
}
