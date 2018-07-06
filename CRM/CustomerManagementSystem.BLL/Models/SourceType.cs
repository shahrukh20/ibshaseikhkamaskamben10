namespace CustomerManagementSystem.BLL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SourceType
    {
        [Key]
        public int Source_Type_Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Source_Name { get; set; }

        public bool Is_Active { get; set; }

        public DateTime? CreatedOn { get; set; }
        public virtual User CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual User UpdatedBy { get; set; }

    }
}
