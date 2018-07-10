using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Reports.ReportModels
{
    public class CampaignReportsBindingViewModel
    {
        public string IsActive { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string Property { get; set; }
    }
}