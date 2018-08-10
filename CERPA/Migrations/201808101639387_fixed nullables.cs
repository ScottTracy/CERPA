namespace CERPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixednullables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "Start", c => c.DateTime());
            AlterColumn("dbo.Jobs", "ConfirmedOn", c => c.DateTime());
            AlterColumn("dbo.Orders", "ConfirmedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ConfirmedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Jobs", "ConfirmedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Jobs", "Start", c => c.DateTime(nullable: false));
        }
    }
}
