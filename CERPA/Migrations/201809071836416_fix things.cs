namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixthings : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PickOrders", "JobId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PickOrders", "JobId", c => c.String());
        }
    }
}
