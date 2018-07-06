namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaadMigrtion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InquiryDatas",
                c => new
                    {
                        InquiryDataID = c.Int(nullable: false, identity: true),
                        LeadPoolId = c.Int(nullable: false),
                        PropertyTypeID = c.Int(nullable: false),
                        PropertyTypeName = c.String(),
                        Detail = c.String(),
                        Budget = c.String(),
                        ContactInformation = c.String(),
                    })
                .PrimaryKey(t => t.InquiryDataID)
                .ForeignKey("dbo.LeadPools", t => t.LeadPoolId, cascadeDelete: true)
                .Index(t => t.LeadPoolId);
            
            AddColumn("dbo.Users", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.Users", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.Users", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InquiryDatas", "LeadPoolId", "dbo.LeadPools");
            DropIndex("dbo.InquiryDatas", new[] { "LeadPoolId" });
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
            DropTable("dbo.InquiryDatas");
        }
    }
}
