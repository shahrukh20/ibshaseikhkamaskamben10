using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.BLL.Models
{
    public partial class User
    {
        [Key]
        public int Id { get; set; }


        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string LoginId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }

        public int GroupId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public DateTime? LastLogin { get; set; }

        public bool IsActive { get; set; }


        public int ApplicationUser { get; set; }

        public string CountryCode { get; set; }

        public int CountryId { get; set; }
        public int CityId { get; set; }
        public virtual User Manager { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
