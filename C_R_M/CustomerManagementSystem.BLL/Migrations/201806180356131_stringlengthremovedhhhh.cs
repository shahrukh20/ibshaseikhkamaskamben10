namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringlengthremovedhhhh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeadHistories", "CurrentStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeadHistories", "CurrentStatus");
        }
    }
}
