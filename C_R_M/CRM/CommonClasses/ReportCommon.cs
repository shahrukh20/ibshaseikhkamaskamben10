using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerManagementSystem.BLL.Models;

namespace CRM.CommonClasses
{
    public class ReportCommon : ApplicationFunctions
    {
        private CRMContext db = null;
        public ReportCommon(CRMContext db) : base(db)
        {
            this.db = db;
        }

    }
}