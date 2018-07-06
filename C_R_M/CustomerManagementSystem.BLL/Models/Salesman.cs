namespace CustomerManagementSystem.BLL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Salesman")]
    public partial class Salesman
    {
        [Key]
        public int Salesman_Id { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string title { get; set; }

        //[Required]
        //[StringLength(100)]
        //public string First_Name { get; set; }

        //[Required]
        //[StringLength(100)]
        //public string Last_Name { get; set; }

        //public int Salesman_Type_ID { get; set; }

        //public int? Reporting_To_ID { get; set; }

        public virtual User user { get; set; }

        public bool Is_Manager { get; set; }
        [Required]
        public virtual TargetType targetType { get; set; }
        [Required]
        public virtual TargetPeriod targetPeriod { get; set; }
        [Required]
        public virtual SalesmenCategory salesmenCategory { get; set; }
        [Required]

        public string Base_Target { get; set; }
        [Required]
        public string ValueLead { get; set; }

        public bool Is_Active { get; set; }

        public int Created_By { get; set; }

        public DateTime Created_Datetime { get; set; }

        public int? Updated_By { get; set; }

        public DateTime? Updated_Datetime { get; set; }

        public virtual Currency Currency { get; set; }
    }
}
