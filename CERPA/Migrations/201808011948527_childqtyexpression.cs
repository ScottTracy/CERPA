namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class childqtyexpression : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChildQuantityExpressions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChildID = c.String(),
                        Variable = c.String(),
                        Devisor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Constant = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PartStructures", "ChildQuantityExpression_Id", c => c.Int());
            CreateIndex("dbo.PartStructures", "ChildQuantityExpression_Id");
            AddForeignKey("dbo.PartStructures", "ChildQuantityExpression_Id", "dbo.ChildQuantityExpressions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PartStructures", "ChildQuantityExpression_Id", "dbo.ChildQuantityExpressions");
            DropIndex("dbo.PartStructures", new[] { "ChildQuantityExpression_Id" });
            DropColumn("dbo.PartStructures", "ChildQuantityExpression_Id");
            DropTable("dbo.ChildQuantityExpressions");
        }
    }
}
