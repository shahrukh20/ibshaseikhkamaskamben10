namespace CustomerManagementSystem.BLL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LeadPoolHistory
    {
        [Key]
        public int Lead_Pool_History_Id { get; set; }

        public int Lead_Pool_Id { get; set; }

        public int? Status { get; set; }

        public int? Assign_To_ID { get; set; }

        public int? Assign_By_ID { get; set; }

        public int? Next_Action { get; set; }

        public DateTime? Next_Action_Date { get; set; }

        [StringLength(50)]
        public string Next_Action_Time { get; set; }

        [StringLength(50)]
        public string Next_Action_Contact1 { get; set; }

        [StringLength(50)]
        public string Next_Action_Contact2 { get; set; }

        [StringLength(500)]
        public string Status_Remarks { get; set; }

        [Required]
        [StringLength(50)]
        public string Created_By { get; set; }

        public DateTime Created_Datetime { get; set; }
    }
}
