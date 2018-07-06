namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statusidchanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LeadPools", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LeadPools", "Status", c => c.Int());
        }
    }
}
