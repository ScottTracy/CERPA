namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedapplicationgroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationGroups",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationGroupRoles",
                c => new
                    {
                        ApplicationRoleId = c.String(nullable: false, maxLength: 128),
                        ApplicationGroupId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationRoleId, t.ApplicationGroupId })
                .ForeignKey("dbo.ApplicationGroups", t => t.ApplicationGroupId, cascadeDelete: true)
                .Index(t => t.ApplicationGroupId);
            
            CreateTable(
                "dbo.ApplicationUserGroups",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        ApplicationGroupId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUserId, t.ApplicationGroupId })
                .ForeignKey("dbo.ApplicationGroups", t => t.ApplicationGroupId, cascadeDelete: true)
                .Index(t => t.ApplicationGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserGroups", "ApplicationGroupId", "dbo.ApplicationGroups");
            DropForeignKey("dbo.ApplicationGroupRoles", "ApplicationGroupId", "dbo.ApplicationGroups");
            DropIndex("dbo.ApplicationUserGroups", new[] { "ApplicationGroupId" });
            DropIndex("dbo.ApplicationGroupRoles", new[] { "ApplicationGroupId" });
            DropTable("dbo.ApplicationUserGroups");
            DropTable("dbo.ApplicationGroupRoles");
            DropTable("dbo.ApplicationGroups");
        }
    }
}
