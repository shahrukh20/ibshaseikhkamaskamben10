namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class curens : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropertyMasters", "CurrencyId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropertyMasters", "CurrencyId");
        }
    }
}
