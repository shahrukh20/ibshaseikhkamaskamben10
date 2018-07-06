namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gfd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeadPools", "Property_PropertyMasterId", c => c.Int());
            CreateIndex("dbo.LeadPools", "Property_PropertyMasterId");
            AddForeignKey("dbo.LeadPools", "Property_PropertyMasterId", "dbo.PropertyMasters", "PropertyMasterId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeadPools", "Property_PropertyMasterId", "dbo.PropertyMasters");
            DropIndex("dbo.LeadPools", new[] { "Property_PropertyMasterId" });
            DropColumn("dbo.LeadPools", "Property_PropertyMasterId");
        }
    }
}
