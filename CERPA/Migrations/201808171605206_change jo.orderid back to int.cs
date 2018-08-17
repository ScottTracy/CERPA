namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changejoorderidbacktoint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "OrderID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "OrderID", c => c.String());
        }
    }
}
