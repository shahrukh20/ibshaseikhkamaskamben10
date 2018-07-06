namespace CustomerManagementSystem.BLL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Status
    {
        [Key]
        public int Status_Id { get; set; }

        [Column("Status")]
        [Required]
        [StringLength(50)]
        [Display(Name = "Status")]
        public string Status1 { get; set; }
    }
}
