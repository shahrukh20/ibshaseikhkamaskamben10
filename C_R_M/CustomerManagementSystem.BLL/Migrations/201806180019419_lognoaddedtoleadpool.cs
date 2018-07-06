namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lognoaddedtoleadpool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeadPools", "LogNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeadPools", "LogNo");
        }
    }
}
