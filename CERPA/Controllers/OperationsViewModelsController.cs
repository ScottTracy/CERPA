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
    public class OperationsViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OperationsViewModels
        public  ActionResult ActiveJobs()
        {
            //var orderId = (Int32)Session["Order"];
            //var order = db.Orders.Where(o => o.Id == orderId).Select(o => o).First();
            //await StartJobAssignments(order);
            var activeJobs = db.OperationsViewModels.Where(m => m.Job.IsConfirmed == false|| m.PickOrder.IsConfirmed==false).Select(m => m);
            return View(activeJobs);
        }

        // GET: OperationsViewModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationsViewModel operationsViewModel = await db.OperationsViewModels.FindAsync(id);
            if (operationsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(operationsViewModel);
        }

        // GET: OperationsViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OperationsViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id")] OperationsViewModel operationsViewModel)
        {
            if (ModelState.IsValid)
            {
                db.OperationsViewModels.Add(operationsViewModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(operationsViewModel);
        }

        // GET: OperationsViewModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationsViewModel operationsViewModel = await db.OperationsViewModels.FindAsync(id);
            if (operationsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(operationsViewModel);
        }

        // POST: OperationsViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id")] OperationsViewModel operationsViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operationsViewModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(operationsViewModel);
        }

        // GET: OperationsViewModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperationsViewModel operationsViewModel = await db.OperationsViewModels.FindAsync(id);
            if (operationsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(operationsViewModel);
        }

        // POST: OperationsViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OperationsViewModel operationsViewModel = await db.OperationsViewModels.FindAsync(id);
            db.OperationsViewModels.Remove(operationsViewModel);
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
        //public Job AssignJob(Order order)
        //{
        //    var workstation = GetWorkstation(order.PartID);

        //    Job job = new Job
        //    {
        //        OrderID = order.Id,
        //        PartID = order.PartID,
        //        Workstation = workstation,
        //        IsConfirmed = false
        //    };
        //    return job;
        //}
        //public string GetWorkstation(string PartId)
        //{
        //    return db.PartProcesses.Where(p => p.PartID == PartId).Select(c => c.WorkstationID).First();
        //}
        //public async Task CreateJob(Job job)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Jobs.Add(job);
        //        await db.SaveChangesAsync();
        //    }
        //}
        //public List<PartStructure> GetPartStructures(string PartId)
        //{
        //    return  db.PartStructures.Where(p => p.PartID == PartId).Select(s => s).ToList();
        //}
        //public List<PartProperty> GetPartProperties(string PartId)
        //{
        //    return db.PartProperties.Where(p => p.PartID == PartId).Select(c => c).ToList();
        //}
        //public VariableExpression GetVariableExpression(int PropertyId)
        //{
        //    return db.VariableExpressions.Where(e => e.VariableId == PropertyId).Select(v => v).First();
        //}
        //public double SolveVariableExpression(VariableExpression variableExpression, VariableValue variableValue)
        //{
        //    return variableValue.Value / variableExpression.Devisor + variableExpression.Constant;
        //}
        //public PropertyValue AssignPropertyValue(PartProperty property)
        //{
        //    var order = (Order)Session["Order"];
        //    var variables = (List<ConfigurableAssemblyVariable>)Session["Variables"];
        //    var variableValues = (List<VariableValue>)Session["VariableValues"];

        //    var value = new PropertyValue();
        //    if (property.IsPropertyConfigurable == false)
        //    {
        //        value.ExpressionResult = property.PropertyValue;
        //        value.PropertyId = property.ID;
        //        value.OrderId = order.Id;
        //        return value;
        //    }
        //    var expression = GetVariableExpression(property.ID);
        //    var variableValue = variableValues.Where(v => v.ConfigurableAssemblyVariableId == expression.VariableId).Select(c => c).First();
        //    value.ExpressionResult = (SolveVariableExpression(expression, variableValue)).ToString();
        //    value.PropertyId = property.ID;
        //    value.OrderId = order.Id;
        //    return value;
        //}
        //public async void CreatePropertyValue(PropertyValue propertyValue)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.PropertyValues.Add(propertyValue);
        //        await db.SaveChangesAsync();
        //    }
        //}
        //public async Task CreateQuantityValue(QuantityExpressionValue Quantityvalue)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.QuantityExpressionValues.Add(Quantityvalue);
        //        await db.SaveChangesAsync();
        //    }
        //}

        //public ChildQuantityExpression GetQuantityExpression(string partId)
        //{
        //    return db.ChildQuantityExpressions.Where(c => c.ChildID == partId).Select(e => e).First();
        //}
        //public double SolveQuantityExpression(VariableValue variableValue, ChildQuantityExpression expression)
        //{
        //    return variableValue.Value / expression.Devisor + expression.Constant;
        //}
        //public QuantityExpressionValue GetQuantity(PartStructure partStructure, Order order)
        //{
        //    QuantityExpressionValue value = new QuantityExpressionValue();
        //    if (partStructure.ISChildQuantityConfigurable == false)
        //    {
        //        value.ChildQuantityValue = partStructure.ChildQuantity;
        //        value.ChildQuantityExpressionId = null;
        //        value.OrderId = order.Id;
        //        return value;
        //    }
        //    var expression = GetQuantityExpression(partStructure.ChildID);
        //    var variableValue = db.VariableValues.Where(v => v.Id == expression.VariableId).Select(v => v).First();
        //    value.ChildQuantityExpressionId = expression.Id;
        //    value.ChildQuantityValue = Convert.ToInt32(SolveQuantityExpression(variableValue, expression));
        //    value.OrderId = order.Id;
        //    return value;
        //}
        //public Job AssignJob(PartStructure partStructure, Order order)
        //{
        //    var workstation = GetWorkstation(partStructure.ChildID);
        //    Job job = new Job
        //    {
        //        OrderID = order.Id,
        //        PartID = partStructure.ChildID,
        //        Workstation = workstation,
        //        IsConfirmed = false
        //    };
        //    return job;
        //}
        //public PickOrder AssignPickOrder(Job job, string partId, int partQuantity)
        //{
        //    PickOrder pickOrder = new PickOrder
        //    {
        //        OrderId = job.OrderID,
        //        PartId = partId,
        //        IsConfirmed = false,
        //        Location = db.Inventory.Where(i => i.PartID == partId && i.Location != job.Workstation).Select(p => p.Location).First(),
        //        Destination = job.Workstation,
        //        PartQuantity = partQuantity
        //    };
        //    return pickOrder;
        //}
        //public async void CreatePickOrder(PickOrder pickOrder)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.PickOrders.Add(pickOrder);
        //        await db.SaveChangesAsync();
        //    }
        //}
        //public async  Task StartJobAssignments(Order order)
        //{
        //    var job = AssignJob(order);
        //    await CreateJob(job);
        //    var partStructures = GetPartStructures(job.PartID);
        //    foreach (var partStructure in partStructures)
        //    {
        //        var partquantity = GetQuantity(partStructure, order);
        //        await CreateQuantityValue(partquantity);
        //        var pickOrder = AssignPickOrder(job, partStructure.ChildID, partquantity.ChildQuantityValue);
        //        CreatePickOrder(pickOrder);
        //        await AssignChildJobs(partStructure, order, partquantity.ChildQuantityValue);
        //    }
        //}
        //public async Task AssignChildJobs(PartStructure partStructure, Order order, int quantity)
        //{
        //    for (var i = 0; i < quantity; i++)
        //    {
        //        var childStructures = GetPartStructures(partStructure.ChildID);
        //        if (childStructures != null && childStructures.Count != 0)
        //        {


        //            var job = AssignJob(partStructure, order);
        //            await CreateJob(job);
        //            var properties = GetPartProperties(partStructure.PartID);
        //            foreach (var property in properties)
        //            {
        //                CreatePropertyValue(AssignPropertyValue(property));
        //            }
        //            foreach (var childstructure in childStructures)
        //            {
        //                var partquantity = GetQuantity(partStructure, order);
        //                await CreateQuantityValue(partquantity);
        //                var pickOrder = AssignPickOrder(job, partStructure.ChildID, partquantity.ChildQuantityValue);
        //                CreatePickOrder(pickOrder);
        //                await AssignChildJobs(partStructure, order, partquantity.ChildQuantityValue);
        //            }
        //        }
        //    }

        //}
    }
}
