namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sfhvcddfff : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Salesman", new[] { "User_Id" });
            CreateTable(
                "dbo.SalesmenCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TargetPeriods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TargetTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Salesman", "ValueLead", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Salesman", "salesmenCategory_Id", c => c.Int());
            AddColumn("dbo.Salesman", "targetPeriod_Id", c => c.Int());
            AddColumn("dbo.Salesman", "targetType_Id", c => c.Int());
            AlterColumn("dbo.Salesman", "Base_Target", c => c.String(unicode: false));
            CreateIndex("dbo.Salesman", "salesmenCategory_Id");
            CreateIndex("dbo.Salesman", "targetPeriod_Id");
            CreateIndex("dbo.Salesman", "targetType_Id");
            CreateIndex("dbo.Salesman", "user_Id");
            AddForeignKey("dbo.Salesman", "salesmenCategory_Id", "dbo.SalesmenCategories", "Id");
            AddForeignKey("dbo.Salesman", "targetPeriod_Id", "dbo.TargetPeriods", "Id");
            AddForeignKey("dbo.Salesman", "targetType_Id", "dbo.TargetTypes", "Id");
            DropColumn("dbo.Salesman", "title");
            DropColumn("dbo.Salesman", "First_Name");
            DropColumn("dbo.Salesman", "Last_Name");
            DropColumn("dbo.Salesman", "Salesman_Type_ID");
            DropColumn("dbo.Salesman", "Reporting_To_ID");
            DropColumn("dbo.Salesman", "Target_Period");
            DropColumn("dbo.Salesman", "Target_Type");
            DropColumn("dbo.Salesman", "No_Of_Base_Target");
            DropColumn("dbo.Salesman", "Comission_Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Salesman", "Comission_Type", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.Salesman", "No_Of_Base_Target", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.Salesman", "Target_Type", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.Salesman", "Target_Period", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.Salesman", "Reporting_To_ID", c => c.Int());
            AddColumn("dbo.Salesman", "Salesman_Type_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Salesman", "Last_Name", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("dbo.Salesman", "First_Name", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("dbo.Salesman", "title", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropForeignKey("dbo.Salesman", "targetType_Id", "dbo.TargetTypes");
            DropForeignKey("dbo.Salesman", "targetPeriod_Id", "dbo.TargetPeriods");
            DropForeignKey("dbo.Salesman", "salesmenCategory_Id", "dbo.SalesmenCategories");
            DropIndex("dbo.Salesman", new[] { "user_Id" });
            DropIndex("dbo.Salesman", new[] { "targetType_Id" });
            DropIndex("dbo.Salesman", new[] { "targetPeriod_Id" });
            DropIndex("dbo.Salesman", new[] { "salesmenCategory_Id" });
            AlterColumn("dbo.Salesman", "Base_Target", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropColumn("dbo.Salesman", "targetType_Id");
            DropColumn("dbo.Salesman", "targetPeriod_Id");
            DropColumn("dbo.Salesman", "salesmenCategory_Id");
            DropColumn("dbo.Salesman", "ValueLead");
            DropTable("dbo.TargetTypes");
            DropTable("dbo.TargetPeriods");
            DropTable("dbo.SalesmenCategories");
            CreateIndex("dbo.Salesman", "User_Id");
        }
    }
}
