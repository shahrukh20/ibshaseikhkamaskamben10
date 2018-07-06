namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificationsinallstrctures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LeadHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LeadPool = c.String(),
                        LeadStatusFields = c.String(),
                        LeadPoolAttachment = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LeadStatusFields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusField = c.String(),
                        NextAction = c.String(),
                        Contact1 = c.String(),
                        Contact2 = c.String(),
                        Remarks = c.String(),
                        StatusEnum = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        leadPool_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LeadPools", t => t.leadPool_Id)
                .Index(t => t.leadPool_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeadStatusFields", "leadPool_Id", "dbo.LeadPools");
            DropIndex("dbo.LeadStatusFields", new[] { "leadPool_Id" });
            DropTable("dbo.LeadStatusFields");
            DropTable("dbo.LeadHistories");
        }
    }
}
