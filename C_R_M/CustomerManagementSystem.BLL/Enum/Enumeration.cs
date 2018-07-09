using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.BLL.Enum
{
    public class Enumeration
    {
        public enum StatusEnum
        {
            New = 1,
            Assign = 2
        }

        public enum ReportType
        {
            UnAssigned = 0,
            OpportunityInHand = 1,
            Target = 2,
            LeadConversion = 3,
            Campaign = 4
        }
    }
}
