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
    public class AssemblyProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AssemblyProfiles
        public async Task<ActionResult> Index()
        {
            return View(await db.AssemblyProfiles.ToListAsync());
        }

        // GET: AssemblyProfiles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssemblyProfile assemblyProfile = await db.AssemblyProfiles.FindAsync(id);
            if (assemblyProfile == null)
            {
                return HttpNotFound();
            }
            return View(assemblyProfile);
        }

        // GET: AssemblyProfiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssemblyProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public  ActionResult Create([Bind(Include = "AssemblyID,Items")] AssemblyProfile assemblyProfile)
        {


            
            return View("AddVariables", assemblyProfile);
        }
     

        // GET: AssemblyProfiles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssemblyProfile assemblyProfile = await db.AssemblyProfiles.FindAsync(id);
            if (assemblyProfile == null)
            {
                return HttpNotFound();
            }
            return View(assemblyProfile);
        }

        // POST: AssemblyProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AssemblyID")] AssemblyProfile assemblyProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assemblyProfile).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(assemblyProfile);
        }

        // GET: AssemblyProfiles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssemblyProfile assemblyProfile = await db.AssemblyProfiles.FindAsync(id);
            if (assemblyProfile == null)
            {
                return HttpNotFound();
            }
            return View(assemblyProfile);
        }

        // POST: AssemblyProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AssemblyProfile assemblyProfile = await db.AssemblyProfiles.FindAsync(id);
            db.AssemblyProfiles.Remove(assemblyProfile);
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
        //GET: ConfigurableAssemblyVariables
        public async Task<ActionResult> AddVariables(AssemblyProfile assemblyProfile)
        {
            
            return View(assemblyProfile);


        }

    }
    
}
