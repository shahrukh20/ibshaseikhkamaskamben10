using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.BLL.Models
{
    public class Campaign
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "From Date")]
        public DateTime DateFrom { get; set; }
        [Display(Name = "To Date")]
        public DateTime DateTo { get; set; }
        [Display(Name = "Running")]
        public bool IsActive { get; set; }

        public virtual PropertyMaster PropertyMaster { get; set; }


        public DateTime? CreatedOn { get; set; }
        public virtual User CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual User UpdatedBy { get; set; }
    }
}
