namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campaingssupdaterealassxc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Campaigns", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Campaigns", "UpdatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Campaigns", "CreatedBy_Id", c => c.Int());
            AddColumn("dbo.Campaigns", "UpdatedBy_Id", c => c.Int());
            CreateIndex("dbo.Campaigns", "CreatedBy_Id");
            CreateIndex("dbo.Campaigns", "UpdatedBy_Id");
            AddForeignKey("dbo.Campaigns", "CreatedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Campaigns", "UpdatedBy_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Campaigns", "UpdatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Campaigns", "CreatedBy_Id", "dbo.Users");
            DropIndex("dbo.Campaigns", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Campaigns", new[] { "CreatedBy_Id" });
            DropColumn("dbo.Campaigns", "UpdatedBy_Id");
            DropColumn("dbo.Campaigns", "CreatedBy_Id");
            DropColumn("dbo.Campaigns", "UpdatedOn");
            DropColumn("dbo.Campaigns", "CreatedOn");
        }
    }
}
