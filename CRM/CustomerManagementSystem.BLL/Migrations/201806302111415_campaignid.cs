namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campaignid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropertyMasters", "CampaignId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropertyMasters", "CampaignId");
        }
    }
}
