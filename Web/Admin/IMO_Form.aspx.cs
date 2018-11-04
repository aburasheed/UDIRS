using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using UDIRS.Classes;
using System.Text;
using IAUHelper.Classes;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace UDIRS.Web.Admin
{
    public partial class IMO_Form : System.Web.UI.Page
    {
        #region Fields
        DataView oDV = new DataView();
        DataSet oDS = new DataSet();
        string sLocation, slblStatus;
        #endregion
        #region Properties
        public string IncidentID
        {
            get
            {
                if (ViewState["IncidentID"] != null)
                    return ViewState["IncidentID"].ToString();
                else
                    return string.Empty;
            }
            set { ViewState["IncidentID"] = value; }

        }
        public string PreviousIncidentStatus
        {
            get
            {
                if (ViewState["PreviousIncidentStatus"] != null)
                    return ViewState["PreviousIncidentStatus"].ToString();
                else
                    return string.Empty;
            }
            set { ViewState["PreviousIncidentStatus"] = value; }

        }
        #endregion
        #region Events
        private void Page_Init(object sender, EventArgs e)
        {
            btnGoBack.Click += new EventHandler(btnGoBack_Click);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["IID"] != null)
                {
                    IncidentID = Cryptography.Decrypt(Request.QueryString["IID"].ToString(), "sblw-3hn8-sqoy19");
                    LoadAllData();
                    BindGridview();
                }
            }

        }

        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("IMO_History.aspx");
        }
        #endregion
        #region Mothods
        private void LoadAllData()
        {
            try
            {
                oDS = AdoUDIRS.GetAllInformation(IncidentID);

                if (AdoUDIRS.BlnError)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDS.Tables.Count == 0)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDS.Tables.Count > 0)
                {
                    string mystring = string.Empty;
                    foreach (DataRow dr in oDS.Tables[0].Rows)
                        if (dr["NatureOfIncident"] != null)
                            mystring += dr["NatureOfIncident"] + ", ".ToString();
                    if (mystring.IndexOf(',') > 0)
                        mystring = mystring.Substring(0, mystring.Length - 2);
                    lblNature.Text = mystring;

                    string myAction = string.Empty;
                    foreach (DataRow dr in oDS.Tables[1].Rows)
                        if (dr["Suggestion"] != null)
                            myAction += dr["Suggestion"] + ", ".ToString();
                    if (myAction.IndexOf(',') > 0)
                        myAction = myAction.Substring(0, myAction.Length - 2);
                    lblSuggestion.Text = myAction;

                    lblDateTime.Text = oDS.Tables[2].Rows[0]["IncidentDate"].ToString();
                    lblIncidentDesc.Text = oDS.Tables[2].Rows[0]["IncidentDescription"].ToString();
                    lblInjuryDesc.Text = oDS.Tables[2].Rows[0]["InjuryDescription"].ToString();
                    lblTypeTeatment.Text = oDS.Tables[2].Rows[0]["TreatmentLocation"].ToString();
                    lblAction.Text = oDS.Tables[2].Rows[0]["ActionDescription"].ToString();
                    lblAddtional.Text = oDS.Tables[2].Rows[0]["AdditionalInfo"].ToString();
                    lblReference.Text = oDS.Tables[2].Rows[0]["RefNumber"].ToString();


                    string myfactors = string.Empty;
                    foreach (DataRow dr in oDS.Tables[4].Rows)
                        if (dr["IncidentInvestigation"] != null)
                            myfactors += dr["IncidentInvestigation"] + ", ".ToString();
                    if (myfactors.IndexOf(',') > 0)
                        myfactors = myfactors.Substring(0, myfactors.Length - 2);
                    lblFactors.Text = myfactors;


                    /* -- Action Taken to prevent
                    string myprevent = string.Empty;
                    foreach (DataRow dr in oDS.Tables[3].Rows)
                        if (dr["ActionDescription"] != null)
                            myprevent += dr["ActionDescription"] + ", ".ToString();
                    if (myprevent.IndexOf(',') > 0)
                        myprevent = myprevent.Substring(0, myprevent.Length - 2);
                    lblToPrevent.Text = myprevent;
                     * */
                    // -- Display Status in Lable
                   
                    slblStatus = oDS.Tables[2].Rows[0]["Incident_Status"].ToString();

                   
                   if (slblStatus == "PE")
                   {
                       lblStatus.Text = "Pending";
                   }
                   else if (slblStatus == "IP")
                   {
                       lblStatus.Text = "In Progress";
                   }
                   else if (slblStatus == "EV")
                   {
                       lblStatus.Text = "Escalate to VDQ";
                   }
                   else if (slblStatus == "CL")
                   {
                       lblStatus.Text = "Closed";
                   }
                   else if (slblStatus == "RP")
                   {
                       lblStatus.Text = "RMO Pending";
                   }


                    sLocation = oDS.Tables[2].Rows[0]["UnitName"].ToString();
                    if (sLocation == "Other")
                    {
                        lblLocation.Text = oDS.Tables[2].Rows[0]["OtherLocation"].ToString();
                    }
                    else
                    {
                        if (oDS.Tables[2].Rows[0]["UnitName"] != null)
                            lblLocation.Text = oDS.Tables[2].Rows[0]["UnitName"].ToString();
                    }
                    string sInjured = oDS.Tables[2].Rows[0]["PersonInjured"].ToString();
                    if (sInjured == "Y")
                    {
                        lblPersonInjured.Text = "Yes";
                    }
                    else
                    {
                        lblPersonInjured.Text = "NO";
                    }
                    string sProvided = oDS.Tables[2].Rows[0]["TreatmentProvided"].ToString();
                    if (sInjured == "Y")
                    {
                        lblTreatmentProvided.Text = "Yes";
                    }
                    else
                    {
                        lblTreatmentProvided.Text = "NO";
                    }
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }

        }

        public void BindGridview()
        {
            try
            {
                oDS = AdoUDIRS.GetAllInformation(IncidentID);

                if (AdoUDIRS.BlnError)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDS.Tables.Count == 0)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDS.Tables.Count > 0)
                {
                    gvActionPlan.DataSource = oDS.Tables[3];
                    gvActionPlan.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
            //gvCommentsHistory.DataSource = dt;
            //gvCommentsHistory.DataBind();
        }
        #endregion


    }
}