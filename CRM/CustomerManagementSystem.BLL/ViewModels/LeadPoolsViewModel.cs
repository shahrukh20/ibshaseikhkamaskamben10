using CustomerManagementSystem.BLL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.BLL.ViewModels
{
    public class LeadPoolUserType
    {
        public int Id { get; set; }
    }
    public class LeadPoolsViewModel
    {
        public string txtStatusRemarks { get; set; }
        public int id { get; set; }
        public string txtLogNo { get; set; }
        public string txtDate { get; set; }
        public decimal txtbudget { get; set; }
        public string txtLeadName { get; set; }
        public string txtContactPerson { get; set; }
        public string txtDesignation { get; set; }
        public string txtTel { get; set; }
        public string txtMobile { get; set; }
        public string txtFax { get; set; }
        public string txtEmail { get; set; }
        public int ddlCurrency { get; set; }

        public int ddlSourceOfLead { get; set; }
        public int ddlProperty { get; set; }
        public string txtSource { get; set; }
        public string txtRemarks { get; set; }
        public string txtNextActionTime { get; set; }
        public string txtNextActionDate { get; set; }
        public int ddstatus { get; set; }
        public int ddnextaction { get; set; }

        public Enumeration.StatusEnum txtStatus { get; set; }
    }

    public class LeadListingModel
    {
        public int Id { get; set; }
        public string LeadName { get; set; }
        public string Date { get; set; }
        public string LeadRemarks { get; set; }
        public int Salesmen { get; set; }

        public string AssignedBy { get; set; }
        public string AssignedTo { get; set; }

        public string CreatedBy { get; set; }

    }

    public class LeadAttachmentsViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
    }

    public class LeadContactViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string contact { get; set; }
    }

    //public class SelectBinding
    //{
    //    public string Name { get; set; }
    //    public int Id { get; set; }
    //}
    public class LeadStatusUpdateViewModel
    {
        public int id { get; set; }
        public string ddstatus { get; set; }
        public string ddnextaction { get; set; }
        public string txtNextActionTime { get; set; }
        public string txtNextActionDate { get; set; }
        public string txtStatusRemarks { get; set; }
    }

    //public class LeadContactUpdateViewModel
    //{
    //    public virtual string name { get; set; }
    //    public virtual string contact { get; set; }
    //}

}
