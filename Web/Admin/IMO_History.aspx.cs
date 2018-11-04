using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Collections;
//using ClassLibrary.BusinessLayer.Entities;
using System.Configuration;
using UDIRS.Classes;
using System.Web.UI.WebControls;
using System.Text;
using IAUHelper.Classes;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Globalization;
using IAUHelper.Classes;


namespace UDIRS.Web.Admin
{
    public partial class IMO_History : System.Web.UI.Page
    {
        DataView oDV = new DataView();
        #region Events
        private void Page_Init(object sender, EventArgs e)
        {
            gvAdminHistory.RowDataBound += new GridViewRowEventHandler(gvAdminHistory_RowDataBound);
            btnSearch.Click += new EventHandler(btnSearch_Click);
            btnReset.Click += new EventHandler(btnReset_Click);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (UDIRSHelper.PersonDetails != null)
                {
                    MethodFactory.BindStatusSearch(ref ddlStatus);
                    BindGridview(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0);
                    PopulateLocation();
                }
            }
            else
            {
                //lblMsg.Text = "No Incident Report.";
            }
        }

        protected void gvAdminHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlAnchor ahref = (HtmlAnchor)e.Row.FindControl("ahReference");
                if (ahref != null)
                    if (DataBinder.Eval(e.Row.DataItem, "Id") != null)
                        ahref.HRef = "IMO_Form.aspx?IID=" + Cryptography.Encrypt(DataBinder.Eval(e.Row.DataItem, "Id").ToString(), "sblw-3hn8-sqoy19");
                Label lblIncidentLocation = (e.Row.FindControl("lblIncidentLocation") as Label);
                Label lblOtherLocation = (e.Row.FindControl("lblOtherLocation") as Label);
                string ilocation, olocation;
                ilocation = lblIncidentLocation.Text;
                olocation = lblOtherLocation.Text;
                if (lblIncidentLocation.Text == "Other")
                    lblIncidentLocation.Text = olocation;

                e.Row.Cells[7].ToolTip = e.Row.Cells[7].Text;
                //e.Row.Cells[3].Text = e.Row.Cells[3].Text.Length > 75 ? (e.Row.Cells[3].Text.Substring(0, 75) + "...") : e.Row.Cells[3].Text;
                String hs = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "IncidentStatus").ToString().Trim());

                if (hs == "PE")
                {
                    e.Row.Cells[7].Text = StatusResource.PE;
                    e.Row.Cells[7].ForeColor = Color.Red;

                }
                else if (hs == "IP")
                {
                    e.Row.Cells[7].Text = StatusResource.IP;

                }
                else if (hs == "EV")
                {
                    e.Row.Cells[7].Text = StatusResource.EV;

                }
                else if (hs == "CL")
                {
                    e.Row.Cells[7].Text = StatusResource.CL;

                }
                else
                {
                    e.Row.Cells[7].ForeColor = Color.Red;
                }
            }

        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = txtFromDate.Text = txtToDate.Text = string.Empty;
            ddlPersonInjured.SelectedIndex = ddlStatus.SelectedIndex = ddlTreatmentProvided.SelectedIndex = ddlLocation.SelectedIndex = 0;
            BindGridview(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string Status = string.Empty, RefNumber = string.Empty, IsPersonInjured = string.Empty, IsTreatmentProvided = string.Empty, FromDate = string.Empty, ToDate = string.Empty;
            int UnitID = 0;
            if (ddlStatus.SelectedIndex > 0)
                Status = ddlStatus.SelectedValue;
            if (ddlPersonInjured.SelectedIndex > 0)
                IsPersonInjured = ddlPersonInjured.SelectedValue;
            if (ddlTreatmentProvided.SelectedIndex > 0)
                IsTreatmentProvided = ddlTreatmentProvided.SelectedValue;
            if (txtSearch.Text.Trim() != string.Empty)
                RefNumber = txtSearch.Text.Trim();
            if (txtFromDate.Text.Trim() != string.Empty)
                FromDate = txtFromDate.Text.Trim();
            if (txtToDate.Text.Trim() != string.Empty)
                ToDate = txtToDate.Text.Trim();
            if (ddlLocation.SelectedIndex > 0)
                UnitID =Convert.ToInt32(ddlLocation.SelectedValue);
            try
            {
                BindGridview(RefNumber, Status, IsPersonInjured, IsTreatmentProvided, FromDate, ToDate, UnitID);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('wfrmGrid: 11')</script>" + ex.Message);
            }
            finally
            {

            }
        }

        #endregion

        #region Methods
        protected void BindGridview(string refNumber, string incidentStatus, string isAnyPersonInjured, string isTreatmentProvided, string fromDate, string toDate, int unitID)
        {
            try
            {
                oDV = AdoUDIRS.GET_InvestigationHistory_Search_IMO(refNumber, incidentStatus, isAnyPersonInjured, isTreatmentProvided, fromDate, toDate, unitID);

                if (AdoUDIRS.BlnError)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count == 0)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    lblMsg.Text = "No incident found.";
                    lblMsg.Attributes.Add("style", "font-size:40px; color:Red; font-weight:bold;");
                    gvAdminHistory.DataSource = null;
                    gvAdminHistory.DataBind();
                    return;
                }
                else if (oDV.Count > 0)
                {
                    gvAdminHistory.DataSource = oDV;
                    gvAdminHistory.DataBind();
                    lblMsg.Text = "";

                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }

        }
        protected void PopulateLocation()
        {
            try
            {
                oDV = AdoUDIRS.GetIncidentLocation();

                if (AdoUDIRS.BlnError)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count == 0)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count > 0)
                {
                    ddlLocation.DataSource = oDV;
                    ddlLocation.DataValueField = "Id";
                    ddlLocation.DataTextField = "Longname";
                    ddlLocation.DataBind();
                    ddlLocation.Items.Insert(0, new ListItem("--Select--", "-1"));
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        #endregion
    }
}