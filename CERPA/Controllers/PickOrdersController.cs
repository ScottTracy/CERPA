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
using System.Collections;

namespace CERPA.Controllers
{
    public class PickOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PickOrders
        public async Task<ActionResult> Index()
        {
            return View(await db.PickOrders.ToListAsync());
        }
        public async Task<ActionResult> _jobInventory()
        {
            var JobId = (int)ViewData["JobId"];
            var pickOrders= await db.PickOrders.Where(o => o.Id == JobId).Select(o => 0).ToListAsync();
            return View(pickOrders);
        }
        // GET: PickOrders
        public async Task<ActionResult> _Operations()
        {
            var pickorders= await db.PickOrders.ToListAsync();
            IEnumerable pickOrders = pickorders;
            return View(pickOrders);
        }
        // GET: PickOrders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickOrder pickOrder = await db.PickOrders.FindAsync(id);
            if (pickOrder == null)
            {
                return HttpNotFound();
            }
            return View(pickOrder);
        }

        // GET: PickOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PickOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,OrderId,PartId,Location,Destination,Start,Confirmed,IsConfirmed,UserId,PartQuantity")] PickOrder pickOrder)
        {
            if (ModelState.IsValid)
            {
                db.PickOrders.Add(pickOrder);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pickOrder);
        }

        // GET: PickOrders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickOrder pickOrder = await db.PickOrders.FindAsync(id);
            if (pickOrder == null)
            {
                return HttpNotFound();
            }
            return View(pickOrder);
        }

        // POST: PickOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OrderId,PartId,Location,Destination,Start,Confirmed,IsConfirmed,UserId,PartQuantity")] PickOrder pickOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickOrder).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pickOrder);
        }

        // GET: PickOrders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickOrder pickOrder = await db.PickOrders.FindAsync(id);
            if (pickOrder == null)
            {
                return HttpNotFound();
            }
            return View(pickOrder);
        }

        // POST: PickOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PickOrder pickOrder = await db.PickOrders.FindAsync(id);
            db.PickOrders.Remove(pickOrder);
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
