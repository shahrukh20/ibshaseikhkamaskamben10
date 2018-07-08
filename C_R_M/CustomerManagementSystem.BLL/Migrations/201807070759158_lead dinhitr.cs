namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leaddinhitr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeadHistories", "LeadId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeadHistories", "LeadId");
        }
    }
}
