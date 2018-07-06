namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campaingssupdaterealas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Campaigns", "DateFrom", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Campaigns", "DateTo", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Campaigns", "DateTo", c => c.String());
            AlterColumn("dbo.Campaigns", "DateFrom", c => c.String());
        }
    }
}
