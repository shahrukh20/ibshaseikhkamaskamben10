using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Reports.ReportModels
{
    public class ReportModel
    {
        public string Name { get; set; }
        public string DateField { get; set; }
    }

    public class SalePipeLineViewModel
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }
}