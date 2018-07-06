namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatwssd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActionTypes", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.ActionTypes", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Campaigns", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Campaigns", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.SourceTypes", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.SourceTypes", "UpdatedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SourceTypes", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SourceTypes", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Campaigns", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Campaigns", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActionTypes", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActionTypes", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
