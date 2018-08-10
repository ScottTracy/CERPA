namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixorderiddatatypeinVariableValue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VariableValues", "OrderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VariableValues", "OrderId", c => c.String());
        }
    }
}
