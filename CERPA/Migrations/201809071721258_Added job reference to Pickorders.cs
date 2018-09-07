namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedjobreferencetoPickorders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PickOrders", "JobId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PickOrders", "JobId");
        }
    }
}
