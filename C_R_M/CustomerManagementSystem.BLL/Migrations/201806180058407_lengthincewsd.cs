namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lengthincewsd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LeadPoolAttachments", "Attachment_Path", c => c.String(nullable: false, maxLength: 500, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LeadPoolAttachments", "Attachment_Path", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
    }
}
