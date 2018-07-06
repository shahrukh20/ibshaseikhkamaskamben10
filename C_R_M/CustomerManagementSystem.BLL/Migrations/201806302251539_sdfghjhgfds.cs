namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdfghjhgfds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Salesman", "Currency_Id", c => c.Int());
            AlterColumn("dbo.Salesman", "ValueLead", c => c.String());
            CreateIndex("dbo.Salesman", "Currency_Id");
            AddForeignKey("dbo.Salesman", "Currency_Id", "dbo.Currencies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Salesman", "Currency_Id", "dbo.Currencies");
            DropIndex("dbo.Salesman", new[] { "Currency_Id" });
            AlterColumn("dbo.Salesman", "ValueLead", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Salesman", "Currency_Id");
        }
    }
}
