namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingmanagertoUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Manager_Id", c => c.Int());
            CreateIndex("dbo.Users", "Manager_Id");
            AddForeignKey("dbo.Users", "Manager_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Manager_Id", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Manager_Id" });
            DropColumn("dbo.Users", "Manager_Id");
        }
    }
}
