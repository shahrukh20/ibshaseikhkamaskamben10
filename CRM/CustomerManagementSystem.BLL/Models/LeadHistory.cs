using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CustomerManagementSystem.BLL.Models
{
    public class LeadHistory
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string LeadPool { get; set; }
        public virtual string LeadStatusFields { get; set; }
        public virtual string LeadPoolAttachment { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual Enum.Enumeration.StatusEnum CurrentStatus { get; set; }
        public virtual DateTime CreatedOn { get; set; }
    }
}
