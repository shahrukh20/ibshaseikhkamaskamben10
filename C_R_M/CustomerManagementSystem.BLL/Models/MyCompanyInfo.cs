using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.BLL.Models
{
    public class MyCompanyInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "My Company Name")]
        public string MyCompanyName { get; set; }
        [Display(Name = "My Company Logo")]
        public string MyCompanyLogo { get; set; }
        [Display(Name = "My Company Miniature Logo")]
        public string MyCompanyMiniatureLogo { get; set; }
        [Display(Name = "My Company Slogan")]
        public string MyCompanySlogan { get; set; }
        [Display(Name = "Fiscal Year StartDate")]
        public DateTime? FiscalYearStartDate { get; set; }
        [Display(Name = "Default Currency")]
        public int CurrencyId { get; set; }
        [Display(Name = "Default Phone Code")]
        public string DefaultPhoneCode { get; set; }
        [Display(Name = "My Company Address")]
        public string MyCompanyAddress { get; set; }

    }
}
