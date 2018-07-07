using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.WebForms
{
    public partial class ReportViwer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Session["ds"];
            ReportDocument rd = new ReportDocument();
            string path = Server.MapPath(@"\Reports\ReportsTemplate\UnAssignedLead.rpt");
            rd.Load(path);
            //rd. = null;
            rd.SetDataSource(ds);
            CrystalReportViewer1.EnableDatabaseLogonPrompt = false;
            CrystalReportViewer1.ReportSource = rd;
            //CrystalReportViewer1.RefreshReport();
            CrystalReportViewer1.DataBind();
        }
    }
}