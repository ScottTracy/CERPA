namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventoryTransactions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryTansactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InventoryItem = c.String(),
                        UserId = c.String(),
                        JobId = c.Int(),
                        TimeStamp = c.DateTime(nullable: false),
                        TransactionType = c.String(),
                        Quantity = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InventoryTansactions");
        }
    }
}
