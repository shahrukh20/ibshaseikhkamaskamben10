namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdf : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Actions", newName: "ActionTypes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ActionTypes", newName: "Actions");
        }
    }
}
