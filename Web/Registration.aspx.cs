using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Collections;
using System.Net.Mail;
using UDIRS.Classes;
using UDIRS.Entities;
using IAUHelper.Classes;
using System.Text.RegularExpressions;
using System.Configuration;

namespace UDIRS.Web
{
    public partial class Registration : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            this.Form.DefaultButton = this.btnSubmit.UniqueID;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string Email = txtEmailID.Text.ToLower().Trim();
            if ((Email.Contains("@iau.edu.sa") || Email.EndsWith("@uod.edu.sa")))
            {
                lblRMsg.Attributes.CssStyle.Add("float", "right");
                lblRMsg.Text = "Sorry! University Email address can not be used.";
                lblRMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                DataView oDV = new DataView();
                oDV = AdoUDIRS.Registration(txtEmailID.Text.Trim(), txtRpwd.Text.Trim(), txtFirstname.Text.Trim(), txtLastname.Text.Trim(),
                                     txtContact.Text.Trim(), rdoGender.SelectedItem.Value);
                if (AdoUDIRS.BlnError)
                {
                    lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                }
                else
                {
                    if (oDV.Table.Rows.Count > 0)
                    {
                        if (oDV[0].Row["PersonRoleID"] != null)
                        {
                            if (oDV[0].Row["PersonRoleID"].ToString() == "0")
                            {
                                Label lbl = (Label)Page.Master.FindControl("lblMessage");
                                Message.Success("User Already exist", lbl);//User Already exist
                            }
                                else
                            { 
                                SendEmail(oDV[0].Row["PersonRoleID"].ToString()); 
                            }
                        }
                    }
                    else
                    {
                        Label lbl = (Label)Page.Master.FindControl("lblMessage");
                        Message.Success("Try again after some time.", lbl);// try again after some time.
                    }

                }
            }
        }
        
        #endregion

        #region Private Methods
       
      
        private void SendEmail(string personRoleID)
        {
            string EmailingStatus = ConfigurationManager.AppSettings["EmailingStatus"].ToString();
            string Subject = "Email Validation for UDIRS", Body = "";
            string FullName = txtFirstname.Text.Trim() + " " + txtLastname.Text.Trim();
            string Email = txtEmailID.Text.Trim();
            string PersonRoleID = Cryptography.Encrypt(personRoleID, "sblw-3hn8-sqoy19");
            Body = "Hello " + FullName + ",<br/><br/>Please<a href=\"https://udirs.IAU.edu.sa/Web/Activation.aspx?vid=\"" + PersonRoleID + ">click here to validate your email.</a><br/><br/>Thanks<br/>UDIRS Team";
            if (EmailingStatus == "Live")
            {
                if (EmailUtility.SendEmail(ConfigurationManager.AppSettings["FromEmail"].ToString(), Email, Subject, Body))
                {
                    Label lbl = (Label)Page.Master.FindControl("lblMessage");
                    Message.Success("Email validation link is sent to your email. Please validate.", lbl);//Email validation link is sent to your email. Please validate.

                }
                else
                {
                    Label lbl = (Label)Page.Master.FindControl("lblMessage");
                    Message.Success("Email sending failed. Please try after some time.", lbl);//show failure message 
                }

            }
            else if (EmailingStatus == "Local")
            {
                if (EmailUtility.SendEmail(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["ToEmail"].ToString(), Subject, Body))
                {
                    Label lbl = (Label)Page.Master.FindControl("lblMessage");
                    Message.Success("Email validation link is sent to your email. Please validate.", lbl);//Email validation link is sent to your email. Please validate.
                }
                else
                {
                    Label lbl = (Label)Page.Master.FindControl("lblMessage");
                    Message.Failure("Email sending failed. Please try after some time.", lbl);//show failure message 
                }


            }
            Utility.ClearControls(this);
        }
        #endregion
    }
}