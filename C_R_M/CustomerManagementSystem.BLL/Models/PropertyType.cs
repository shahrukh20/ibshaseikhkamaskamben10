using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.BLL.Models
{
    public class PropertyType
    {
        [Key]
        public virtual int Id { get; set; }
        [Display(Name = "Property Type")]
        public virtual string Name{ get; set; }
    }
}
