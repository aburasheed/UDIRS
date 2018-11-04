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
    public partial class Form : System.Web.UI.Page
    {
        #region Fields
        DataView oDV = new DataView();
        StringBuilder sbIncidentSubCategoryIDs = new StringBuilder();
        string RefId = string.Empty;
        string strCorActionIDs = string.Empty;
        string strHistorystatus = string.Empty;
        string strHistorycomments = string.Empty;
        int number = 0;

        #endregion
        #region Events
        private void Page_Init(object sender, EventArgs e)
        {
            btnSave.Click += new EventHandler(btnSave_Click);

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CreateAttributes();
                PopulateHealthSafety();
                PopulateInformationSystem();
                PopulateAcademicIntegrity();
                PopulateRegulationsEthics();
                PopulateOthers();
                PopulateCorrectiveAction();
                PopulateLocation();
                PopulateTreatmentLocation();
                BindHours();
                BindMinutes();
                //BindRefNumber();
                LoadSession();
            }


        }
        private void CreateAttributes()
        {
            txtIncidentDesc.Attributes.Add("onkeydown", "return checkMaxLength(event, this, 'spIncidentRemaining');");
            txtInjuryDesc.Attributes.Add("onkeydown", "return checkMaxLength(event, this, 'spInjuryRemaining');");
            txtCorrectiveActionDesc.Attributes.Add("onkeydown", "return checkMaxLength(event, this, 'spCorrectiveRemaining');");
            txtAdditionalInformationDesc.Attributes.Add("onkeydown", "return checkMaxLength(event, this, 'spAdditionalRemaining');");
        }
        private bool IsValidExtension(string filePath)
        {
            bool isValid = false;
            string[] fileExtensions = { ".bmp", ".jpg", ".png", ".gif", ".jpeg", ".BMP", ".JPG", ".PNG", ".GIF", ".JPEG" };

            for (int i = 0; i <= fileExtensions.Length - 1; i++)
            {
                if (filePath.Contains(fileExtensions[i]))
                {
                    isValid = true;
                }
            }
            return isValid;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //  oDV = AdoUDIRS.IncidentData(txtIncidentDesc.Text.Trim());
            //oDV = AdoUDIRS.IncidentData();
            string strFileName = "";
            if (fuImage.HasFile)
            {
                if (IsValidExtension(fuImage.PostedFile.FileName))
                {
                    //strFileName = fuImage.PostedFile.FileName;
                    strFileName = Guid.NewGuid() + "_" + fuImage.PostedFile.FileName;
                    string filePath = (Server.MapPath("Uploads/") + strFileName);
                    fuImage.SaveAs(filePath);
                }
                else
                {
                    Label lbl = (Label)Page.Master.FindControl("lblMessage");
                    Message.Failure("File format not recognised." + "Upload .jpeg/.png/.gif/.jpg/.bmp formats only.", lbl);
                    fuImage.Focus();
                    return;
                }

            }

            InsertIncident(strFileName);
            Utility.ClearControls(this);
            //InsertNatureIncident();

            //InsertCorrectiveAction();

            if (AdoUDIRS.BlnError)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;

            }

        }
        #endregion
        #region Methods
        public void BindHours()
        {
            for (int i = 0; i < 24; i++)
                if (i < 10)
                    ddlHours.Items.Add(new ListItem('0' + i.ToString(), '0' + i.ToString()));
                else
                    ddlHours.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        public void BindMinutes()
        {
            for (int i = 0; i < 60; i++)
                if (i < 10)
                    ddlMinutes.Items.Add(new ListItem('0' + i.ToString(), '0' + i.ToString()));
                else
                    ddlMinutes.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        protected void InsertIncident(string fileName)
        {

            try
            {
                BuildNatureIncident();
                BuildCorrectiveAction();
                string strRoleID = string.Empty, strPersonID = string.Empty;
                strRoleID = UDIRSHelper.PersonRoleDetails.Where(p => p.Shortname == "USE" || p.Shortname == "Other").Select(s => s.RolesID).Single().ToString();
                strPersonID = UDIRSHelper.PersonDetails.Id.ToString();
                String combinedDateTime;
                //string selectedval = HiddenField1.Value;
                combinedDateTime = txtDate.Text + " " + ddlHours.Text + ":" + ddlMinutes.Text;

                oDV = AdoUDIRS.IncidentData(strRoleID, ddlLocation.SelectedValue, rblTreatmentPlace.SelectedValue, combinedDateTime, txtOtherlocation.Text.Trim(), txtIncidentDesc.Text.Trim(), rblInjured.SelectedValue, txtInjuryDesc.Text.Trim(), rblTreatmentProvided.SelectedValue, txtCorrectiveActionDesc.Text.Trim(), txtAdditionalInformationDesc.Text.Trim(), fileName, sbIncidentSubCategoryIDs.ToString(), txtOthers.Text.Trim(), strCorActionIDs, strPersonID, "PE", strHistorycomments);

                if (oDV == null)
                {
                    Label lbl = (Label)Page.Master.FindControl("lblMessage");
                    Message.Failure("Failed to save incident data.", lbl); ///// Pass label saleem
                    return;
                }
                else
                {
                    DataTable dt = oDV.ToTable();
                    //RefId = oDV.Table.Rows[0]["IncidentID"].ToString();
                    //Session["IncidentID"] = RefId;
                    Label lbl = (Label)Page.Master.FindControl("lblMessage");

                    Response.Redirect("~/Web/ThankYou.aspx", true);
                    //Message.Success("Incident data saved Successfullyssssssssssss", lbl); ///// Pass label saleem

                }
                if (AdoUDIRS.BlnError)
                {

                }
                else if (oDV.Count == 0)
                {

                }
                else if (oDV.Count > 0)
                {
                    //RefId = oDV.ToTable().Rows[0]["IncidentID"].ToString();
                    //Session["PersonID"] = RefId;
                }
            }
            catch (Exception ex)
            {
                Label lbl = (Label)Page.Master.FindControl("lblMessage");
                Message.Failure("Error 221, sorry, an error occured, " + AdoUDIRS.StrError, lbl); ///// Pass label saleem
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        private void BuildNatureIncident()
        {
            BuildIDsString(chkHealthSafety);
            BuildIDsString(chkInformationSystem);
            BuildIDsString(chkAcademicIntegrity);
            BuildIDsString(chkRegulationsEthics);
            BuildIDsString(chkOthers);
        }
        private void BuildIDsString(CheckBoxList chkList)
        {
            for (int i = 0; i <= chkList.Items.Count - 1; i++)
            {
                if (chkList.Items[i].Selected)
                {
                    sbIncidentSubCategoryIDs.Append(chkList.Items[i].Value.ToString() + "_");

                }

            }
        }
        private void BuildCorrectiveAction()
        {

            for (int i = 0; i <= chkCorrectiveAction.Items.Count - 1; i++)
            {
                if (chkCorrectiveAction.Items[i].Selected)
                {
                    strCorActionIDs += chkCorrectiveAction.Items[i].Value + "_"; //+= "_" + chkHealthSafety.Items[i].Text;

                }

            }
            //AdoUDIRS.CorrectiveAction(RefId, str.ToString(), "");
        }
        protected void PopulateTreatmentLocation()
        {
            try
            {
                oDV = AdoUDIRS.GetTreatmentLocation();

                if (AdoUDIRS.BlnError)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count == 0)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count > 0)
                {
                    rblTreatmentPlace.DataSource = oDV;
                    rblTreatmentPlace.DataValueField = "Id";
                    rblTreatmentPlace.DataTextField = "Title";
                    rblTreatmentPlace.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }

        }
        protected void PopulateHealthSafety()
        {

            try
            {
                oDV = AdoUDIRS.GetHealthSafety();

                if (AdoUDIRS.BlnError)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count == 0)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count > 0)
                {
                    chkHealthSafety.DataSource = oDV;
                    chkHealthSafety.DataValueField = "Id";
                    chkHealthSafety.DataTextField = "Title";
                    chkHealthSafety.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        protected void PopulateInformationSystem()
        {

            try
            {
                oDV = AdoUDIRS.GetInformationSystem();

                if (AdoUDIRS.BlnError)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count == 0)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count > 0)
                {
                    chkInformationSystem.DataSource = oDV;
                    chkInformationSystem.DataValueField = "Id";
                    chkInformationSystem.DataTextField = "Title";
                    chkInformationSystem.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        protected void PopulateAcademicIntegrity()
        {

            try
            {
                oDV = AdoUDIRS.GetAcademicIntegrity();

                if (AdoUDIRS.BlnError)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count == 0)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count > 0)
                {
                    chkAcademicIntegrity.DataSource = oDV;
                    chkAcademicIntegrity.DataValueField = "Id";
                    chkAcademicIntegrity.DataTextField = "Title";
                    chkAcademicIntegrity.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        protected void PopulateRegulationsEthics()
        {

            try
            {
                oDV = AdoUDIRS.GetRegulationsEthics();

                if (AdoUDIRS.BlnError)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count == 0)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count > 0)
                {
                    chkRegulationsEthics.DataSource = oDV;
                    chkRegulationsEthics.DataValueField = "Id";
                    chkRegulationsEthics.DataTextField = "Title";
                    chkRegulationsEthics.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        protected void PopulateOthers()
        {

            try
            {
                oDV = AdoUDIRS.GetOthersIncident();

                if (AdoUDIRS.BlnError)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count == 0)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count > 0)
                {
                    //cbOthers.Text = oDV[0].Row["Title"].ToString();
                    chkOthers.DataSource = oDV;
                    chkOthers.DataValueField = "Id";
                    chkOthers.DataTextField = "Title";
                    chkOthers.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        protected void PopulateCorrectiveAction()
        {

            try
            {
                oDV = AdoUDIRS.GetCorrectiveAction();

                if (AdoUDIRS.BlnError)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count == 0)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count > 0)
                {
                    chkCorrectiveAction.DataSource = oDV;
                    chkCorrectiveAction.DataValueField = "Id";
                    chkCorrectiveAction.DataTextField = "Title";
                    chkCorrectiveAction.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        protected void PopulateLocation()
        {
            try
            {
                oDV = AdoUDIRS.GetIncidentLocation();

                if (AdoUDIRS.BlnError)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count == 0)
                {
                    //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
                    return;
                }
                else if (oDV.Count > 0)
                {
                    ddlLocation.DataSource = oDV;
                    ddlLocation.DataValueField = "Id";
                    ddlLocation.DataTextField = "Longname";
                    ddlLocation.DataBind();
                    ddlLocation.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        protected void LoadSession()
        {
            if (UDIRSHelper.PersonDetails != null)
            {
                lblName.Text = UDIRSHelper.PersonDetails.Fullname;
                lblContact.Text = UDIRSHelper.PersonDetails.WorkPhone;
                lblEmail.Text = UDIRSHelper.PersonDetails.WorkEmail;
            }
        }
        #endregion
    }
}