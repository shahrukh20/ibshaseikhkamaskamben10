namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class City_Country : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropertyMasters", "Status", c => c.String());
            AddColumn("dbo.PropertyMasters", "PublishToWeb", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropertyMasters", "PublishToWeb");
            DropColumn("dbo.PropertyMasters", "Status");
        }
    }
}
