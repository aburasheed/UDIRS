using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUHelper.Classes;

namespace UDIRS.Master
{
    public partial class Form : System.Web.UI.MasterPage
    {
        public string alphabeta = "abcdefghijklmnopqrstuvwxyz" + "abcdefghijklmnopqrstuvwxyz".ToUpper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UDIRSHelper.PersonDetails == null || UDIRSHelper.PersonRoleDetails == null)
            {
                Response.Redirect(Page.ResolveUrl("~/web/logon.aspx"), true);
            }
            if (!Page.IsPostBack)
            {
                //Label oLblUsername = this.Master.FindControl("lblUsername") as Label;
                //Label oLblFullname = this.Master.FindControl("lblFullname") as Label;//lblUsername username:  mfalquraan    pwd: ju789rt		
                if (UDIRSHelper.PersonDetails != null)
                {
                    string strFullname = "", strFullnameAr = "";
                    if (UDIRSHelper.PersonDetails.Username != null)
                    {
                        lblUsername.Text = UDIRSHelper.PersonDetails.Username.ToString();
                        lblFullName.Text = UDIRSHelper.PersonDetails.Fullname.ToString();
                        strFullname = UDIRSHelper.PersonDetails.Fullname.ToString();
                        strFullnameAr = UDIRSHelper.PersonDetails.FullnameAr.ToString();
                        if (strFullname.Trim() != "")
                        {
                            lblFullName.Text = strFullname;
                        }
                        else
                        {
                            lblFullName.Text = strFullnameAr;
                        }

                        if (alphabeta.IndexOf(lblFullName.Text.Substring(0, 1)) == -1)
                        {
                            lblFullName.Style.Add("font-size", "140%");
                            //oLblUsername.Style.Add("font-family", "Times New Roman");
                            //oLblUsername.Style.Add("font-family", "Times New Roman");
                            lblFullName.Style.Add("font-family", "Verdana");
                            lblFullName.Style.Add("text-align", "right");
                            lblFullName.Style.Add("padding-right", "4px");
                        }
                    }
                    // --------------------------------------------------------------------
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();

                Session.Abandon();

                Session.RemoveAll();

                Session.Contents.RemoveAll();

                System.Web.Security.FormsAuthentication.SignOut();

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Buffer = true;
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
                Response.Expires = -1000;
                Response.CacheControl = "no-cache";

                //Server.Transfer("~/Web/KpiHome.aspx");		
                Server.Transfer(ResolveUrl("~/Web/Logon.aspx"), false);
                //Server.Execute(ResolveUrl("~/Web/KpiHome.aspx"), false);
            }
            catch (Exception ex)
            {
                //Response.Write("Exeption is: " + ex.Message);
                // Thread was being aborted
            }
        }
    }
}