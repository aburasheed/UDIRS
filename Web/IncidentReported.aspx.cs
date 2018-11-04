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

namespace UDIRS.Web
{
    public partial class IncidentReported : System.Web.UI.Page
    {

        string sLocation, slblStatus;
        DataSet oDS = new DataSet();
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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                if (Request.QueryString["IID"] != null)
                {
                    IncidentID = Cryptography.Decrypt(Request.QueryString["IID"].ToString(), "sblw-3hn8-sqoy19");
                    Bindvalues();
                }
            }
            //RefId = Request.QueryString["RID"].ToString();
            //lblReference.Text = RefId;
            //Bindvalues();
           
        }
        private void Bindvalues()
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
                    lblReference.Text = oDS.Tables[2].Rows[0]["RefNumber"].ToString();
                    lblDateTime.Text = oDS.Tables[2].Rows[0]["IncidentDate"].ToString();
                    lblIncidentDesc.Text = oDS.Tables[2].Rows[0]["IncidentDescription"].ToString();
                    lblInjuryDesc.Text = oDS.Tables[2].Rows[0]["InjuryDescription"].ToString();
                    lblTypeTeatment.Text = oDS.Tables[2].Rows[0]["TreatmentLocation"].ToString();
                    lblAction.Text = oDS.Tables[2].Rows[0]["ActionDescription"].ToString();
                    lblAddtional.Text = oDS.Tables[2].Rows[0]["AdditionalInfo"].ToString();
                    string myfactors = string.Empty;
                    foreach (DataRow dr in oDS.Tables[4].Rows)
                        if (dr["IncidentInvestigation"] != null)
                            myfactors += dr["IncidentInvestigation"] + ", ".ToString();
                    if (myfactors.IndexOf(',') > 0)
                        myfactors = myfactors.Substring(0, myfactors.Length - 2);
                    lblFactors.Text = myfactors;

                    string myprevent = string.Empty;
                    foreach (DataRow dr in oDS.Tables[3].Rows)
                        if (dr["ActionDescription"] != null)
                            myprevent += dr["ActionDescription"] + ", ".ToString();
                    if (myprevent.IndexOf(',') > 0)
                        myprevent = myprevent.Substring(0, myprevent.Length - 2);
                    lblToPrevent.Text = myprevent;

                    slblStatus = oDS.Tables[2].Rows[0]["Incident_Status"].ToString();
                    if (slblStatus == "PE")
                    {
                        lblStatus.Text = StatusResource.PE;
                    }
                    if(slblStatus == "IP")
                    {
                        lblStatus.Text = StatusResource.IP;
                    }
                    else if (slblStatus == "EV")
                    {
                        lblStatus.Text = StatusResource.EV;
                    }
                    else if (slblStatus == "CL")
                    {
                        lblStatus.Text = StatusResource.CL;
                    }

                    //lblPersonInjured.Text = oDS.Tables[2].Rows[0]["PersonInjured"].ToString();

                    sLocation = oDS.Tables[2].Rows[0]["UnitName"].ToString();
                    if (sLocation == "Other")
                    {
                        lblLocation.Text = oDS.Tables[2].Rows[0]["OtherLocation"].ToString();
                    }
                    else
                    {
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
    }
}