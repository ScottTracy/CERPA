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

    public class ConfigurableAssemblyVariablesController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ConfigurableAssemblyVariables
        public async Task<ActionResult> Index()
        {
            return View(await db.ConfigurableAssemblyVariables.ToListAsync());
        }

        // GET: ConfigurableAssemblyVariables/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfigurableAssemblyVariable configurableAssemblyVariable = await db.ConfigurableAssemblyVariables.FindAsync(id);
            if (configurableAssemblyVariable == null)
            {
                return HttpNotFound();
            }
            return View(configurableAssemblyVariable);
        }

        // GET: ConfigurableAssemblyVariables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConfigurableAssemblyVariables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PartID,VariableName,ISRequired")] ConfigurableAssemblyVariable configurableAssemblyVariable)
        {
            if (ModelState.IsValid)
            {
                Session["PartID"]= configurableAssemblyVariable.PartID;
                db.ConfigurableAssemblyVariables.Add(configurableAssemblyVariable);
                await db.SaveChangesAsync();
                return RedirectToAction("AddAnother");
            }

            return View(configurableAssemblyVariable);
        }
        // GET: ConfigurableAssemblyVariables/Create
        public ActionResult CreateAnother()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAnother([Bind(Include = "PartID,VariableName,ISRequired")] ConfigurableAssemblyVariable configurableAssemblyVariable)
        {
            var partIDTransfer =
            configurableAssemblyVariable.PartID = Session["PartID"].ToString();
            if (ModelState.IsValid)
            {
                
                db.ConfigurableAssemblyVariables.Add(configurableAssemblyVariable);
                await db.SaveChangesAsync();
                return RedirectToAction("AddAnother");
            }

            return View(configurableAssemblyVariable);
        }

        // GET: ConfigurableAssemblyVariables/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfigurableAssemblyVariable configurableAssemblyVariable = await db.ConfigurableAssemblyVariables.FindAsync(id);
            if (configurableAssemblyVariable == null)
            {
                return HttpNotFound();
            }
            return View(configurableAssemblyVariable);
        }

        // POST: ConfigurableAssemblyVariables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PartID,VariableName,ISRequired")] ConfigurableAssemblyVariable configurableAssemblyVariable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(configurableAssemblyVariable).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(configurableAssemblyVariable);
        }

        // GET: ConfigurableAssemblyVariables/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            
            ConfigurableAssemblyVariable configurableAssemblyVariable = await db.ConfigurableAssemblyVariables.FindAsync(id);
            if (configurableAssemblyVariable == null)
            {
                return HttpNotFound();
            }
            return View(configurableAssemblyVariable);
        }

        // POST: ConfigurableAssemblyVariables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ConfigurableAssemblyVariable configurableAssemblyVariable = await db.ConfigurableAssemblyVariables.FindAsync(id);
            db.ConfigurableAssemblyVariables.Remove(configurableAssemblyVariable);
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
        public async Task<ActionResult> AddAnother()
        {
       
            return View(await db.ConfigurableAssemblyVariables.ToListAsync());
        }


    }
}
