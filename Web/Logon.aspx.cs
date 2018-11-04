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
    public partial class Logon : System.Web.UI.Page
    {
        #region Properties
        public string _QualityUser
        {
            get
            {
                return "quality";
            }

        }
        public string _QualityPass
        {
            get
            {
                return "Quality2#$";
            }

        }
        public string UserName
        {
            get
            {
                return (ViewState["UserName"] == null) ? string.Empty : ViewState["UserName"].ToString();
            }
            set { ViewState["UserName"] = value; }
        }
        public string Password
        {
            get
            {
                return (ViewState["Password"] == null) ? string.Empty : ViewState["Password"].ToString();
            }
            set { ViewState["Password"] = value; }
        }
        #endregion
        #region Fields
        nsWebServices.ldapSoapClient oLSC = new nsWebServices.ldapSoapClient();
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            btnLogIn.Click += new EventHandler(btnLogIn_Click);
            this.Form.DefaultButton = this.btnLogIn.UniqueID;
        }
        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                UserName = txtUserName.Text.ToLower().Trim();
                Password = txtPassword.Text.Trim();
                if (UserName.Contains("@iau.edu.sa") || UserName.Contains("@uod.edu.sa") || !UserName.Contains('@'))
                {
                    #region Active Directory Login
                    
                    if (IsUserExist())
                    {
                        
                        string FullName = string.Empty, Email = string.Empty, Phone = string.Empty, Mobile = string.Empty, NationalID = string.Empty;
                        nsWebServices.GetUser[] oArray;
                        oArray = GetUserDetails();
                        if (oArray != null)
                        {
                            if (oArray[0].ID != null && oArray[0].ID != string.Empty)
                            {
                                #region Person details from webservice
                                NationalID = oArray[0].ID;
                                if (oArray[0].FullName != null)
                                    FullName = oArray[0].FullName;
                                if (oArray[0].Email != null)
                                    Email = oArray[0].Email;
                                if (oArray[0].Phone != null)
                                    Phone = oArray[0].Phone;
                                if (oArray[0].Mobile != null)
                                    Mobile = oArray[0].Mobile;
                                #endregion
                                DataSet dSet = new DataSet();
                                dSet = AdoUDIRS.AddUpdateUserDetailsByNationalID(NationalID, FullName, Email, Phone, Mobile);
                                if (dSet != null)
                                {
                                    if (dSet.Tables[0] != null)
                                    {
                                        if (dSet.Tables[0].Rows.Count > 0)
                                            SetPersonDetails(dSet.Tables[0]);
                                        else
                                        {
                                            Label lbl = (Label)Page.Master.FindControl("lblMessage");
                                            Message.Success("Person does not exist/Database error", lbl);//Person does not exist/Database error
                                        }
                                        #region Set PersonRoleDetails Entity
                                        if (dSet.Tables[1] != null)
                                        {
                                            if (dSet.Tables[1].Rows.Count > 0)
                                            {
                                                SetPersonRoleDetails(dSet.Tables[1]);
                                                Response.Redirect("~/Web/Home.aspx", true);
                                            }
                                        }
                                        else
                                        {
                                            Label lbl = (Label)Page.Master.FindControl("lblMessage");
                                            Message.Failure("PersonRoles does not exist/Database error", lbl); ///// Pass label saleem//PersonRoles does not exist/Database error
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        Label lbl = (Label)Page.Master.FindControl("lblMessage");
                                        Message.Failure("PersonRoles does not exist/Database error", lbl);//Person does not exist/Database error
                                    }
                                }
                                else
                                {
                                    Label lbl = (Label)Page.Master.FindControl("lblMessage");
                                    Message.Failure("PersonRoles does not exist/Database error", lbl);//Person does not exist/Database error
                                }
                            }
                            else
                            {
                                Label lbl = (Label)Page.Master.FindControl("lblMessage");
                                Message.Failure("Your NationalID not found, Please contact helpdesk@iau.edu.sa or 31111.", lbl);//Message: Your NationalID not found, Please contact helpdesk@iau.edu.sa or 31111.
                            }
                        }
                        else
                        {
                            Label lbl = (Label)Page.Master.FindControl("lblMessage");
                            Message.Failure("Server not responding, Please try after some time.", lbl);//Message: Server not responding, Please try after some time.
                        }
                    }
                    else
                    {
                        Label lbl = (Label)Page.Master.FindControl("lblMessage");
                        Message.Failure("Incorrect login credentials", lbl);//Message: incorrect login credentials
                    }
                    #endregion

                }
                else
                {
                    #region Other Login
                    DataSet dSet = new DataSet();
                    dSet = AdoUDIRS.CheckAndGetDataForOthersLogin(UserName);
                    if (dSet != null)
                    {
                        if (dSet.Tables[0] != null)
                        {
                            if (dSet.Tables[0].Rows.Count > 0)
                                SetPersonDetails(dSet.Tables[0]);
                            else
                            {
                                Label lbl = (Label)Page.Master.FindControl("lblMessage");
                                Message.Success("sfsfsfsfsfsfsfsfsf", lbl);
                                return;
                                //Person does not exist/Database error
                            }
                            #region Set PersonRoleDetails Entity
                            if (dSet.Tables[1] != null)
                            {
                                if (dSet.Tables[1].Rows.Count > 0)
                                {
                                    SetPersonRoleDetails(dSet.Tables[1]);
                                    Response.Redirect("~/Web/Home.aspx", true);
                                }
                            }
                            else
                            {
                                Label lbl = (Label)Page.Master.FindControl("lblMessage");
                                Message.Success("PersonRoles does not exist/Database error", lbl);//PersonRoles does not exist/Database error
                            }
                            #endregion
                        }
                        else
                        {
                            Label lbl = (Label)Page.Master.FindControl("lblMessage");
                            Message.Success("Incorrect Username/Password", lbl);//Incorrect Username/Password
                        }
                    }
                    else
                    {
                        Label lbl = (Label)Page.Master.FindControl("lblMessage");
                        Message.Success("Incorrect Username/Password", lbl);//Incorrect Username/Password
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                Label lbl1 = (Label)Page.Master.FindControl("lblMessage");
                Message.Success(ex.Message + " <br /> "+ ex.InnerException, lbl1);//Person does not exist/Database error
            }
        }
        #endregion
        #region Private Methods
        private static void SetPersonRoleDetails(DataTable dTable)
        {
            IList<PersonRoles> PersonRolesList = new List<PersonRoles>();
            foreach (DataRow row in dTable.Rows)
                PersonRolesList.Add(new PersonRoles() { RolesID = Convert.ToInt32(row["RolesID"]), Shortname = row["Shortname"].ToString(), Longname = row["Longname"].ToString() });
            UDIRSHelper.PersonRoleDetails = PersonRolesList;
        }
        private static void SetPersonDetails(DataTable dTable)
        {
            #region Set PersonDetails Entity
            Person person = new Person();
            if (dTable.Rows[0]["Id"] != null)
                person.Id = Convert.ToInt32(dTable.Rows[0]["Id"]);
            if (dTable.Rows[0]["Username"] != null)
                person.Username = dTable.Rows[0]["Username"].ToString();
            if (dTable.Rows[0]["Pwd"] != null)
                person.Pwd = dTable.Rows[0]["Pwd"].ToString();
            if (dTable.Rows[0]["Firstname"] != null)
                person.Firstname = dTable.Rows[0]["Firstname"].ToString();
            if (dTable.Rows[0]["Midname"] != null)
                person.Midname = dTable.Rows[0]["Midname"].ToString();
            if (dTable.Rows[0]["Lastname"] != null)
                person.Lastname = dTable.Rows[0]["Lastname"].ToString();
            if (dTable.Rows[0]["Fullname"] != null)
                person.Fullname = dTable.Rows[0]["Fullname"].ToString();
            if (dTable.Rows[0]["FirstnameAr"] != null)
                person.FirstnameAr = dTable.Rows[0]["FirstnameAr"].ToString();
            if (dTable.Rows[0]["MidnameAr"] != null)
                person.MidnameAr = dTable.Rows[0]["MidnameAr"].ToString();
            if (dTable.Rows[0]["LastnameAr"] != null)
                person.LastnameAr = dTable.Rows[0]["LastnameAr"].ToString();
            if (dTable.Rows[0]["FullnameAr"] != null)
                person.FullnameAr = dTable.Rows[0]["FullnameAr"].ToString();
            if (dTable.Rows[0]["Title"] != null)
                person.Title = dTable.Rows[0]["Title"].ToString();
            if (dTable.Rows[0]["JobTitle"] != null)
                person.JobTitle = dTable.Rows[0]["JobTitle"].ToString();
            if (dTable.Rows[0]["NationIqama"] != null)
                person.NationIqama = dTable.Rows[0]["NationIqama"].ToString();
            if (dTable.Rows[0]["ComputerNo"] != null)
                person.ComputerNo = dTable.Rows[0]["ComputerNo"].ToString();
            if (dTable.Rows[0]["WorkEmail"] != null)
                person.WorkEmail = dTable.Rows[0]["WorkEmail"].ToString();
            if (dTable.Rows[0]["PersonalEmail"] != null)
                person.PersonalEmail = dTable.Rows[0]["PersonalEmail"].ToString();
            if (dTable.Rows[0]["WorkPhone"] != null)
                person.WorkPhone = dTable.Rows[0]["WorkPhone"].ToString();
            if (dTable.Rows[0]["Mobile"] != null)
                person.Mobile = dTable.Rows[0]["Mobile"].ToString();
            if (dTable.Rows[0]["Gender"] != null)
                person.Gender = dTable.Rows[0]["Gender"].ToString();
            if (dTable.Rows[0]["RecStatus"] != null)
                person.RecStatus = dTable.Rows[0]["RecStatus"].ToString();
            if (dTable.Rows[0]["RecUser"] != null)
                person.RecUser = dTable.Rows[0]["RecUser"].ToString();
            if (dTable.Rows[0]["RecDate"] != null)
                person.RecDate = Convert.ToDateTime(dTable.Rows[0]["RecDate"]);
            UDIRSHelper.PersonDetails = person;
            #endregion
        }
        private bool IsUserExist()
        {
            return oLSC.UserValid(_QualityUser, _QualityPass, UserName, Password);
        }
        private nsWebServices.GetUser[] GetUserDetails()
        {
            return oLSC.GetUser_(_QualityUser, _QualityPass, UserName, true);
        }
        
        #endregion
    }
}