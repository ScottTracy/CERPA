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
using Microsoft.AspNet.Identity;

namespace CERPA.Controllers
{
   
    public class JobsController : Controller
    {
        InventoryControlController ic =new InventoryControlController();
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Jobs
        public async Task<ActionResult> ActiveJobs()
        {
            var activeJobs = await db.Jobs.ToListAsync();
            
            return View(activeJobs.Where(j=>j.IsConfirmed==false).Select(c=>c).ToList());
        }
        
        // GET: Jobs
        public async Task<ActionResult> JobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            if (UserId == null)
            {
                return RedirectToAction("Login","Account");
            }
            IEnumerable<string> workstations = db.Jobs.Where(j=>j.IsConfirmed== false).Select(j => j.Workstation).Distinct().ToList();
            List<SelectListItem> stations = new List<SelectListItem>();
            foreach(var item in workstations)
            {
                stations.Add(new SelectListItem
                {
                    Text = item,
                    Value = item
                });
            }
            ViewData["Workstations"] = stations; 
            var jobsByUser =await db.Jobs.Where(j=>j.UserID==UserId&& j.IsConfirmed== false).Select(j=>j).ToListAsync();
            return View(jobsByUser);
        }
       
        public ActionResult GetJobsByWorkstation(string workstation)
        {
            
            IEnumerable<string> workstations= db.Jobs.Select(j => j.Workstation).Distinct().ToList();
            ViewBag.Workstations = new SelectList(workstations);
           
            return View("JobsByWorkstation",db.Jobs.Where(j => j.Workstation == workstation&& j.IsConfirmed==false).Select(c => c).ToList());
        }
        public ActionResult JobsByWorkstation(List<Job> jobs)
        {
            IEnumerable<Job> jobsList = jobs;


            
            


            return View(jobsList);
            

        }


