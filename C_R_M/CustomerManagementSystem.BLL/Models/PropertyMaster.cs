using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.BLL.Models
{
    public class PropertyMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PropertyMasterId { get; set; }
        [Required]
        [Display(Name = "Property Name")]
        public string PropertyName { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Area")]
        public string Area { get; set; }
        [Required]
        [Display(Name = "Property Type")]
        public string PropertyType { get; set; }
        [Required]
        [Display(Name = "Property Detail")]
        public string PropertyDetail { get; set; }
        [Display(Name = "PlotNo")]
        public string PlotNo { get; set; }
        [Display(Name = "Plot Area")]
        public string PlotArea { get; set; }
        [Display(Name = "BuiltUp Area")]
        public string BuiltUpArea { get; set; }
        [Display(Name = "Commercial Area")]
        public string CommercialArea { get; set; }
        [Display(Name = "Residential Area")]
        public string ResidentialArea { get; set; }
        [Required]
        [Display(Name = "No of floors")]
        public int NoOfFloors { get; set; }
        [Required]
        [Display(Name = "Property Owner Name")]
        public string PropertyOwnerName { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
        [Required]
        [Display(Name = "Selling Price")]
        public int SellingPrice { get; set; }
        public string Status { get; set; }
        public string PublishToWeb { get; set; }
        public string ImageJson { get; set; }
        public int? CampaignId { get; set; }
        [Display(Name = "Currency")]
        public int? CurrencyId { get; set; }

    }
}
