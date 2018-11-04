using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using UDIRS.Classes;
using System.Text;
using IAUHelper.Classes;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace UDIRS.Web.Admin
{
    public partial class InvestigationForm1 : System.Web.UI.Page
    {
        #region Fields
        DataView oDV = new DataView();
        StringBuilder sbInvestigationSubCategoryIDs = new StringBuilder();
        string strHistorycomments = string.Empty;
        string sLocation; //
        DataSet oDS = new DataSet();
        public long ActionPlanCount = 0;
        #endregion

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
        public string PreviousIncidentComment
        {
            get
            {
                if (ViewState["PreviousIncidentComment"] != null)
                    return ViewState["PreviousIncidentComment"].ToString();
                else
                    return string.Empty;
            }
            set { ViewState["PreviousIncidentComment"] = value; }

        }
        public string PreviousIncidentStatus
        {
            get
            {
                if (ViewState["PreviousIncidentStatus"] != null)
                    return ViewState["PreviousIncidentStatus"].ToString();
                else
                    return string.Empty;
            }
            set { ViewState["PreviousIncidentStatus"] = value; }

        }
        #endregion

        #region Events
        private void Page_Init(object sender, EventArgs e)
        {
            btnSave.Click += new EventHandler(btnSave_Click);
            btnAddNew.Click += new EventHandler(btnAddNew_Click);
            gvActionPlan.RowDataBound += new GridViewRowEventHandler(gvActionPlan_RowDataBound);
            gvCommentsHistory.RowDataBound += new GridViewRowEventHandler(gvCommentsHistory_RowDataBound);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["IID"] != null)
                    {
                        IncidentID = Cryptography.Decrypt(Request.QueryString["IID"].ToString(), "sblw-3hn8-sqoy19");
                        MethodFactory.BindStatus(ref ddlStatus);
                        CreateAttributes();
                        PopulateCheckboxlists();
                        LoadAllData();
                        BindGridview();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void gvCommentsHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[3].ToolTip = e.Row.Cells[3].Text;
                //e.Row.Cells[3].Text = e.Row.Cells[3].Text.Length > 75 ? (e.Row.Cells[3].Text.Substring(0, 75) + "...") : e.Row.Cells[3].Text;
                String hs = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Historystatus").ToString().Trim());
                if (hs == "PE")
                {
                    e.Row.Cells[4].Text =StatusResource.PE;
                    e.Row.Cells[4].ForeColor = Color.Red;

                }
                else if (hs == "IP")
                {
                    e.Row.Cells[4].Text = StatusResource.IP;

                }
                else if (hs == "EV")
                {
                    e.Row.Cells[4].Text = StatusResource.EV;

                }
                else if (hs == "CL")
                {
                    e.Row.Cells[4].Text = StatusResource.CL;

                }
                else
                {
                    e.Row.Cells[4].ForeColor = Color.Red;
                }
                HtmlGenericControl hg = (HtmlGenericControl)e.Row.FindControl("dvComments");
                if (hg != null)
                    if (DataBinder.Eval(e.Row.DataItem, "Comments") != null)
                    {
                        hg.InnerText = DataBinder.Eval(e.Row.DataItem, "Comments").ToString().Trim();
                    }

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            InsertInvestigation();
            InsertActionPlan();

            Utility.ClearControls(this);
            if (AdoUDIRS.BlnError)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;

            }
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();

            //DataRow drCurrentRow = null;
            //drCurrentRow = CurrentTable.NewRow();
            //CurrentTable.Rows.Add(drCurrentRow);
            //gvActionPlan.DataSource = CurrentTable;
            //gvActionPlan.DataBind();
        }
        protected void gvActionPlan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblID");
                if (lbl != null)
                    if (DataBinder.Eval(e.Row.DataItem, "ActionPlanID") != null)
                        lbl.Text = DataBinder.Eval(e.Row.DataItem, "ActionPlanID").ToString();
                TextBox txt1 = (TextBox)e.Row.FindControl("txtAction");
                if (txt1 != null)
                    if (DataBinder.Eval(e.Row.DataItem, "ActionDescription") != null)
                        txt1.Text = DataBinder.Eval(e.Row.DataItem, "ActionDescription").ToString();
                TextBox txt2 = (TextBox)e.Row.FindControl("txtDeadline");
                if (txt2 != null)
                    if (DataBinder.Eval(e.Row.DataItem, "Deadline") != null && DataBinder.Eval(e.Row.DataItem, "Deadline").ToString() != string.Empty)
                        txt2.Text = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Deadline").ToString()).ToString("MM/dd/yyyy");
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlCompleted");
                FillDropDownList(ddl);
                if (ddl != null)
                    if (DataBinder.Eval(e.Row.DataItem, "IsCompleted") != null && DataBinder.Eval(e.Row.DataItem, "IsCompleted").ToString() != string.Empty)
                        ddl.SelectedValue = DataBinder.Eval(e.Row.DataItem, "IsCompleted").ToString();

                LinkButton lb = (LinkButton)e.Row.FindControl("lbRemove");
                if (lb != null)
                    if (ActionPlanCount == 1)
                    {
                        if (txt1.Text == string.Empty && txt2.Text == string.Empty && ddl.SelectedValue == "-1")
                            //if (e.Row.RowIndex == dt.Rows.Count - 1)
                            lb.Visible = false;
                    }
            }
        }
        protected void lbRemove_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            Label lbl = (Label)gvRow.FindControl("lblID");
            DataTable dt = null;
            int rowID = gvRow.RowIndex;
            if (lbl.Text != string.Empty)
            {
                if (DeleteActionPlanByID(lbl.Text))
                {
                    dt = FillDatatableByGrid();
                    //Successfully saved message
                }
                else
                {
                    // Message for error/not successfull.
                }
            }
            else
                dt = FillDatatableByGrid();

            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count == 0)
            {
                DataRow dr = CreateEmptyRecord(dt);
                dt.Rows.Add(dr);
            }
            BindActionPlan(dt);
        }
        #endregion

        #region Methods
        public void BindGridview()
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
                    gvCommentsHistory.DataSource = oDS.Tables[5];
                    gvCommentsHistory.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
            //gvCommentsHistory.DataSource = dt;
            //gvCommentsHistory.DataBind();
        }
        private DataTable FillDatatableByGrid()
        {
            Label lbl = null;
            TextBox txt = null;
            DropDownList ddl = null;

            DataTable dt = CreateDataTable();
            DataRow dr = null;
            foreach (GridViewRow grow in gvActionPlan.Rows)
            {
                dr = dt.NewRow();
                lbl = (Label)grow.Cells[0].FindControl("lblID");
                if (lbl != null)
                    dr["ActionPlanId"] = lbl.Text;
                txt = (TextBox)grow.Cells[0].FindControl("txtAction");
                if (txt != null)
                    dr["ActionDescription"] = txt.Text;
                txt = (TextBox)grow.Cells[0].FindControl("txtDeadline");
                if (txt != null)
                    dr["Deadline"] = txt.Text;
                ddl = (DropDownList)grow.Cells[0].FindControl("ddlCompleted");
                if (ddl != null)
                    dr["IsCompleted"] = ddl.SelectedValue;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private bool DeleteActionPlanByID(string actionPlanID)
        {
            oDV = AdoUDIRS.DeleteActionPlanByID(actionPlanID);

            if (oDV == null)
            {
                Label lbl = (Label)Page.Master.FindControl("lblMessage");
                Message.Failure("Failed to delete action plan data.", lbl); ///// Pass label saleem
                return false;
            }
            else
            {
                DataTable dt = oDV.ToTable();
                //RefId = oDV.Table.Rows[0]["IncidentID"].ToString();
                //Session["IncidentID"] = RefId;
                Label lbl = (Label)Page.Master.FindControl("lblMessage");

                //--Response.Redirect("~/Web/ThankYou.aspx", true);
                Message.Success("Action plan deleted successfully.", lbl); ///// Pass label saleem
                return true;
            }
        }
        private ArrayList GetIsCompleted()
        {

            ArrayList arr = new ArrayList();
            arr.Add(new ListItem("Yes", "Y"));
            arr.Add(new ListItem("No", "N"));

            return arr;
        }
        private void FillDropDownList(DropDownList ddl)
        {
            ArrayList arr = GetIsCompleted();
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("Select", "-1"));
            foreach (ListItem item in arr)
            {
                ddl.Items.Add(item);
            }
        }
        private void SetInitialRow()
        {

            DataTable dt = CreateDataTable();
            DataRow dr = CreateEmptyRecord(dt);
            dt.Rows.Add(dr);
            //Store the DataTable in ViewState for future reference   
            BindActionPlan(dt);
        }
        private DataRow CreateEmptyRecord(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["ActionPlanId"] = string.Empty;
            dr["ActionDescription"] = string.Empty;
            dr["Deadline"] = string.Empty;
            dr["IsCompleted"] = string.Empty;
            return dr;
        }
        private static DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("ActionPlanId", typeof(string)));
            dt.Columns.Add(new DataColumn("ActionDescription", typeof(string)));//for TextBox value   
            dt.Columns.Add(new DataColumn("Deadline", typeof(string)));//for TextBox value   
            dt.Columns.Add(new DataColumn("IsCompleted", typeof(string)));//for DropDownList selected item   
            return dt;
        }
        private void CreateAttributes()
        {
            txtComments.Attributes.Add("onkeydown", "return checkMaxLength(event, this, 'spComments');");

        }
        private void AddNewRowToGrid()
        {
            DataTable dt = FillDatatableByGrid();
            DataRow dr = CreateEmptyRecord(dt);
            dt.Rows.Add(dr);
            BindActionPlan(dt);
        }
        private void InsertActionPlan()
        {
            StringBuilder sb = new StringBuilder();
            string str = string.Empty;
            Label lbl = null;
            TextBox txt1 = null;
            TextBox txt2 = null;
            DropDownList ddl = null;

            foreach (GridViewRow row in gvActionPlan.Rows)
            {
                lbl = (Label)row.Cells[0].FindControl("lblID");
                txt1 = (TextBox)row.Cells[0].FindControl("txtAction");
                txt2 = (TextBox)row.Cells[0].FindControl("txtDeadline");
                ddl = (DropDownList)row.Cells[0].FindControl("ddlCompleted");
                if (lbl != null && txt1 != null && txt2 != null && ddl != null)
                {
                    if (lbl.Text != string.Empty || txt1.Text != string.Empty || txt2.Text != string.Empty || ddl.SelectedValue != "-1")
                    {
                        sb.Append(lbl.Text + "|$|");
                        sb.Append(txt1.Text + "|$|");
                        sb.Append(txt2.Text + "|$|");
                        sb.Append(ddl.SelectedValue + "|#|");
                    }
                }
            }
            str = sb.ToString();
            if (str.LastIndexOf("|#|") > -1)
                str = str.Substring(0, str.Length - 3);
            if (str != string.Empty)
                oDV = AdoUDIRS.DoActionPlan(IncidentID, str.ToString(), "|#|", "|$|");

            Response.Redirect("~/Web/ThankYou.aspx", true);
            /*

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sb.ToString(), connection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            lblMessage.Text = "Records successfully saved!";
             */
        }
        private void LoadAllData()
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

                    lblName.Text = oDS.Tables[2].Rows[0]["Fullname"].ToString();
                    lblContact.Text = oDS.Tables[2].Rows[0]["Mobile"].ToString();
                    lblEmail.Text = oDS.Tables[2].Rows[0]["WorkEmail"].ToString();
                    lblDateTime.Text = oDS.Tables[2].Rows[0]["IncidentDate"].ToString();
                    lblIncidentDesc.Text = oDS.Tables[2].Rows[0]["IncidentDescription"].ToString();
                    lblInjuryDesc.Text = oDS.Tables[2].Rows[0]["InjuryDescription"].ToString();
                    lblTypeTeatment.Text = oDS.Tables[2].Rows[0]["TreatmentLocation"].ToString();
                    lblAction.Text = oDS.Tables[2].Rows[0]["ActionDescription"].ToString();
                    lblAddtional.Text = oDS.Tables[2].Rows[0]["AdditionalInfo"].ToString();
                    lblReference.Text = oDS.Tables[2].Rows[0]["RefNumber"].ToString();
                    //lblPersonInjured.Text = oDS.Tables[2].Rows[0]["PersonInjured"].ToString();
                    if (ddlStatus.Items.FindByValue(oDS.Tables[2].Rows[0]["Incident_Status"].ToString()) != null)
                    {
                        ddlStatus.SelectedValue = oDS.Tables[2].Rows[0]["Incident_Status"].ToString();
                        PreviousIncidentStatus = ddlStatus.SelectedValue;
                    }
                    if (oDS.Tables[2].Rows[0]["Comments"] != null && oDS.Tables[2].Rows[0]["Comments"].ToString() != string.Empty)
                        PreviousIncidentComment = oDS.Tables[2].Rows[0]["Comments"].ToString();
                    // ____________________________________ Working Retrieve Checkboxlist _____________________________________________
                    if (oDS.Tables[4].Rows.Count > 0)
                    {
                        BindRMOCheckboxlist();
                        DisableCheckBoxList();
                    }
                    // ____________________________________ Working Retrieve Checkboxlist _____________________________________________
                    if (oDS.Tables[3].Rows.Count > 0)
                        BindActionPlan(oDS.Tables[3]);
                    else
                    {
                        SetInitialRow();
                    }


                    sLocation = oDS.Tables[2].Rows[0]["UnitName"].ToString();
                    if (sLocation == "Other")
                    {
                        lblLocation.Text = oDS.Tables[2].Rows[0]["OtherLocation"].ToString();
                    }
                    else
                    {
                        if (oDS.Tables[2].Rows[0]["UnitName"] != null)
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
        private void BindActionPlan(DataTable dt)
        {
            ActionPlanCount = dt.Rows.Count;
            gvActionPlan.DataSource = dt;
            gvActionPlan.DataBind();
        }
        private void BindRMOCheckboxlist()
        {
            foreach (DataRow row in oDS.Tables[4].Rows)
            {
                //string oIncidentID = oDS.Tables[4].Rows[0]["IncidentID"].ToString();
                if (row["Other"].ToString() == string.Empty)
                {

                    if (chkTypeofIncident.Items.FindByValue(row["Id"].ToString()) != null)
                        chkTypeofIncident.SelectedValue = row["Id"].ToString();
                    else if (chkEvaluationofLoss.Items.FindByValue(row["Id"].ToString()) != null)
                        chkEvaluationofLoss.SelectedValue = row["Id"].ToString();
                    else if (chkProbabilityofReoccurrence.Items.FindByValue(row["Id"].ToString()) != null)
                        chkProbabilityofReoccurrence.SelectedValue = row["Id"].ToString();
                    else if (chkEnvironmentEquipment.Items.FindByValue(row["Id"].ToString()) != null)
                        chkEnvironmentEquipment.SelectedValue = row["Id"].ToString();
                    else if (chkHumanFactorsWorkSystems.Items.FindByValue(row["Id"].ToString()) != null)
                        chkHumanFactorsWorkSystems.SelectedValue = row["Id"].ToString();
                }
                else
                {
                    if (chkInvestigationOthers.Items.FindByValue(row["Id"].ToString()) != null)
                        chkInvestigationOthers.SelectedValue = row["Id"].ToString();
                    txtOthers.Text = row["Other"].ToString();
                }
            }
        }
        private void DisableCheckBoxList()
        {
            foreach (ListItem li in chkTypeofIncident.Items)
                li.Enabled = false;
            foreach (ListItem li in chkEvaluationofLoss.Items)
                li.Enabled = false;
            foreach (ListItem li in chkProbabilityofReoccurrence.Items)
                li.Enabled = false;
            foreach (ListItem li in chkEnvironmentEquipment.Items)
                li.Enabled = false;
            foreach (ListItem li in chkHumanFactorsWorkSystems.Items)
                li.Enabled = false;
            foreach (ListItem li in chkInvestigationOthers.Items)
                li.Enabled = false;
        }
        protected void InsertInvestigation()
        {
            //string strIncidentID = "1";
            string strPersonID = string.Empty;

            try
            {
                if (UDIRSHelper.PersonDetails != null)
                    strPersonID = UDIRSHelper.PersonDetails.Id.ToString();
                else
                {
                    //redirect to login
                }
                BuildIncidentInvestigation();
                if (PreviousIncidentStatus == ddlStatus.SelectedValue && txtComments.Text == string.Empty && PreviousIncidentComment != string.Empty)
                    txtComments.Text = PreviousIncidentComment;

                // @PersonID ,      @IncidentID,       @IncidentSubInvestigationCausesIDs,     @OtherInvestigation,  @IncidentStatus,      @Comments)
                oDV = AdoUDIRS.InvestigationData(strPersonID, IncidentID, sbInvestigationSubCategoryIDs.ToString(), txtOthers.Text.Trim(), ddlStatus.SelectedValue, txtComments.Text.Trim());
                // string RefId = oDV.ToTable().Rows[0]["IncidentID"].ToString();
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

                    //--Response.Redirect("~/Web/ThankYou.aspx", true);
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
        private void BuildIncidentInvestigation()
        {
            BuildIDsString(chkTypeofIncident);
            BuildIDsString(chkEvaluationofLoss);
            BuildIDsString(chkProbabilityofReoccurrence);
            BuildIDsString(chkEnvironmentEquipment);
            BuildIDsString(chkHumanFactorsWorkSystems);
            BuildIDsString(chkInvestigationOthers);

        }
        private void BuildIDsString(CheckBoxList chkList)
        {
            for (int i = 0; i <= chkList.Items.Count - 1; i++)
            {
                if (chkList.Items[i].Selected)
                {
                    sbInvestigationSubCategoryIDs.Append(chkList.Items[i].Value.ToString() + "_");

                }

            }
        }
        #region Populate
        private void PopulateCheckboxlists()
        {
            PopulateTypeofIncident();
            PopulateEvaluationofLoss();
            PopulateProbabilityofReoccurrence();
            PopulateEnvironmentEquipment();
            PopulateHumanWork();
            PopulateInvestigationOthers();
        }
        protected void PopulateTypeofIncident()
        {

            try
            {
                oDV = AdoUDIRS.GetTypeofIncident();

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
                    chkTypeofIncident.DataSource = oDV;
                    chkTypeofIncident.DataValueField = "Id";
                    chkTypeofIncident.DataTextField = "Title";
                    chkTypeofIncident.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        protected void PopulateEvaluationofLoss()
        {

            try
            {
                oDV = AdoUDIRS.GetEvaluationofLoss();

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
                    chkEvaluationofLoss.DataSource = oDV;
                    chkEvaluationofLoss.DataValueField = "Id";
                    chkEvaluationofLoss.DataTextField = "Title";
                    chkEvaluationofLoss.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        protected void PopulateProbabilityofReoccurrence()
        {

            try
            {
                oDV = AdoUDIRS.GetProbabilityofReoccurrence();

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
                    chkProbabilityofReoccurrence.DataSource = oDV;
                    chkProbabilityofReoccurrence.DataValueField = "Id";
                    chkProbabilityofReoccurrence.DataTextField = "Title";
                    chkProbabilityofReoccurrence.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        protected void PopulateEnvironmentEquipment()
        {

            try
            {
                oDV = AdoUDIRS.GetEnvironmentEquipment();

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
                    chkEnvironmentEquipment.DataSource = oDV;
                    chkEnvironmentEquipment.DataValueField = "Id";
                    chkEnvironmentEquipment.DataTextField = "Title";
                    chkEnvironmentEquipment.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        protected void PopulateHumanWork()
        {

            try
            {
                oDV = AdoUDIRS.GetHumanWork();

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
                    chkHumanFactorsWorkSystems.DataSource = oDV;
                    chkHumanFactorsWorkSystems.DataValueField = "Id";
                    chkHumanFactorsWorkSystems.DataTextField = "Title";
                    chkHumanFactorsWorkSystems.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        protected void PopulateInvestigationOthers()
        {

            try
            {
                oDV = AdoUDIRS.GetInvestigationOthers();

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
                    chkInvestigationOthers.DataSource = oDV;
                    chkInvestigationOthers.DataValueField = "Id";
                    chkInvestigationOthers.DataTextField = "Title";
                    chkInvestigationOthers.DataBind();
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
                    chkInvestigationOthers.DataSource = oDV;
                    chkInvestigationOthers.DataValueField = "Id";
                    chkInvestigationOthers.DataTextField = "Title";
                    chkInvestigationOthers.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblRMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;
            }
        }
        #endregion
        #endregion
    }
}