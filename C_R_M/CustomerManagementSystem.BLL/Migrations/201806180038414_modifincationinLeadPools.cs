namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifincationinLeadPools : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeadPools", "Contact", c => c.String());
            AddColumn("dbo.LeadPools", "Telephone", c => c.String());
            AddColumn("dbo.LeadPools", "Designation", c => c.String());
            AddColumn("dbo.LeadPools", "Email", c => c.String());
            AddColumn("dbo.LeadPools", "Fax", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeadPools", "Fax");
            DropColumn("dbo.LeadPools", "Email");
            DropColumn("dbo.LeadPools", "Designation");
            DropColumn("dbo.LeadPools", "Telephone");
            DropColumn("dbo.LeadPools", "Contact");
        }
    }
}
