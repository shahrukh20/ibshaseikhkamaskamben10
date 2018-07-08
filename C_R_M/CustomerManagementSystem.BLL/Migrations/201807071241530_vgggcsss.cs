namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vgggcsss : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Campaigns", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Campaigns", "IsActive", c => c.Boolean());
        }
    }
}
