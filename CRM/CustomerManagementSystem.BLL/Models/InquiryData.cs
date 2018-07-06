using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.BLL.Models
{
    public class InquiryData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InquiryDataID { get; set; }

        [ForeignKey("LeadPool")]
        public int LeadPoolId { get; set; }
        public int PropertyTypeID { get; set; }
        public string PropertyTypeName { get; set; }
        public string Detail { get; set; }
        public string Budget { get; set; }
        public string ContactInformation { get; set; }

        public virtual LeadPool LeadPool { get; set; }
    }
}
