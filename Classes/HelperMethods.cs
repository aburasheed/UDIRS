using System.Text;
using System.Web.UI;
using System;
using System.Net.Mail;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using UDIRS.Entities;
using System.Collections.Generic;

namespace IAUHelper.Classes
{
	
    public static class UDIRSHelper
    {
        public static Person PersonDetails
        {
            get
            {
                return (HttpContext.Current.Session["PersonDetails"] == null) ? null : (Person)HttpContext.Current.Session["PersonDetails"];
            }
            set { HttpContext.Current.Session["PersonDetails"] = value; }
        }
        public static IList<PersonRoles> PersonRoleDetails
        {
            get
            {
                return (HttpContext.Current.Session["PersonRoleDetails"] == null) ? null : (IList<PersonRoles>)HttpContext.Current.Session["PersonRoleDetails"];
            }
            set { HttpContext.Current.Session["PersonRoleDetails"] = value; }
        }

    }
}
