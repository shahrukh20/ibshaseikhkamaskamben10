namespace CustomerManagementSystem.BLL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ActionType
    {
        [Key]
        public int Action_Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Action Name")]
        public string Action_Name { get; set; }

        [Required(ErrorMessage = "Numbers are required")]

        public decimal Score { get; set; }

        public DateTime? CreatedOn { get; set; }
        public virtual User CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual User UpdatedBy { get; set; }
    }
}
