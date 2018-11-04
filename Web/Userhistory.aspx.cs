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

namespace UDIRS.Web
{
    public partial class Userhistory : System.Web.UI.Page
    {
        DataView oDV = new DataView();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridview();
            }
            else
            {
                //lblMsg.Text = "No Incident Report.";
            }
        }
        protected void BindGridview()
        {
            /*
            SqlConnection con = new SqlConnection("Data Source=SureshDasari;Integrated Security=true;Initial Catalog=MySampleDB");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from UserDetails", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Close();
            DataSet ds = new DataSet();
            da.Fill(ds);
            gvHistory.DataSource = ds;
            gvHistory.DataBind();
             */


            try
            {
                if (UDIRSHelper.PersonDetails != null)
                    oDV = AdoUDIRS.GET_IncidentsForPerson(UDIRSHelper.PersonDetails.Id.ToString());
                else
                {
                    //redirect to logon page
                }
                if (AdoUDIRS.BlnError)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count == 0)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    lblMsg.Text = "No Incident Reported.";
                    lblMsg.Attributes.Add("style", "font-size:40px; color:Red; font-weight:bold;");
                    return;
                }
                else if (oDV.Count > 0)
                {
                    gvHistory.DataSource = oDV;
                    gvHistory.DataBind();

                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }

        }

        protected void gvHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlAnchor ahref = (HtmlAnchor)e.Row.FindControl("ahReference");
                if (ahref != null)
                    if (DataBinder.Eval(e.Row.DataItem, "Id") != null)
                        ahref.HRef = "IncidentReported.aspx?IID=" + Cryptography.Encrypt(DataBinder.Eval(e.Row.DataItem, "Id").ToString(), "sblw-3hn8-sqoy19");
                Label lblIncidentLocation = (e.Row.FindControl("lblIncidentLocation") as Label);
                Label lblOtherLocation = (e.Row.FindControl("lblOtherLocation") as Label);
                string ilocation, olocation;
                ilocation = lblIncidentLocation.Text;
                olocation = lblOtherLocation.Text;
                if (lblIncidentLocation.Text == "Other")
                {
                    lblIncidentLocation.Text = olocation;

                }
            }

        }
    }
}
