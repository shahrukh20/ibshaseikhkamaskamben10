namespace CustomerManagementSystem.BLL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LeadPool
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public int Id { get; set; }

        public DateTime Log_Date { get; set; }
        public string LogNo { get; set; }

        public decimal Budget { get; set; }

        [Required]
        public string Lead_Name { get; set; }

        public int Source_Type_Id { get; set; }

        [Required]
        public string Source { get; set; }

        public string Lead_Remarks { get; set; }

        public CustomerManagementSystem.BLL.Enum.Enumeration.StatusEnum Status { get; set; }

        public int? Assign_To_ID { get; set; }

        public int? Assign_By_ID { get; set; }

        public int? Next_Action { get; set; }

        public DateTime? Next_Action_Date { get; set; }

        public string Next_Action_Time { get; set; }

        public string Status_Remarks { get; set; }

        public string Channel { get; set; }

        public int Created_By { get; set; }

        public DateTime Created_DateTime { get; set; }

        public int? Updated_By { get; set; }

        public DateTime? Updated_Datetime { get; set; }
        public string Contact { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }

        public decimal Score { get; set; }

        public virtual PropertyMaster PropertyMaster { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
