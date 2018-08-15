namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixaddedpickordermigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PickOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        PartId = c.String(),
                        Location = c.String(),
                        Destination = c.String(),
                        Start = c.DateTime(),
                        Confirmed = c.DateTime(),
                        IsConfirmed = c.Boolean(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PickOrders");
        }
    }
}
