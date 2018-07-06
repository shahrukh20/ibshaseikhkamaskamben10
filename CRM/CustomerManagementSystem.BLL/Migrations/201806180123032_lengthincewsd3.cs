namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lengthincewsd3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LeadPoolAttachments", "Lead_Pool_Id", "dbo.LeadPools");
            DropForeignKey("dbo.LeadPoolContacts", "Lead_Pool_Id", "dbo.LeadPools");
            DropIndex("dbo.LeadPoolAttachments", new[] { "Lead_Pool_Id" });
            DropIndex("dbo.LeadPoolContacts", new[] { "Lead_Pool_Id" });
            AddColumn("dbo.LeadPoolAttachments", "Lead_Pool_Id1", c => c.Int());
            AddColumn("dbo.LeadPoolContacts", "Lead_Pool_Id1", c => c.Int());
            CreateIndex("dbo.LeadPoolAttachments", "Lead_Pool_Id1");
            CreateIndex("dbo.LeadPoolContacts", "Lead_Pool_Id1");
            AddForeignKey("dbo.LeadPoolAttachments", "Lead_Pool_Id1", "dbo.LeadPools", "Id");
            AddForeignKey("dbo.LeadPoolContacts", "Lead_Pool_Id1", "dbo.LeadPools", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeadPoolContacts", "Lead_Pool_Id1", "dbo.LeadPools");
            DropForeignKey("dbo.LeadPoolAttachments", "Lead_Pool_Id1", "dbo.LeadPools");
            DropIndex("dbo.LeadPoolContacts", new[] { "Lead_Pool_Id1" });
            DropIndex("dbo.LeadPoolAttachments", new[] { "Lead_Pool_Id1" });
            DropColumn("dbo.LeadPoolContacts", "Lead_Pool_Id1");
            DropColumn("dbo.LeadPoolAttachments", "Lead_Pool_Id1");
            CreateIndex("dbo.LeadPoolContacts", "Lead_Pool_Id");
            CreateIndex("dbo.LeadPoolAttachments", "Lead_Pool_Id");
            AddForeignKey("dbo.LeadPoolContacts", "Lead_Pool_Id", "dbo.LeadPools", "Id");
            AddForeignKey("dbo.LeadPoolAttachments", "Lead_Pool_Id", "dbo.LeadPools", "Id");
        }
    }
}
