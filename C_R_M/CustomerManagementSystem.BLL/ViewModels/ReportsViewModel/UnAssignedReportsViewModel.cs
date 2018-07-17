using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CustomerManagementSystem.BLL.Enum.Enumeration;

namespace CustomerManagementSystem.BLL.ViewModels.ReportsViewModel
{
    public class UnAssignedReportsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Manager")]
        public int ManagerId { get; set; }
        [Display(Name = "Salesmen")]
        public int SalemanId { get; set; }
        [Display(Name = "Saleman Category")]
        public int SalemanCategoryId { get; set; }
        [Display(Name = "Lead")]
        public int LeadId { get; set; }
        [Display(Name = "Target")]
        public int TargetId { get; set; }

        [Display(Name = "Date To")]
        public DateTime? fromdate { get; set; }
        [Display(Name = "Date From")]
        public DateTime? todate { get; set; }

        public ReportType ReportType { get; set; }
    }
}
