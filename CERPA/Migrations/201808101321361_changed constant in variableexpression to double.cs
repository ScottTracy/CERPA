namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedconstantinvariableexpressiontodouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VariableExpressions", "Constant", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VariableExpressions", "Constant", c => c.Int(nullable: false));
        }
    }
}
