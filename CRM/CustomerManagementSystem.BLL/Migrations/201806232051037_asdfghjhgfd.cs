namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdfghjhgfd : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.LeadPools", name: "Property_PropertyMasterId", newName: "PropertyMaster_PropertyMasterId");
            RenameIndex(table: "dbo.LeadPools", name: "IX_Property_PropertyMasterId", newName: "IX_PropertyMaster_PropertyMasterId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.LeadPools", name: "IX_PropertyMaster_PropertyMasterId", newName: "IX_Property_PropertyMasterId");
            RenameColumn(table: "dbo.LeadPools", name: "PropertyMaster_PropertyMasterId", newName: "Property_PropertyMasterId");
        }
    }
}
