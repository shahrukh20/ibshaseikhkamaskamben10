namespace CustomerManagementSystem.BLL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LeadPoolContacts
    {
        [Key]
        public int Lead_Pool_Contact_Id { get; set; }

        public int Lead_Pool_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Contact_Person_Name { get; set; }

        [StringLength(100)]
        public string Contact_Designation { get; set; }

        [StringLength(50)]
        public string Contact_Person_Mobile1 { get; set; }

        [StringLength(50)]
        public string Contact_Person_Tel_no { get; set; }

        [StringLength(50)]
        public string Contact_Person_Mobile2 { get; set; }

        [StringLength(100)]
        public string Contact_Person_Email1 { get; set; }

        [StringLength(100)]
        public string Contact_Person_Email2 { get; set; }

        [StringLength(50)]
        public string Contact_Person_Fax { get; set; }

        [Required]
        [StringLength(50)]
        public string Lead_Type { get; set; }

        public virtual LeadPool Lead_Pool { get; set; }
    }
}
