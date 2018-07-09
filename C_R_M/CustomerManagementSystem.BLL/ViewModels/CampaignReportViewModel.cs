using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.BLL.ViewModels
{
    public class CampaignReportViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "From Date")]
        public DateTime DateFrom { get; set; }
        [Display(Name = "To Date")]
        public DateTime DateTo { get; set; }
        [Display(Name = "Running")]
        public bool IsActive { get; set; }
        public virtual string PropertyMaster { get; set; }


    }
}
