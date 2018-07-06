using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CustomerManagementSystem.BLL.Enum.Enumeration;

namespace CustomerManagementSystem.BLL.ViewModels.ReportsViewModel
{
    public class UnAssignedReportsViewModel
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public int SalemanId { get; set; }
        public int SalemanCategoryId { get; set; }
        public int LeadId { get; set; }
        public ReportType ReportType { get; set; }
    }
}
