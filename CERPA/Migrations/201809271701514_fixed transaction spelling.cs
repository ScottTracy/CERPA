namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedtransactionspelling : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.InventoryTansactions", newName: "InventoryTransactions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.InventoryTransactions", newName: "InventoryTansactions");
        }
    }
}
