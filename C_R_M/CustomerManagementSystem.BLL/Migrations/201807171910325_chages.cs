namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyCompanyInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MyCompanyName = c.String(),
                        MyCompanyLogo = c.String(),
                        MyCompanyMiniatureLogo = c.String(),
                        MyCompanySlogan = c.String(),
                        FiscalYearStartDate = c.DateTime(),
                        CurrencyId = c.Int(nullable: false),
                        DefaultPhoneCode = c.String(),
                        MyCompanyAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "CountryCode", c => c.String());
            AddColumn("dbo.Users", "CountryId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "CityId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CityId");
            DropColumn("dbo.Users", "CountryId");
            DropColumn("dbo.Users", "CountryCode");
            DropTable("dbo.MyCompanyInfoes");
        }
    }
}
