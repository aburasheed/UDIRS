using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUHelper.Classes;

namespace UDIRS.Web
{
	public partial class Home : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (UDIRSHelper.PersonDetails != null)
			{
                //LoadSession();
                string fullName = UDIRSHelper.PersonDetails.Fullname.ToString();
                string email = UDIRSHelper.PersonDetails.WorkEmail.ToString();
                string mobile = UDIRSHelper.PersonDetails.Mobile.ToString();

                hdntxtName.Value = fullName;
                hdntxtContact.Value = mobile;
                hdntxtEmail.Value = email;
			}

           
		}
	}
}