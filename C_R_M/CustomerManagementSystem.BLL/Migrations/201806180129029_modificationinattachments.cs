namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificationinattachments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeadPoolAttachments", "Attachment_Name", c => c.String());
            AddColumn("dbo.LeadPoolAttachments", "Saved_Name", c => c.String());
            AddColumn("dbo.LeadPoolAttachments", "Size", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeadPoolAttachments", "Size");
            DropColumn("dbo.LeadPoolAttachments", "Saved_Name");
            DropColumn("dbo.LeadPoolAttachments", "Attachment_Name");
        }
    }
}
