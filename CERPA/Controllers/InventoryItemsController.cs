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
    
     
    public class InventoryItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InventoryItems
        public async Task<ActionResult> Index()
        {
            return View(await db.Inventory.ToListAsync());
        }

        // GET: InventoryItems/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryItem inventoryItem = await db.Inventory.FindAsync(id);
            if (inventoryItem == null)
            {
                return HttpNotFound();
            }
            return View(inventoryItem);
        }

        // GET: InventoryItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InventoryItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PartID,Location,LastConfirmed,Quantity,ReorderPoint,ReorderQuantity")] InventoryItem inventoryItem)
        {
            if (ModelState.IsValid)
            {
                db.Inventory.Add(inventoryItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(inventoryItem);
        }
        public async void AutoCreate(string _PartID)
        {
            InventoryItem item = new InventoryItem();
            item.PartID = _PartID;
            item.LastConfirmed = DateTime.Now;
            item.Quantity = 0;
            item.ReorderPoint = 0;
            item.ReorderQuantity = 0;
            if (ModelState.IsValid)
            {
                db.Inventory.Add(item);
                await db.SaveChangesAsync();

            }
        }

        // GET: InventoryItems/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryItem inventoryItem = await db.Inventory.FindAsync(id);
            if (inventoryItem == null)
            {
                return HttpNotFound();
            }
            return View(inventoryItem);
        }

        // POST: InventoryItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PartID,Location,LastConfirmed,Quantity,ReorderPoint,ReorderQuantity")] InventoryItem inventoryItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventoryItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(inventoryItem);
        }

        // GET: InventoryItems/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryItem inventoryItem = await db.Inventory.FindAsync(id);
            if (inventoryItem == null)
            {
                return HttpNotFound();
            }
            return View(inventoryItem);
        }

        // POST: InventoryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            InventoryItem inventoryItem = await db.Inventory.FindAsync(id);
            db.Inventory.Remove(inventoryItem);
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
