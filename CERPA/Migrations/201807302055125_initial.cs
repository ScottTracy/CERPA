namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
            
            CreateTable(
                "dbo.AssemblyProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Items_ID = c.Int(),
                        Processes_ID = c.Int(),
                        Structures_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InventoryItems", t => t.Items_ID)
                .ForeignKey("dbo.PartProcesses", t => t.Processes_ID)
                .ForeignKey("dbo.PartStructures", t => t.Structures_ID)
                .Index(t => t.Items_ID)
                .Index(t => t.Processes_ID)
                .Index(t => t.Structures_ID);
            
            CreateTable(
                "dbo.InventoryItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PartID = c.String(),
                        Location = c.String(),
                        LastConfirmed = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ReorderPoint = c.Int(nullable: false),
                        ReorderQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PartProcesses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PartID = c.String(),
                        WorkstationID = c.String(),
                        ProcessTime = c.Time(nullable: false, precision: 7),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PartProperties",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PartID = c.String(),
                        PropertyName = c.String(),
                        IsPropertyConfigurable = c.Boolean(nullable: false),
                        PropertyValue = c.String(),
                        AssemblyProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AssemblyProfiles", t => t.AssemblyProfile_Id)
                .Index(t => t.AssemblyProfile_Id);
            
            CreateTable(
                "dbo.PartStructures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PartID = c.String(),
                        ChildID = c.String(),
                        ISChildQuantityConfigurable = c.Boolean(nullable: false),
                        ChildQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ConfigurableAssemblyVariables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PartID = c.String(),
                        VariableName = c.String(),
                        ISRequired = c.Boolean(nullable: false),
                        AssemblyProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AssemblyProfiles", t => t.AssemblyProfile_Id)
                .Index(t => t.AssemblyProfile_Id);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.String(),
                        Workstation = c.String(),
                        PartID = c.String(),
                        Start = c.DateTime(nullable: false),
                        Confirmed = c.DateTime(nullable: false),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.String(nullable: false, maxLength: 128),
                        PartID = c.String(),
                        UserID = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ConfigurableAssemblyVariables", "AssemblyProfile_Id", "dbo.AssemblyProfiles");
            DropForeignKey("dbo.AssemblyProfiles", "Structures_ID", "dbo.PartStructures");
            DropForeignKey("dbo.PartProperties", "AssemblyProfile_Id", "dbo.AssemblyProfiles");
            DropForeignKey("dbo.AssemblyProfiles", "Processes_ID", "dbo.PartProcesses");
            DropForeignKey("dbo.AssemblyProfiles", "Items_ID", "dbo.InventoryItems");
            DropForeignKey("dbo.ApplicationUserGroups", "ApplicationGroupId", "dbo.ApplicationGroups");
            DropForeignKey("dbo.ApplicationGroupRoles", "ApplicationGroupId", "dbo.ApplicationGroups");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ConfigurableAssemblyVariables", new[] { "AssemblyProfile_Id" });
            DropIndex("dbo.PartProperties", new[] { "AssemblyProfile_Id" });
            DropIndex("dbo.AssemblyProfiles", new[] { "Structures_ID" });
            DropIndex("dbo.AssemblyProfiles", new[] { "Processes_ID" });
            DropIndex("dbo.AssemblyProfiles", new[] { "Items_ID" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "ApplicationGroupId" });
            DropIndex("dbo.ApplicationGroupRoles", new[] { "ApplicationGroupId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Orders");
            DropTable("dbo.Jobs");
            DropTable("dbo.ConfigurableAssemblyVariables");
            DropTable("dbo.PartStructures");
            DropTable("dbo.PartProperties");
            DropTable("dbo.PartProcesses");
            DropTable("dbo.InventoryItems");
            DropTable("dbo.AssemblyProfiles");
            DropTable("dbo.ApplicationUserGroups");
            DropTable("dbo.ApplicationGroupRoles");
            DropTable("dbo.ApplicationGroups");
        }
    }
}
