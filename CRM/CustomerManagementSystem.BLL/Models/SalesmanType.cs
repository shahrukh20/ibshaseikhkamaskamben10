namespace CustomerManagementSystem.BLL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SalesmanType
    {
        [Key]
        public int Salesman_Type_Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Type_Name { get; set; }

        public bool Is_Active { get; set; }
    }
}
