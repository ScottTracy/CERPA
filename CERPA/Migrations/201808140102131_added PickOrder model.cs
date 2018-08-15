namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPickOrdermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChildQuantityExpressions", "VariableId", c => c.Int(nullable: false));
            AlterColumn("dbo.ChildQuantityExpressions", "Devisor", c => c.Double(nullable: false));
            AlterColumn("dbo.ChildQuantityExpressions", "Constant", c => c.Double(nullable: false));
            AlterColumn("dbo.PropertyValues", "ExpressionResult", c => c.String());
            AlterColumn("dbo.QuantityExpressionValues", "OrderId", c => c.Int(nullable: false));
            AlterColumn("dbo.QuantityExpressionValues", "ChildQuantityExpressionId", c => c.Int());
            DropColumn("dbo.ChildQuantityExpressions", "Variable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChildQuantityExpressions", "Variable", c => c.String());
            AlterColumn("dbo.QuantityExpressionValues", "ChildQuantityExpressionId", c => c.Int(nullable: false));
            AlterColumn("dbo.QuantityExpressionValues", "OrderId", c => c.String());
            AlterColumn("dbo.PropertyValues", "ExpressionResult", c => c.Double(nullable: false));
            AlterColumn("dbo.ChildQuantityExpressions", "Constant", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ChildQuantityExpressions", "Devisor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ChildQuantityExpressions", "VariableId");
        }
    }
}
