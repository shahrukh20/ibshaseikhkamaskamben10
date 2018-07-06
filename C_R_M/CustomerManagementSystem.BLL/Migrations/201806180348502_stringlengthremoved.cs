namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringlengthremoved : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LeadPools", "Lead_Name", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.LeadPools", "Source", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.LeadPools", "Lead_Remarks", c => c.String(unicode: false));
            AlterColumn("dbo.LeadPools", "Next_Action_Time", c => c.String(unicode: false));
            AlterColumn("dbo.LeadPools", "Status_Remarks", c => c.String(unicode: false));
            AlterColumn("dbo.LeadPools", "Channel", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LeadPools", "Channel", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.LeadPools", "Status_Remarks", c => c.String(maxLength: 500, unicode: false));
            AlterColumn("dbo.LeadPools", "Next_Action_Time", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.LeadPools", "Lead_Remarks", c => c.String(maxLength: 500, unicode: false));
            AlterColumn("dbo.LeadPools", "Source", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.LeadPools", "Lead_Name", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
    }
}
