using CustomerManagementSystem.BLL.Models;
using CustomerManagementSystem.BLL.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.CommonClasses
{
    public class LeadPoolsCommon : ApplicationFunctions
    {
        private CRMContext db = null;
        public LeadPoolsCommon(CRMContext db) : base(db)
        { this.db = db; }
        public List<SelectListItem> BindSourceOfLead(int id)
        {
            var sourceOfLead = db.SourceOfLeads.ToList();
            List<SelectListItem> sourceOfLeadItems = new List<SelectListItem>();
            foreach (var item in sourceOfLead)
            {
                if (item.Id == id)
                    sourceOfLeadItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = true
                    });
                else
                    sourceOfLeadItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });

            }
            return sourceOfLeadItems;

        }

        internal List<SelectListItem> BindNextActionHC(int actionTypeId)
        {
            try
            {
                var Actions = db.Actions.ToList();
                List<SelectListItem> ActionsItems = new List<SelectListItem>();
                foreach (var item in Actions)
                {
                    if (item.Action_Id == actionTypeId)
                        ActionsItems.Add(new SelectListItem()
                        {
                            Text = item.Action_Name,
                            Value = item.Action_Id.ToString(),
                            Selected = true
                        });
                    else
                        ActionsItems.Add(new SelectListItem()
                        {
                            Text = item.Action_Name,
                            Value = item.Action_Id.ToString()
                        });

                }
                return ActionsItems;
                //if (actionTypeId == 0)
                //{

                //}

                //return db.Actions.Select(x => new SelectListItem()
                //{
                //    Text = x.Action_Name,
                //    Value = x.Action_Id.ToString()
                //}).ToList();
            }
            catch (Exception e)
            {

            }

            //List<SelectListItem> items = new List<SelectListItem>()
            //{
            //    new SelectListItem() { Text = "No", Value = "No" },
            //    new SelectListItem() { Text = "Case/Suspect", Value = "CaseSuspect" },
            //    new SelectListItem() { Text = "Case/Prospect", Value = "CaseProspect" },
            //    new SelectListItem() { Text = "Hold", Value = "Hold" },
            //    new SelectListItem() { Text = "Dropped", Value = "Dropped" },
            //    new SelectListItem() { Text = "Lost", Value = "Lost" },
            //    new SelectListItem() { Text = "Closed", Value = "Closed" }
            //};
            return null;
        }

        internal List<SelectListItem> BindStatusHC(int statusId)
        {

            try
            {
                var Statuss = db.Status.ToList();
                List<SelectListItem> StatusItems = new List<SelectListItem>();
                foreach (var item in Statuss)
                {
                    if (item.Status_Id == statusId)
                        StatusItems.Add(new SelectListItem()
                        {
                            Text = item.Status1,
                            Value = item.Status_Id.ToString(),
                            Selected = true
                        });
                    else
                        StatusItems.Add(new SelectListItem()
                        {
                            Text = item.Status1,
                            Value = item.Status_Id.ToString()
                        });

                }
                return StatusItems;

                //return db.Status.Select(x => new SelectListItem()
                //{
                //    Text = x.Status1,
                //    Value = x.Status_Id.ToString()
                //}).ToList();
            }
            catch (Exception e)
            {

            }

            //List<SelectListItem> items = new List<SelectListItem>()
            //{
            //    new SelectListItem() { Text = "Meeting", Value = "Meeting" },
            //    new SelectListItem() { Text = "Demo", Value = "Demo" },
            //    new SelectListItem() { Text = "Quotation", Value = "Quotation" },

            //};
            return null;
        }

        internal void GenerateHistory(int id)
        {
            try
            {
                var leadPool = db.Lead_Pool.FirstOrDefault(x => x.Id == id);
                var leadStatus = db.LeadStatusFields.FirstOrDefault(x => x.leadPool.Id == id);
                var leadAttachments = db.Lead_Pool_Attachment.Where(x => x.Lead_Pool.Id == id);
                db.LeadHistories.Add(new LeadHistory()
                {
                    CreatedBy = 1,
                    CreatedOn = DateTime.Now,
                    LeadPool = JsonConvert.SerializeObject(leadPool),
                    LeadPoolAttachment = JsonConvert.SerializeObject(leadAttachments),
                    LeadStatusFields = JsonConvert.SerializeObject(leadStatus),
                });
                db.SaveChanges();
            }
            catch (Exception e)
            {


            }
        }

        public List<SelectListItem> BindProperties(int id)
        {
            var PropertyMasters = db.PropertyMaster.ToList();
            List<SelectListItem> propertyMastersItems = new List<SelectListItem>();
            foreach (var item in PropertyMasters)
            {
                if (id == item.PropertyMasterId)
                    propertyMastersItems.Add(new SelectListItem()
                    {
                        Text = item.PropertyName,
                        Value = item.PropertyMasterId.ToString(),
                        Selected = true
                    });
                else
                    propertyMastersItems.Add(new SelectListItem()
                    {
                        Text = item.PropertyName,
                        Value = item.PropertyMasterId.ToString()
                    });

            }

            return propertyMastersItems;

        }

        

        public LeadPoolsViewModel ConvertoLeadModel(int id, LeadPool _leadPool)
        {
            LeadPoolsViewModel leadPoolsViewModel = new LeadPoolsViewModel()
            {
                id = id,
                txtDate = _leadPool.Log_Date.ToString("dd-MM-yyyy"),// = DateTime.Parse(LeadVM.),
                txtbudget = _leadPool.Budget,//= decimal.Parse(LeadVM.),
                txtLeadName = _leadPool.Lead_Name,
                ddlSourceOfLead = _leadPool.Source_Type_Id,
                txtSource = _leadPool.Source,
                txtRemarks = _leadPool.Lead_Remarks,
                txtLogNo = _leadPool.LogNo,
                txtContactPerson = _leadPool.Contact,
                txtDesignation = _leadPool.Designation,
                txtEmail = _leadPool.Email,
                txtFax = _leadPool.Fax,
                txtTel = _leadPool.Telephone,
                txtStatus = _leadPool.Status

            };
            return leadPoolsViewModel;
        }
    }

}