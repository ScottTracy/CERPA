namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unkowndbchange : DbMigration
    {
        public override void Up()
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
                        Order_OrderID = c.Int(),
                        PartProcess_ID = c.Int(),
                        PartProperty_ID = c.Int(),
                        PartStructure_ID = c.Int(),
                        PickOrder_Id = c.Int(),
                        PropertyValue_Id = c.Int(),
                        QuantityExpressionValue_Id = c.Int(),
                        VariableExpression_Id = c.Int(),
                        VariableValue_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssemblyProfiles", t => t.AssemblyProfile_Id)
                .ForeignKey("dbo.ChildQuantityExpressions", t => t.ChildQuantityExpression_Id)
                .ForeignKey("dbo.ConfigurableAssemblyVariables", t => t.ConfigurableAssemblyVariable_ID)
                .ForeignKey("dbo.InventoryItems", t => t.InventoryItem_ID)
                .ForeignKey("dbo.Jobs", t => t.Job_ID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .ForeignKey("dbo.PartProcesses", t => t.PartProcess_ID)
                .ForeignKey("dbo.PartProperties", t => t.PartProperty_ID)
                .ForeignKey("dbo.PartStructures", t => t.PartStructure_ID)
                .ForeignKey("dbo.PickOrders", t => t.PickOrder_Id)
                .ForeignKey("dbo.PropertyValues", t => t.PropertyValue_Id)
                .ForeignKey("dbo.QuantityExpressionValues", t => t.QuantityExpressionValue_Id)
                .ForeignKey("dbo.VariableExpressions", t => t.VariableExpression_Id)
                .ForeignKey("dbo.VariableValues", t => t.VariableValue_Id)
                .Index(t => t.AssemblyProfile_Id)
                .Index(t => t.ChildQuantityExpression_Id)
                .Index(t => t.ConfigurableAssemblyVariable_ID)
                .Index(t => t.InventoryItem_ID)
                .Index(t => t.Job_ID)
                .Index(t => t.Order_OrderID)
                .Index(t => t.PartProcess_ID)
                .Index(t => t.PartProperty_ID)
                .Index(t => t.PartStructure_ID)
                .Index(t => t.PickOrder_Id)
                .Index(t => t.PropertyValue_Id)
                .Index(t => t.QuantityExpressionValue_Id)
                .Index(t => t.VariableExpression_Id)
                .Index(t => t.VariableValue_Id);
            
            AddColumn("dbo.PickOrders", "PartQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OperationsViewModels", "VariableValue_Id", "dbo.VariableValues");
            DropForeignKey("dbo.OperationsViewModels", "VariableExpression_Id", "dbo.VariableExpressions");
            DropForeignKey("dbo.OperationsViewModels", "QuantityExpressionValue_Id", "dbo.QuantityExpressionValues");
            DropForeignKey("dbo.OperationsViewModels", "PropertyValue_Id", "dbo.PropertyValues");
            DropForeignKey("dbo.OperationsViewModels", "PickOrder_Id", "dbo.PickOrders");
            DropForeignKey("dbo.OperationsViewModels", "PartStructure_ID", "dbo.PartStructures");
            DropForeignKey("dbo.OperationsViewModels", "PartProperty_ID", "dbo.PartProperties");
            DropForeignKey("dbo.OperationsViewModels", "PartProcess_ID", "dbo.PartProcesses");
            DropForeignKey("dbo.OperationsViewModels", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.OperationsViewModels", "Job_ID", "dbo.Jobs");
            DropForeignKey("dbo.OperationsViewModels", "InventoryItem_ID", "dbo.InventoryItems");
            DropForeignKey("dbo.OperationsViewModels", "ConfigurableAssemblyVariable_ID", "dbo.ConfigurableAssemblyVariables");
            DropForeignKey("dbo.OperationsViewModels", "ChildQuantityExpression_Id", "dbo.ChildQuantityExpressions");
            DropForeignKey("dbo.OperationsViewModels", "AssemblyProfile_Id", "dbo.AssemblyProfiles");
            DropIndex("dbo.OperationsViewModels", new[] { "VariableValue_Id" });
            DropIndex("dbo.OperationsViewModels", new[] { "VariableExpression_Id" });
            DropIndex("dbo.OperationsViewModels", new[] { "QuantityExpressionValue_Id" });
            DropIndex("dbo.OperationsViewModels", new[] { "PropertyValue_Id" });
            DropIndex("dbo.OperationsViewModels", new[] { "PickOrder_Id" });
            DropIndex("dbo.OperationsViewModels", new[] { "PartStructure_ID" });
            DropIndex("dbo.OperationsViewModels", new[] { "PartProperty_ID" });
            DropIndex("dbo.OperationsViewModels", new[] { "PartProcess_ID" });
            DropIndex("dbo.OperationsViewModels", new[] { "Order_OrderID" });
            DropIndex("dbo.OperationsViewModels", new[] { "Job_ID" });
            DropIndex("dbo.OperationsViewModels", new[] { "InventoryItem_ID" });
            DropIndex("dbo.OperationsViewModels", new[] { "ConfigurableAssemblyVariable_ID" });
            DropIndex("dbo.OperationsViewModels", new[] { "ChildQuantityExpression_Id" });
            DropIndex("dbo.OperationsViewModels", new[] { "AssemblyProfile_Id" });
            DropColumn("dbo.PickOrders", "PartQuantity");
            DropTable("dbo.OperationsViewModels");
        }
    }
}
