namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedorderidtoid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OperationsViewModels", "Order_OrderID", "dbo.Orders");
            RenameColumn(table: "dbo.OperationsViewModels", name: "Order_OrderID", newName: "Order_Id");
            RenameIndex(table: "dbo.OperationsViewModels", name: "IX_Order_OrderID", newName: "IX_Order_Id");
            DropPrimaryKey("dbo.Orders");
            AddColumn("dbo.Orders", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Orders", "Id");
            AddForeignKey("dbo.OperationsViewModels", "Order_Id", "dbo.Orders", "Id");
            DropColumn("dbo.Orders", "OrderID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.OperationsViewModels", "Order_Id", "dbo.Orders");
            DropPrimaryKey("dbo.Orders");
            DropColumn("dbo.Orders", "Id");
            AddPrimaryKey("dbo.Orders", "OrderID");
            RenameIndex(table: "dbo.OperationsViewModels", name: "IX_Order_Id", newName: "IX_Order_OrderID");
            RenameColumn(table: "dbo.OperationsViewModels", name: "Order_Id", newName: "Order_OrderID");
            AddForeignKey("dbo.OperationsViewModels", "Order_OrderID", "dbo.Orders", "OrderID");
        }
    }
}
