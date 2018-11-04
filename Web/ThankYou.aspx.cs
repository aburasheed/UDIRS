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
using System.Drawing;
using UDIRS.Entities;
using IAUHelper.Classes;

namespace UDIRS.Web
{
    public partial class ThankYou : System.Web.UI.Page
    {
        DataView oDV = new DataView();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["Fullname"] = dSet.Tables[0].Rows[0]["Fullname"].ToString();
            LoadSession();
            lblMsg.Text = " Thank you for Report an Incident";
            lblReference.Text = "Your Incident Reference number is:  ";

        }

        protected void LoadSession()
        {
            try
            {
                //oDV = AdoUDIRS.GetIncidentHistory();
                oDV = AdoUDIRS.GetReferenceNumber();

                if (AdoUDIRS.BlnError)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count == 0)
                {
                    return;
                }
                else if (oDV.Count > 0)
                {
                    string sValue = oDV.ToTable().Rows[0]["RefNumber"].ToString();
                    lblRefID.Text = sValue;

                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
            if (UDIRSHelper.PersonDetails != null)
            {
                if (UDIRSHelper.PersonDetails.Fullname != null)
                    lblName.Text = UDIRSHelper.PersonDetails.Fullname;
            }
            else
            {
                //redirect to home or logout
            }

            //lblRefID.Text = ReferenceId;

        }
    }
}