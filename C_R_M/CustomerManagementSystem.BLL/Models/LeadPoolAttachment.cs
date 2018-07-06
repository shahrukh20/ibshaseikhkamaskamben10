namespace CustomerManagementSystem.BLL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LeadPoolAttachment
    {
        [Key]
        public int Lead_Pool_Attachement_Id { get; set; }

        public int Lead_Pool_Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Attachment_Path { get; set; }
        public string Attachment_Name { get; set; }
        public string Saved_Name { get; set; }
        public string Size { get; set; }
        public virtual LeadPool Lead_Pool { get; set; }
    }
}
