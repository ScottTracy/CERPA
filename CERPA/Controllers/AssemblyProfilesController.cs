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
        // GET: AssemblyProfiles/Start
        public ActionResult Start()
        {
            return View();
        }

        // POST: AssemblyProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Start([Bind(Include = "AssemblyID,Items")] AssemblyProfile assemblyProfile)
        {
            Session["PartId"] = assemblyProfile.Items.PartID;


            return RedirectToAction("AddAnother", "ConfigurableAssemblyVariables");
        }
        // GET: AssemblyProfiles/Start
        public ActionResult FullCreate()
        {
            AssemblyProfile assemblyProfile = new AssemblyProfile();
            string partId = Session["PartID"].ToString();
            assemblyProfile = db.AssemblyProfiles.Where(a => a.Items.PartID == partId).FirstOrDefault();
            return View(assemblyProfile);
        }

        // POST: AssemblyProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public async Task<ActionResult> FullCreate([Bind(Include = "AssemblyID,Items,Structures,Processes,Properties,Variables")] AssemblyProfile assemblyProfile)
        {
            string partId = Session["PartID"].ToString();
            assemblyProfile.Structures = db.PartStructures.Where(p => p.PartID == partId).FirstOrDefault();
            assemblyProfile.Processes = db.PartProcesses.Where(p => p.PartID == partId).FirstOrDefault();
            assemblyProfile.Properties = db.PartProperties.Where(p => p.PartID == partId).ToList();
            assemblyProfile.Items = db.Inventory.Where(p => p.PartID == partId).FirstOrDefault();
            assemblyProfile.Variables = db.ConfigurableAssemblyVariables.Where(v => v.PartID == partId).ToList();
            
            
            if (ModelState.IsValid)
            {
                AutoCreate(partId);
                db.AssemblyProfiles.Add(assemblyProfile);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }

            return RedirectToAction("Index", "Home");
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
        public ActionResult AddVariables(AssemblyProfile assemblyProfile)
        {
            
            return View(assemblyProfile);


        }
        public async void AutoCreate(string _PartID)
        {
            if (db.Inventory.Any(i => i.PartID == _PartID))
            {
                return;
            }
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

    }
    
}
