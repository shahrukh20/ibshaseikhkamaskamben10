using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.BLL.ViewModels
{
    public class SalemenViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public int NameId { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Target Period")]
        public int TargetPeriodId { get; set; }
        [Display(Name = "Target Type")]
        public int TargetTypeId { get; set; }
        [Display(Name = "Base Target")]
        public string BaseTarget { get; set; }
        [Display(Name = "Value/Lead")]
        public string ValueAndLead { get; set; }
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

    }
    public class SalesmenJsGridViewModel
    {
        public int SalesmenID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string TargetPeriod { get; set; }
        public string TargetType { get; set; }
        public string BaseTarget { get; set; }
        public string ValueLead { get; set; }
        public string Symbol { get; set; }
      
    }
}
