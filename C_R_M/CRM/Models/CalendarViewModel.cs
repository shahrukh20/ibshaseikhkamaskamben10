using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class CalendarViewModel
    {
        public int LogNo { get; set; }
        public string LeadName { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
    }
}