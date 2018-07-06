namespace CustomerManagementSystem.BLL.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CRMContext : DbContext
    {
        public CRMContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Campaign> Campaigns { get; set; }
        public virtual DbSet<LeadHistory> LeadHistories { get; set; }
        public virtual DbSet<LeadStatusFields> LeadStatusFields { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<ActionType> Actions { get; set; }
        public virtual DbSet<LeadPool> Lead_Pool { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<SourceOfLead> SourceOfLeads { get; set; }

        public virtual DbSet<PropertyMaster> PropertyMaster { get; set; }
        public virtual DbSet<InquiryData> InquiryData { get; set; }

        public virtual DbSet<LeadPoolAttachment> Lead_Pool_Attachment { get; set; }
        public virtual DbSet<LeadPoolContacts> Lead_Pool_Contacts { get; set; }
        public virtual DbSet<LeadPoolHistory> Lead_Pool_History { get; set; }
        public virtual DbSet<Salesman> Salesmen { get; set; }
        public virtual DbSet<SalesmanType> Salesman_Type { get; set; }
        public virtual DbSet<SourceType> Source_Type { get; set; }
        public virtual DbSet<Status> Status { get; set; }


        public virtual DbSet<SalesmenCategory> SalesmenCategories { get; set; }

        public virtual DbSet<TargetPeriod> TargetPeriods { get; set; }
        public virtual DbSet<TargetType> TargetTypes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionType>()
                .Property(e => e.Action_Name)
                .IsUnicode(false);

            modelBuilder.Entity<ActionType>()
                .Property(e => e.Score)
                .HasPrecision(18, 0);

            modelBuilder.Entity<LeadPool>()
                .Property(e => e.Budget)
                .HasPrecision(18, 0);

            modelBuilder.Entity<LeadPool>()
                .Property(e => e.Lead_Name)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPool>()
                .Property(e => e.Source)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPool>()
                .Property(e => e.Lead_Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPool>()
                .Property(e => e.Next_Action_Time)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPool>()
                .Property(e => e.Status_Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPool>()
                .Property(e => e.Channel)
                .IsUnicode(false);



            modelBuilder.Entity<LeadPoolAttachment>()
                .Property(e => e.Attachment_Path)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPoolContacts>()
                .Property(e => e.Contact_Person_Name)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPoolContacts>()
                .Property(e => e.Contact_Designation)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPoolContacts>()
                .Property(e => e.Contact_Person_Mobile1)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPoolContacts>()
                .Property(e => e.Contact_Person_Tel_no)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPoolContacts>()
                .Property(e => e.Contact_Person_Mobile2)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPoolContacts>()
                .Property(e => e.Contact_Person_Email1)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPoolContacts>()
                .Property(e => e.Contact_Person_Email2)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPoolContacts>()
                .Property(e => e.Contact_Person_Fax)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPoolContacts>()
                .Property(e => e.Lead_Type)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPoolHistory>()
                .Property(e => e.Next_Action_Time)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPoolHistory>()
                .Property(e => e.Next_Action_Contact1)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPoolHistory>()
                .Property(e => e.Next_Action_Contact2)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPoolHistory>()
                .Property(e => e.Status_Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<LeadPoolHistory>()
                .Property(e => e.Created_By)
                .IsUnicode(false);


            modelBuilder.Entity<Salesman>()
                .Property(e => e.Base_Target)
                .IsUnicode(false);

         

            modelBuilder.Entity<SalesmanType>()
                .Property(e => e.Type_Name)
                .IsUnicode(false);

            modelBuilder.Entity<SourceType>()
                .Property(e => e.Source_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .Property(e => e.Status1)
                .IsUnicode(false);
        }
        public System.Data.Entity.DbSet<CustomerManagementSystem.BLL.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<CustomerManagementSystem.BLL.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<CustomerManagementSystem.BLL.Models.Area> Areas { get; set; }
    }
}
