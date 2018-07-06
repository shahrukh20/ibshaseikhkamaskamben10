using CustomerManagementSystem.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.CommonClasses
{
    public class SalemenCommon : ApplicationFunctions
    {
        private CRMContext db = null;
        public SalemenCommon(CRMContext db) : base(db)
        { this.db = db; }


    }
}