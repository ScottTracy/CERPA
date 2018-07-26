namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedreferencetoConfigurableAssemblyVariableinIdentitymodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConfigurableAssemblyVariables",
                c => new
                    {
                        PartID = c.String(nullable: false, maxLength: 128),
                        VariableName = c.String(),
                        ISRequired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PartID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ConfigurableAssemblyVariables");
        }
    }
}