        // GET: Jobs
        public async Task<ActionResult> Index()
        {
            var jobs = await db.Jobs.ToListAsync();
            foreach(var job in jobs)
            {
                if (job.OrderID > 1000)
                {
                    await DeleteConfirmed(job.ID);
                }
                if (job.UserID != null)
                {
                    job.UserID = GetUsername(job.UserID);
                }
                
            }
            return View(jobs);
        }
        // GET: Jobs
        public async Task<ActionResult> Operations()
        {
            var orderId = (Int32)Session["Order"];
            var order = db.Orders.Where(o => o.Id == orderId).Select(o => o).First();
            await StartJobAssignments(order);
            var pickorders = await db.PickOrders.ToListAsync();
            IEnumerable pickOrders = (IEnumerable)pickorders;
            ViewData["pickOrders"] = pickOrders;

            return View(await db.Jobs.ToListAsync());
        }
        // GET: Jobs/CurrentJob/5
        public async Task<ActionResult> CurrentJob(int? id)
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
            Session["JobId"] = job.ID;
            return View(job);
        }
        public async Task<ActionResult> Start(int? id)
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
             
            job.UserID = User.Identity.GetUserId();
            job.Start = DateTime.Now;
            Session["JobId"] = job.ID;
            db.Entry(job).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return View(job);
        }
        
        public async Task<ActionResult> Confirm()
        {
            var jobId = (int)Session["JobId"];
            Job job = db.Jobs.Where(j => j.ID == jobId).Select(j => j).First();
            job.ConfirmedOn = DateTime.Now;
            job.IsConfirmed = true;
            if (ModelState.IsValid)
            {
                await ic.ReduceInventory(job);
                db.Entry(job).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View(job);
        }


        //public async Task ReportInventory()
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
        public  string GetUsername(string Id)
        {
            var userName =  db.Users.Where(u => u.Id == Id).Select(u => u.UserName).First();
            return userName;
        }

        // GET: Jobs/Assign/5
        public async Task<ActionResult> Assign(int? id)
        {
            var groupId = db.ApplicationGroups.Where(a => a.Name == "Production").Select(a => a.Id).First();
            var productionEmployees = db.ApplicationUsers.Where(g => g.ApplicationGroupId == groupId).Select(g => g.ApplicationUserId).ToList();
            
                                    
            List<SelectListItem> prodEmployees = new List<SelectListItem>();
            foreach (var employee in productionEmployees)
            {

                prodEmployees.Add(new SelectListItem
                {

                    Text = GetUsername(employee),
                    Value = employee
                });
            }
            ViewData["ProductionEmployees"] = new SelectList(prodEmployees,"Value","Text");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["Job"] = job;
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
        // POST: Jobs/Assign/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Assign([Bind(Include = "ID,OrderID,Workstation,PartID,Start,ConfirmedOn,UserID,IsConfirmed")] Job job)
        {
            var jobOriginal = (Job)Session["Job"];
            job.ID = jobOriginal.ID;
            job.OrderID = jobOriginal.OrderID;
            job.PartID = jobOriginal.PartID;
            job.Workstation = jobOriginal.Workstation;
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
        public async Task<Job> AssignJob(Order order)
        {
            var workstation =await GetWorkstation(order.PartID);

            Job job = new Job
            {
                OrderID = order.Id,
                PartID = order.PartID,
                Workstation = workstation,
                IsConfirmed = false
            };
            return job;
        }
        public async Task<string> GetWorkstation(string PartId)
        {
            return await db.PartProcesses.Where(p => p.PartID == PartId).Select(c => c.WorkstationID).FirstAsync();
        }
        public async Task CreateJob(Job job)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                await db.SaveChangesAsync();
            }
        }
        public async Task<List<PartStructure>> GetPartStructures(string PartId)
        {
            return await db.PartStructures.Where(p => p.PartID == PartId).Select(s => s).ToListAsync();
        }
        public async Task<List<PartProperty>> GetPartProperties(string PartId)
        {
            return await db.PartProperties.Where(p => p.PartID == PartId).Select(c => c).ToListAsync();
        }
        public async Task<VariableExpression> GetVariableExpression(int PropertyId)
        {
            return await db.VariableExpressions.Where(e => e.VariableId == PropertyId).Select(v => v).FirstAsync();
        }
        public double SolveVariableExpression(VariableExpression variableExpression, VariableValue variableValue)
        {
            return variableValue.Value / variableExpression.Devisor + variableExpression.Constant;
        }
        public async Task<PropertyValue> AssignPropertyValue(PartProperty property)
        {
            var order = (Order)Session["Order"];
            var variables = (List<ConfigurableAssemblyVariable>)Session["Variables"];
            var variableValues = (List<VariableValue>)Session["VariableValues"];

            var value = new PropertyValue();
            if (property.IsPropertyConfigurable == false)
            {
                value.ExpressionResult = property.PropertyValue;
                value.PropertyId = property.ID;
                value.OrderId = order.Id;
                return value;
            }
            var expression = await GetVariableExpression(property.ID);
            var variableValue = variableValues.Where(v => v.ConfigurableAssemblyVariableId == expression.VariableId).Select(c => c).First();
            value.ExpressionResult = (SolveVariableExpression(expression, variableValue)).ToString();
            value.PropertyId = property.ID;
            value.OrderId = order.Id;
            return value;
        }
        public async Task CreatePropertyValue(PropertyValue propertyValue)
        {
            if (ModelState.IsValid)
            {
                db.PropertyValues.Add(propertyValue);
                await db.SaveChangesAsync();
            }
        }
        public async Task CreateQuantityValue(QuantityExpressionValue Quantityvalue)
        {
            if (ModelState.IsValid)
            {
                db.QuantityExpressionValues.Add(Quantityvalue);
                await db.SaveChangesAsync();
            }
        }

        public async Task<ChildQuantityExpression> GetQuantityExpression(string partId)
        {
            return await db.ChildQuantityExpressions.Where(c => c.ChildID == partId).Select(e => e).FirstAsync();
        }
        public double SolveQuantityExpression(VariableValue variableValue, ChildQuantityExpression expression)
        {
            return variableValue.Value / expression.Devisor + expression.Constant;
        }
        public async Task<QuantityExpressionValue> GetQuantity(PartStructure partStructure, Order order)
        {
            QuantityExpressionValue value = new QuantityExpressionValue();
            if (partStructure.ISChildQuantityConfigurable == false)
            {
                value.ChildQuantityValue = partStructure.ChildQuantity;
                value.ChildQuantityExpressionId = null;
                value.OrderId = order.Id;
                return value;
            }
            var expression = await GetQuantityExpression(partStructure.ChildID);
            var variableValue = await db.VariableValues.Where(v => v.Id == expression.VariableId).Select(v => v).FirstAsync();
            value.ChildQuantityExpressionId = expression.Id;
            value.ChildQuantityValue = Convert.ToInt32(SolveQuantityExpression(variableValue, expression));
            value.OrderId = order.Id;
            return value;
        }
        public async Task<Job> AssignJob(PartStructure partStructure, Order order)
        {
            var workstation =await GetWorkstation(partStructure.ChildID);
            Job job = new Job
            {
                OrderID = order.Id,
                PartID = partStructure.ChildID,
                Workstation = workstation,
                IsConfirmed = false
            };
            return job;
        }
        public async Task<PickOrder> AssignPickOrder(Job job, string partId, int partQuantity)
        {
            PickOrder pickOrder = new PickOrder
            {
                OrderId = job.OrderID,
                PartId = partId,
                IsConfirmed = false,
                Location =await db.Inventory.Where(i => i.PartID == partId && i.Location != job.Workstation).Select(p => p.Location).FirstAsync(),
                Destination = job.Workstation,
                PartQuantity = partQuantity,
                JobId = job.ID
            };
            return pickOrder;
        }
        public async Task CreatePickOrder(PickOrder pickOrder)
        {
            if (ModelState.IsValid)
            {
                db.PickOrders.Add(pickOrder);
                await db.SaveChangesAsync();
            }
        }
        public async Task StartJobAssignments(Order order)
        {
            var job = await AssignJob(order);
            await CreateJob(job);
            var partStructures =await GetPartStructures(job.PartID);
            foreach (var partStructure in partStructures)
            {
                var partquantity = await GetQuantity(partStructure, order);
                await CreateQuantityValue(partquantity);
                var pickOrder =await AssignPickOrder(job, partStructure.ChildID, partquantity.ChildQuantityValue);
                await CreatePickOrder(pickOrder);
                await AssignChildJobs(partStructure, order, partquantity.ChildQuantityValue);
            }
        }
        public async Task AssignChildJobs(PartStructure partStructure, Order order, int quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                var childStructures = await GetPartStructures(partStructure.ChildID);
                if (childStructures != null && childStructures.Count != 0)
                {


                    var job = await AssignJob(partStructure, order);
                    await CreateJob(job);
                    var properties = await GetPartProperties(partStructure.PartID);
                    foreach (var property in properties)
                    {
                        await CreatePropertyValue(await AssignPropertyValue(property));
                    }
                    foreach (var childStructure in childStructures)
                    {
                        var partquantity = await GetQuantity(childStructure, order);
                        await CreateQuantityValue(partquantity);
                        var pickOrder =await AssignPickOrder(job, childStructure.ChildID, partquantity.ChildQuantityValue);
                        await CreatePickOrder(pickOrder);
                        await AssignChildJobs(childStructure, order, partquantity.ChildQuantityValue);
                    }
                    Session["order"] = order;
                }
                //RedirectToAction("Operations");
            }

        }

    }
}
