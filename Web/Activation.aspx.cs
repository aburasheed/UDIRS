using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using UDIRS.Classes;
using IAUHelper.Classes;

namespace UDIRS.Web
{
    public partial class Activation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string personRoleID = string.Empty;
                if (Request.QueryString["vid"] != null)
                {
                    if (Request.QueryString["vid"].ToString() != string.Empty)
                    {
                        personRoleID = Cryptography.Decrypt(Request.QueryString["vid"].ToString(), "sblw-3hn8-sqoy19"); 
                        DataView dv = AdoUDIRS.ActivatePersonRole(personRoleID);
                        if (dv[0]["StatusMsg"] != null)
                        {
                            if (dv[0]["StatusMsg"].ToString() == "UpdatedSuccessfully")
                            {
                                // User activated successfully, Please click here to login.
                            }
                            else
                            {
                                //Failed to activate the user
                            }
                        }

                    }
                }
            }
        }
    }
}