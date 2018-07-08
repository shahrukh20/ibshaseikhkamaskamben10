namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vgggcss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Campaigns", "IsActive", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Campaigns", "IsActive");
        }
    }
}
