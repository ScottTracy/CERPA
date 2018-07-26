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
    public class PartPropertiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PartProperties
        public async Task<ActionResult> Index()
        {
            return View(await db.PartProperties.ToListAsync());
        }

        // GET: PartProperties/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartProperty partProperty = await db.PartProperties.FindAsync(id);
            if (partProperty == null)
            {
                return HttpNotFound();
            }
            return View(partProperty);
        }

        // GET: PartProperties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PartProperties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PartID,PropertyName,IsPropertyConfigurable,PropertyValue")] PartProperty partProperty)
        {
            if (ModelState.IsValid)
            {
                db.PartProperties.Add(partProperty);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(partProperty);
        }

        // GET: PartProperties/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartProperty partProperty = await db.PartProperties.FindAsync(id);
            if (partProperty == null)
            {
                return HttpNotFound();
            }
            return View(partProperty);
        }

        // POST: PartProperties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PartID,PropertyName,IsPropertyConfigurable,PropertyValue")] PartProperty partProperty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partProperty).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(partProperty);
        }

        // GET: PartProperties/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartProperty partProperty = await db.PartProperties.FindAsync(id);
            if (partProperty == null)
            {
                return HttpNotFound();
            }
            return View(partProperty);
        }

        // POST: PartProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            PartProperty partProperty = await db.PartProperties.FindAsync(id);
            db.PartProperties.Remove(partProperty);
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
