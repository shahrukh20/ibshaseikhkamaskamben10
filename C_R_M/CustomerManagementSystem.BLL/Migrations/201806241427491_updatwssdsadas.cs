namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatwssdsadas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeadStatusFields", "ActionType_Action_Id", c => c.Int());
            AddColumn("dbo.LeadStatusFields", "SourceType_Source_Type_Id", c => c.Int());
            CreateIndex("dbo.LeadStatusFields", "ActionType_Action_Id");
            CreateIndex("dbo.LeadStatusFields", "SourceType_Source_Type_Id");
            AddForeignKey("dbo.LeadStatusFields", "ActionType_Action_Id", "dbo.ActionTypes", "Action_Id");
            AddForeignKey("dbo.LeadStatusFields", "SourceType_Source_Type_Id", "dbo.SourceTypes", "Source_Type_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeadStatusFields", "SourceType_Source_Type_Id", "dbo.SourceTypes");
            DropForeignKey("dbo.LeadStatusFields", "ActionType_Action_Id", "dbo.ActionTypes");
            DropIndex("dbo.LeadStatusFields", new[] { "SourceType_Source_Type_Id" });
            DropIndex("dbo.LeadStatusFields", new[] { "ActionType_Action_Id" });
            DropColumn("dbo.LeadStatusFields", "SourceType_Source_Type_Id");
            DropColumn("dbo.LeadStatusFields", "ActionType_Action_Id");
        }
    }
}
