namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jhfs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropertyMasters",
                c => new
                    {
                        PropertyMasterId = c.Int(nullable: false, identity: true),
                        PropertyName = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Area = c.String(nullable: false),
                        PropertyType = c.String(nullable: false),
                        PropertyDetail = c.String(nullable: false),
                        PlotNo = c.String(),
                        PlotArea = c.String(),
                        BuiltUpArea = c.String(),
                        CommercialArea = c.String(),
                        ResidentialArea = c.String(),
                        NoOfFloors = c.Int(nullable: false),
                        PropertyOwnerName = c.String(nullable: false),
                        ContactNumber = c.String(nullable: false),
                        SellingPrice = c.Int(nullable: false),
                        ImageJson = c.String(),
                    })
                .PrimaryKey(t => t.PropertyMasterId);
            
            AddColumn("dbo.LeadPools", "Score", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeadPools", "Score");
            DropTable("dbo.PropertyMasters");
        }
    }
}
