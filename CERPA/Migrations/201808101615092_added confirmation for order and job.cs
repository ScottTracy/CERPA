namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedconfirmationfororderandjob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "ConfirmedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jobs", "IsConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "ConfirmedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "IsConfirmed", c => c.Boolean(nullable: false));
            DropColumn("dbo.Jobs", "Confirmed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "Confirmed", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "IsConfirmed");
            DropColumn("dbo.Orders", "ConfirmedOn");
            DropColumn("dbo.Jobs", "IsConfirmed");
            DropColumn("dbo.Jobs", "ConfirmedOn");
        }
    }
}
