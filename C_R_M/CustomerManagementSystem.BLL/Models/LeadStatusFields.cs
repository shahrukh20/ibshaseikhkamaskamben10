using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CustomerManagementSystem.BLL.Models
{
    public class LeadStatusFields
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual LeadPool leadPool { get; set; }
        public decimal TotalLeadScore { get; set; }
        public virtual string StatusField { get; set; }

        public virtual Status StatusType { get; set; }
        public virtual ActionType ActionType { get; set; }

        public virtual string NextActionDate { get; set; }
        public virtual string NextActionTime { get; set; }
        public virtual string ExpectedClosureDate { get; set; }
        public virtual string ExpectedValue { get; set; }
        public int? CurrencyId { get; set; }
        public virtual string Contact1 { get; set; }
        public virtual string Contact2 { get; set; }
        public virtual string Remarks { get; set; }
        public virtual CustomerManagementSystem.BLL.Enum.Enumeration.StatusEnum StatusEnum { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual DateTime CreatedOn { get; set; }
    }
}
