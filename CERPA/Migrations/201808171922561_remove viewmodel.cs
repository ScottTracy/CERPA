namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeviewmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OperationsViewModels", "AssemblyProfile_Id", "dbo.AssemblyProfiles");
            DropForeignKey("dbo.OperationsViewModels", "ChildQuantityExpression_Id", "dbo.ChildQuantityExpressions");
            DropForeignKey("dbo.OperationsViewModels", "ConfigurableAssemblyVariable_ID", "dbo.ConfigurableAssemblyVariables");
            DropForeignKey("dbo.OperationsViewModels", "InventoryItem_ID", "dbo.InventoryItems");
            DropForeignKey("dbo.OperationsViewModels", "Job_ID", "dbo.Jobs");
            DropForeignKey("dbo.OperationsViewModels", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OperationsViewModels", "PartProcess_ID", "dbo.PartProcesses");
            DropForeignKey("dbo.OperationsViewModels", "PartProperty_ID", "dbo.PartProperties");
            DropForeignKey("dbo.OperationsViewModels", "PartStructure_ID", "dbo.PartStructures");
            DropForeignKey("dbo.OperationsViewModels", "PickOrder_Id", "dbo.PickOrders");
            DropForeignKey("dbo.OperationsViewModels", "PropertyValue_Id", "dbo.PropertyValues");
            DropForeignKey("dbo.OperationsViewModels", "QuantityExpressionValue_Id", "dbo.QuantityExpressionValues");
            DropForeignKey("dbo.OperationsViewModels", "VariableExpression_Id", "dbo.VariableExpressions");
            DropForeignKey("dbo.OperationsViewModels", "VariableValue_Id", "dbo.VariableValues");
            DropIndex("dbo.OperationsViewModels", new[] { "AssemblyProfile_Id" });
            DropIndex("dbo.OperationsViewModels", new[] { "ChildQuantityExpression_Id" });
            DropIndex("dbo.OperationsViewModels", new[] { "ConfigurableAssemblyVariable_ID" });
            DropIndex("dbo.OperationsViewModels", new[] { "InventoryItem_ID" });
            DropIndex("dbo.OperationsViewModels", new[] { "Job_ID" });
            DropIndex("dbo.OperationsViewModels", new[] { "Order_Id" });
            DropIndex("dbo.OperationsViewModels", new[] { "PartProcess_ID" });
            DropIndex("dbo.OperationsViewModels", new[] { "PartProperty_ID" });
            DropIndex("dbo.OperationsViewModels", new[] { "PartStructure_ID" });
            DropIndex("dbo.OperationsViewModels", new[] { "PickOrder_Id" });
            DropIndex("dbo.OperationsViewModels", new[] { "PropertyValue_Id" });
            DropIndex("dbo.OperationsViewModels", new[] { "QuantityExpressionValue_Id" });
            DropIndex("dbo.OperationsViewModels", new[] { "VariableExpression_Id" });
            DropIndex("dbo.OperationsViewModels", new[] { "VariableValue_Id" });
            DropTable("dbo.OperationsViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OperationsViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssemblyProfile_Id = c.Int(),
                        ChildQuantityExpression_Id = c.Int(),
                        ConfigurableAssemblyVariable_ID = c.Int(),
                        InventoryItem_ID = c.Int(),
                        Job_ID = c.Int(),
                        Order_Id = c.Int(),
                        PartProcess_ID = c.Int(),
                        PartProperty_ID = c.Int(),
                        PartStructure_ID = c.Int(),
                        PickOrder_Id = c.Int(),
                        PropertyValue_Id = c.Int(),
                        QuantityExpressionValue_Id = c.Int(),
                        VariableExpression_Id = c.Int(),
                        VariableValue_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.OperationsViewModels", "VariableValue_Id");
            CreateIndex("dbo.OperationsViewModels", "VariableExpression_Id");
            CreateIndex("dbo.OperationsViewModels", "QuantityExpressionValue_Id");
            CreateIndex("dbo.OperationsViewModels", "PropertyValue_Id");
            CreateIndex("dbo.OperationsViewModels", "PickOrder_Id");
            CreateIndex("dbo.OperationsViewModels", "PartStructure_ID");
            CreateIndex("dbo.OperationsViewModels", "PartProperty_ID");
            CreateIndex("dbo.OperationsViewModels", "PartProcess_ID");
            CreateIndex("dbo.OperationsViewModels", "Order_Id");
            CreateIndex("dbo.OperationsViewModels", "Job_ID");
            CreateIndex("dbo.OperationsViewModels", "InventoryItem_ID");
            CreateIndex("dbo.OperationsViewModels", "ConfigurableAssemblyVariable_ID");
            CreateIndex("dbo.OperationsViewModels", "ChildQuantityExpression_Id");
            CreateIndex("dbo.OperationsViewModels", "AssemblyProfile_Id");
            AddForeignKey("dbo.OperationsViewModels", "VariableValue_Id", "dbo.VariableValues", "Id");
            AddForeignKey("dbo.OperationsViewModels", "VariableExpression_Id", "dbo.VariableExpressions", "Id");
            AddForeignKey("dbo.OperationsViewModels", "QuantityExpressionValue_Id", "dbo.QuantityExpressionValues", "Id");
            AddForeignKey("dbo.OperationsViewModels", "PropertyValue_Id", "dbo.PropertyValues", "Id");
            AddForeignKey("dbo.OperationsViewModels", "PickOrder_Id", "dbo.PickOrders", "Id");
            AddForeignKey("dbo.OperationsViewModels", "PartStructure_ID", "dbo.PartStructures", "ID");
            AddForeignKey("dbo.OperationsViewModels", "PartProperty_ID", "dbo.PartProperties", "ID");
            AddForeignKey("dbo.OperationsViewModels", "PartProcess_ID", "dbo.PartProcesses", "ID");
            AddForeignKey("dbo.OperationsViewModels", "Order_Id", "dbo.Orders", "Id");
            AddForeignKey("dbo.OperationsViewModels", "Job_ID", "dbo.Jobs", "ID");
            AddForeignKey("dbo.OperationsViewModels", "InventoryItem_ID", "dbo.InventoryItems", "ID");
            AddForeignKey("dbo.OperationsViewModels", "ConfigurableAssemblyVariable_ID", "dbo.ConfigurableAssemblyVariables", "ID");
            AddForeignKey("dbo.OperationsViewModels", "ChildQuantityExpression_Id", "dbo.ChildQuantityExpressions", "Id");
            AddForeignKey("dbo.OperationsViewModels", "AssemblyProfile_Id", "dbo.AssemblyProfiles", "Id");
        }
    }
}
