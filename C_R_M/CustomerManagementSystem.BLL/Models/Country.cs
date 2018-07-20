using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.BLL.Models
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }
        [Display(Name = "Name")]
        public string CountryName { get; set; }

        public ICollection<City> Cities { get; set; }
    }
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }
        [Display(Name = "Name")]
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Area> Areas { get; set; }
    }

    public class Area
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AreaId { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string AreaName { get; set; }
        [Display(Name = "City Name")]
        [Required(ErrorMessage = "City is required.")]
        public int CityId { get; set; }
        public virtual City City { get; set; }

    }
}
