using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.BLL.Models
{
    public class SourceOfLead
    {
        public virtual int Id { get; set; }
        [Required]
        public virtual string Name { get; set; }
    }
}
