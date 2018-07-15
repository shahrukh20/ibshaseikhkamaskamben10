using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Reports.ReportModels
{
    public class UnAssignedReportsBindingViewModel
    {
        public string LeadNo { get; set; }
        public string LeadName { get; set; }
        public string SourceType { get; set; }
        public string Source { get; set; }
        public string Operator { get; set; }
        public string Manager { get; set; }
        public string CreatedBy { get; set; }
        public string Chanel { get; set; }
    }

    public class OpportunityInHand
    {
        public string LeadNo { get; set; }
        public string LeadName { get; set; }
        public string SourceType { get; set; }
        public string Source { get; set; }      
        public string Manager { get; set; }
        public string Salesman { get; set; }
        public string Status { get; set; }
        public string NextAction  { get; set; }
        public string ActionDate{ get; set; }
        public string ActionTime { get; set; }
        public string ExpectedClosureDate { get; set; }
        public string CreatedDate { get; set; }
        public string Chanel { get; set; }
    }
   
}