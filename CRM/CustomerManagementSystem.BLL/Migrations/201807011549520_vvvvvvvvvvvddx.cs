namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vvvvvvvvvvvddx : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Salesman", "salesmenCategory_Id", "dbo.SalesmenCategories");
            DropForeignKey("dbo.Salesman", "targetPeriod_Id", "dbo.TargetPeriods");
            DropForeignKey("dbo.Salesman", "targetType_Id", "dbo.TargetTypes");
            DropIndex("dbo.Salesman", new[] { "salesmenCategory_Id" });
            DropIndex("dbo.Salesman", new[] { "targetPeriod_Id" });
            DropIndex("dbo.Salesman", new[] { "targetType_Id" });
            AlterColumn("dbo.UserTypes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Areas", "AreaName", c => c.String(nullable: false));
            AlterColumn("dbo.Campaigns", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Currencies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Salesman", "Base_Target", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Salesman", "ValueLead", c => c.String(nullable: false));
            AlterColumn("dbo.Salesman", "salesmenCategory_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Salesman", "targetPeriod_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Salesman", "targetType_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.SalesmenCategories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.TargetPeriods", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.TargetTypes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.SourceOfLeads", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Salesman", "salesmenCategory_Id");
            CreateIndex("dbo.Salesman", "targetPeriod_Id");
            CreateIndex("dbo.Salesman", "targetType_Id");
            AddForeignKey("dbo.Salesman", "salesmenCategory_Id", "dbo.SalesmenCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Salesman", "targetPeriod_Id", "dbo.TargetPeriods", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Salesman", "targetType_Id", "dbo.TargetTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Salesman", "targetType_Id", "dbo.TargetTypes");
            DropForeignKey("dbo.Salesman", "targetPeriod_Id", "dbo.TargetPeriods");
            DropForeignKey("dbo.Salesman", "salesmenCategory_Id", "dbo.SalesmenCategories");
            DropIndex("dbo.Salesman", new[] { "targetType_Id" });
            DropIndex("dbo.Salesman", new[] { "targetPeriod_Id" });
            DropIndex("dbo.Salesman", new[] { "salesmenCategory_Id" });
            AlterColumn("dbo.SourceOfLeads", "Name", c => c.String());
            AlterColumn("dbo.TargetTypes", "Name", c => c.String());
            AlterColumn("dbo.TargetPeriods", "Name", c => c.String());
            AlterColumn("dbo.SalesmenCategories", "Name", c => c.String());
            AlterColumn("dbo.Salesman", "targetType_Id", c => c.Int());
            AlterColumn("dbo.Salesman", "targetPeriod_Id", c => c.Int());
            AlterColumn("dbo.Salesman", "salesmenCategory_Id", c => c.Int());
            AlterColumn("dbo.Salesman", "ValueLead", c => c.String());
            AlterColumn("dbo.Salesman", "Base_Target", c => c.String(unicode: false));
            AlterColumn("dbo.Currencies", "Name", c => c.String());
            AlterColumn("dbo.Campaigns", "Name", c => c.String());
            AlterColumn("dbo.Areas", "AreaName", c => c.String());
            AlterColumn("dbo.UserTypes", "Name", c => c.String());
            CreateIndex("dbo.Salesman", "targetType_Id");
            CreateIndex("dbo.Salesman", "targetPeriod_Id");
            CreateIndex("dbo.Salesman", "salesmenCategory_Id");
            AddForeignKey("dbo.Salesman", "targetType_Id", "dbo.TargetTypes", "Id");
            AddForeignKey("dbo.Salesman", "targetPeriod_Id", "dbo.TargetPeriods", "Id");
            AddForeignKey("dbo.Salesman", "salesmenCategory_Id", "dbo.SalesmenCategories", "Id");
        }
    }
}
