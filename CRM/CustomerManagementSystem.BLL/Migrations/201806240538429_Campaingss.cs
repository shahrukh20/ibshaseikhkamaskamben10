namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campaingss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DateFrom = c.String(),
                        DateTo = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(),
                        UpdatedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.UpdatedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            AddColumn("dbo.ActionTypes", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.ActionTypes", "UpdatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.ActionTypes", "CreatedBy_Id", c => c.Int());
            AddColumn("dbo.ActionTypes", "UpdatedBy_Id", c => c.Int());
            AddColumn("dbo.SourceTypes", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.SourceTypes", "UpdatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.SourceTypes", "CreatedBy_Id", c => c.Int());
            AddColumn("dbo.SourceTypes", "UpdatedBy_Id", c => c.Int());
            CreateIndex("dbo.ActionTypes", "CreatedBy_Id");
            CreateIndex("dbo.ActionTypes", "UpdatedBy_Id");
            CreateIndex("dbo.SourceTypes", "CreatedBy_Id");
            CreateIndex("dbo.SourceTypes", "UpdatedBy_Id");
            AddForeignKey("dbo.ActionTypes", "CreatedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.ActionTypes", "UpdatedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.SourceTypes", "CreatedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.SourceTypes", "UpdatedBy_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SourceTypes", "UpdatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.SourceTypes", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Campaigns", "UpdatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Campaigns", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ActionTypes", "UpdatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ActionTypes", "CreatedBy_Id", "dbo.Users");
            DropIndex("dbo.SourceTypes", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.SourceTypes", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Campaigns", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Campaigns", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ActionTypes", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.ActionTypes", new[] { "CreatedBy_Id" });
            DropColumn("dbo.SourceTypes", "UpdatedBy_Id");
            DropColumn("dbo.SourceTypes", "CreatedBy_Id");
            DropColumn("dbo.SourceTypes", "UpdatedOn");
            DropColumn("dbo.SourceTypes", "CreatedOn");
            DropColumn("dbo.ActionTypes", "UpdatedBy_Id");
            DropColumn("dbo.ActionTypes", "CreatedBy_Id");
            DropColumn("dbo.ActionTypes", "UpdatedOn");
            DropColumn("dbo.ActionTypes", "CreatedOn");
            DropTable("dbo.Campaigns");
        }
    }
}
