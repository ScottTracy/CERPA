namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderchangeidtoint : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "OrderID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Orders", "OrderID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "OrderID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Orders", "OrderID");
        }
    }
}
