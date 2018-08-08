namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpropertyexpression : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VariableExpressions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VariableId = c.Int(nullable: false),
                        Devisor = c.Int(nullable: false),
                        Constant = c.Int(nullable: false),
                        PropertyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VariableExpressions");
        }
    }
}
