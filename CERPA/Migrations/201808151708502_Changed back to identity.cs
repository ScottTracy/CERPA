namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedbacktoidentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OperationsViewModels", "Order_OrderID", "dbo.Orders");
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "OrderID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Orders", "OrderID");
            AddForeignKey("dbo.OperationsViewModels", "Order_OrderID", "dbo.Orders", "OrderID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OperationsViewModels", "Order_OrderID", "dbo.Orders");
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "OrderID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Orders", "OrderID");
            AddForeignKey("dbo.OperationsViewModels", "Order_OrderID", "dbo.Orders", "OrderID");
        }
    }
}
