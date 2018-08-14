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
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jobs
        public async Task<ActionResult> Index()
        {

            return View(await db.Jobs.ToListAsync());
        }

        // GET: Jobs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,OrderID,Workstation,PartID,Start,ConfirmedOn,UserID,IsConfirmed")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,OrderID,Workstation,PartID,Start,ConfirmedOn,UserID,IsConfirmed")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Job job = await db.Jobs.FindAsync(id);
            db.Jobs.Remove(job);
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
        public void JobCreate(Order order)
        {
            var workstation = GetWorkstation(order.PartID);

            Job job = new Job
            {
                OrderID = order.OrderID,
                PartID = order.PartID,
                Workstation = workstation,
                IsConfirmed = false
            };
            JobCreate(job);


        }
        public  string GetWorkstation(string PartId)
        {
            return db.PartProcesses.Where(p => p.PartID ==PartId).Select(c => c.WorkstationID).First();         
        }
        public async void JobCreate(Job job)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                await db.SaveChangesAsync();
            }
        }
        public List<PartStructure> GetPartStructures(string PartId)
        {
            return db.PartStructures.Where(p => p.PartID == PartId).Select(s => s).ToList();
        }
        public List<PartProperty> GetPartProperties(string PartId)
        {
            return db.PartProperties.Where(p => p.PartID == PartId).Select(c => c).ToList();
        }
        public VariableExpression GetVariableExpression(int PropertyId)
        {
            return db.VariableExpressions.Where(e => e.VariableId == PropertyId).Select(v => v).First();
        }
        public double SolveVariableExpression(VariableExpression variableExpression,VariableValue variableValue)
        {
            return variableValue.Value / variableExpression.Devisor + variableExpression.Constant;
        }
        public PropertyValue AssignPropertyValue(PartProperty property)
        {
            var order = (Order)Session["Order"];
            var variables = (List<ConfigurableAssemblyVariable>)Session["Variables"];
            var variableValues = (List<VariableValue>)Session["VariableValues"];
            
            var value = new PropertyValue();
            if (property.IsPropertyConfigurable == false)
            {
                value.ExpressionResult= property.PropertyValue;
                value.PropertyId = property.ID;
                value.OrderId = order.OrderID;
                return value;
            }
            var expression = GetVariableExpression(property.ID);
            var variableValue = variableValues.Where(v => v.ConfigurableAssemblyVariableId == expression.VariableId).Select(c => c).First();
            value.ExpressionResult = (SolveVariableExpression(expression, variableValue)).ToString();
            value.PropertyId = property.ID;
            value.OrderId = order.OrderID;
            return value;
        }
        public async void CreateQuantityValue(QuantityExpressionValue Quantityvalue)
        {
            if (ModelState.IsValid)
            {
                db.QuantityExpressionValues.Add(Quantityvalue);
                await db.SaveChangesAsync();
            }
        }
        
        public ChildQuantityExpression GetQuantityExpression(string partId)
        {
            return db.ChildQuantityExpressions.Where(c => c.ChildID == partId).Select(e => e).First();
        }
        public double SolveQuantityExpression(VariableValue variableValue,ChildQuantityExpression expression )
        {
            return variableValue.Value / expression.Devisor + expression.Constant;
        }
        public QuantityExpressionValue GetQuantity(PartStructure partStructure,Order order)
        {
            QuantityExpressionValue value = new QuantityExpressionValue();
            if (partStructure.ISChildQuantityConfigurable == false)
            {
                value.ChildQuantityValue = partStructure.ChildQuantity;
                value.ChildQuantityExpressionId = null;
                value.OrderId = order.OrderID;
                return value;
            }
            var expression = GetQuantityExpression(partStructure.ChildID);
            var variableValue = db.VariableValues.Where(v => v.Id == expression.VariableId).Select(v => v).First();
            value.ChildQuantityExpressionId = expression.Id;
            value.ChildQuantityValue = Convert.ToInt32(SolveQuantityExpression(variableValue, expression));
            value.OrderId = order.OrderID;
            return value;
        }

        
        public Job AssignJob(PartStructure partStructure,Order order)
        {
            var workstation = GetWorkstation(partStructure.ChildID);
            Job job = new Job
            {
                OrderID = order.OrderID,
                PartID = partStructure.ChildID,
                Workstation = workstation,
                IsConfirmed = false
            };
            return job;
        }
    }
}
