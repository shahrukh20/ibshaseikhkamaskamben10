namespace CustomerManagementSystem.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CRMFirstMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        Action_Id = c.Int(nullable: false, identity: true),
                        Action_Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Score = c.Decimal(nullable: false, precision: 18, scale: 0),
                    })
                .PrimaryKey(t => t.Action_Id);
            
            CreateTable(
                "dbo.LeadPools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Log_Date = c.DateTime(nullable: false),
                        Budget = c.Decimal(nullable: false, precision: 18, scale: 0),
                        Lead_Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Source_Type_Id = c.Int(nullable: false),
                        Source = c.String(nullable: false, maxLength: 100, unicode: false),
                        Lead_Remarks = c.String(maxLength: 500, unicode: false),
                        Status = c.Int(),
                        Assign_To_ID = c.Int(),
                        Assign_By_ID = c.Int(),
                        Next_Action = c.Int(),
                        Next_Action_Date = c.DateTime(),
                        Next_Action_Time = c.String(maxLength: 50, unicode: false),
                        Status_Remarks = c.String(maxLength: 500, unicode: false),
                        Channel = c.String(nullable: false, maxLength: 50, unicode: false),
                        Created_By = c.Int(nullable: false),
                        Created_DateTime = c.DateTime(nullable: false),
                        Updated_By = c.Int(),
                        Updated_Datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LeadPoolAttachments",
                c => new
                    {
                        Lead_Pool_Attachement_Id = c.Int(nullable: false, identity: true),
                        Lead_Pool_Id = c.Int(nullable: false),
                        Attachment_Path = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Lead_Pool_Attachement_Id)
                .ForeignKey("dbo.LeadPools", t => t.Lead_Pool_Id)
                .Index(t => t.Lead_Pool_Id);
            
            CreateTable(
                "dbo.LeadPoolContacts",
                c => new
                    {
                        Lead_Pool_Contact_Id = c.Int(nullable: false, identity: true),
                        Lead_Pool_Id = c.Int(nullable: false),
                        Contact_Person_Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Contact_Designation = c.String(maxLength: 100, unicode: false),
                        Contact_Person_Mobile1 = c.String(maxLength: 50, unicode: false),
                        Contact_Person_Tel_no = c.String(maxLength: 50, unicode: false),
                        Contact_Person_Mobile2 = c.String(maxLength: 50, unicode: false),
                        Contact_Person_Email1 = c.String(maxLength: 100, unicode: false),
                        Contact_Person_Email2 = c.String(maxLength: 100, unicode: false),
                        Contact_Person_Fax = c.String(maxLength: 50, unicode: false),
                        Lead_Type = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Lead_Pool_Contact_Id)
                .ForeignKey("dbo.LeadPools", t => t.Lead_Pool_Id)
                .Index(t => t.Lead_Pool_Id);
            
            CreateTable(
                "dbo.LeadPoolHistories",
                c => new
                    {
                        Lead_Pool_History_Id = c.Int(nullable: false, identity: true),
                        Lead_Pool_Id = c.Int(nullable: false),
                        Status = c.Int(),
                        Assign_To_ID = c.Int(),
                        Assign_By_ID = c.Int(),
                        Next_Action = c.Int(),
                        Next_Action_Date = c.DateTime(),
                        Next_Action_Time = c.String(maxLength: 50, unicode: false),
                        Next_Action_Contact1 = c.String(maxLength: 50, unicode: false),
                        Next_Action_Contact2 = c.String(maxLength: 50, unicode: false),
                        Status_Remarks = c.String(maxLength: 500, unicode: false),
                        Created_By = c.String(nullable: false, maxLength: 50, unicode: false),
                        Created_Datetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Lead_Pool_History_Id);
            
            CreateTable(
                "dbo.SalesmanTypes",
                c => new
                    {
                        Salesman_Type_Id = c.Int(nullable: false, identity: true),
                        Type_Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Is_Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Salesman_Type_Id);
            
            CreateTable(
                "dbo.Salesman",
                c => new
                    {
                        Salesman_Id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 50, unicode: false),
                        First_Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Last_Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Salesman_Type_ID = c.Int(nullable: false),
                        Reporting_To_ID = c.Int(),
                        Is_Manager = c.Boolean(nullable: false),
                        Target_Period = c.String(nullable: false, maxLength: 50, unicode: false),
                        Target_Type = c.String(nullable: false, maxLength: 50, unicode: false),
                        Base_Target = c.String(nullable: false, maxLength: 50, unicode: false),
                        No_Of_Base_Target = c.Decimal(nullable: false, precision: 18, scale: 0),
                        Comission_Type = c.String(nullable: false, maxLength: 50, unicode: false),
                        Is_Active = c.Boolean(nullable: false),
                        Created_By = c.Int(nullable: false),
                        Created_Datetime = c.DateTime(nullable: false),
                        Updated_By = c.Int(),
                        Updated_Datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Salesman_Id);
            
            CreateTable(
                "dbo.SourceTypes",
                c => new
                    {
                        Source_Type_Id = c.Int(nullable: false, identity: true),
                        Source_Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Is_Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Source_Type_Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Status_Id = c.Int(nullable: false, identity: true),
                        Status = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Status_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeadPoolContacts", "Lead_Pool_Id", "dbo.LeadPools");
            DropForeignKey("dbo.LeadPoolAttachments", "Lead_Pool_Id", "dbo.LeadPools");
            DropIndex("dbo.LeadPoolContacts", new[] { "Lead_Pool_Id" });
            DropIndex("dbo.LeadPoolAttachments", new[] { "Lead_Pool_Id" });
            DropTable("dbo.Status");
            DropTable("dbo.SourceTypes");
            DropTable("dbo.Salesman");
            DropTable("dbo.SalesmanTypes");
            DropTable("dbo.LeadPoolHistories");
            DropTable("dbo.LeadPoolContacts");
            DropTable("dbo.LeadPoolAttachments");
            DropTable("dbo.LeadPools");
            DropTable("dbo.Actions");
        }
    }
}
