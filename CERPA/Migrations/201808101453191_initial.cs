namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            
           
           
           
            CreateTable(
                "dbo.PropertyValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.String(),
                        PropertyId = c.Int(nullable: false),
                        ExpressionResult = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuantityExpressionValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.String(),
                        ChildQuantityExpressionId = c.Int(nullable: false),
                        ChildQuantityValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            
            
            CreateTable(
                "dbo.VariableValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.String(),
                        ConfigurableAssemblyVariableId = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            
        }
    }
}
