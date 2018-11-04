using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Collections;
//using ClassLibrary.BusinessLayer.Entities;
using System.Configuration;
using DBFun;
using UDIRS.Entities;

namespace UDIRS.Classes
{
    public class AdoUDIRS
    {
        public string alphabeta = "abcdefghijklmnopqrstuvwxyz" + "abcdefghijklmnopqrstuvwxyz".ToUpper();
        #region Fields
        private static bool _blnError = false;
        private static string _strError = "";
        private static string _isDuplicate = "";
        private static string _paramTooBig = "";
        //private static string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["UDIAMS"].ConnectionString;
        private static string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        #endregion
        #region Properties
        public static bool BlnError
        {
            get
            {
                if (_blnError == true)
                {
                    _blnError = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                //blnError = value;
                //_blnError = value;
            }
        }
        public static string StrError
        {
            get
            {
                if (_strError != "")
                {
                    string str = _strError;
                    _strError = "";
                    return str;
                }
                else
                {
                    return "";
                }

            }
            set { }
        }
        public static string IsDuplicateProperty
        {
            get
            {
                return _isDuplicate;
            }
            set
            {
                //_isDuplicate = value;
            }
        }
        public static string ParamTooBig
        {
            get
            {
                return _paramTooBig;
            }
            set
            {
                //_isDuplicate = value;
            }
        }
        #endregion
        //_________________________________________________________________________________
        public static DataView DoLogin(string username, string pwd)
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);
            dbFun.ExecSelectProc("DoLogin", username + "#~~#" + pwd);
            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }
        public static DataView Registration(string UserName, string Password, string FirstName, string LastName, string Mobile, string Gender)
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("UserRegistration", UserName + "#~~#" + Password + "#~~#" + FirstName + "#~~#" + LastName + "#~~#" + Mobile + "#~~#" + Gender);

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }
        #region Login
        public static DataSet AddUpdateUserDetailsByNationalID(string nationalID, string fullName, string email, string phone, string mobile)
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);
            // @username, @pwd , @firstname, @lastname, @nationIqama, @mobile , @personalEmail , @gender , @ERROR OUT)

            dbFun.ExecSelectProc("AddUpdateUserDetailsByNationalID", nationalID + "#~~#" + fullName + "#~~#" + email + "#~~#" + phone + "#~~#" + mobile);

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DSet;
        }
        public static DataSet CheckAndGetDataForOthersLogin(string userName)
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);
            // @username, @pwd , @firstname, @lastname, @nationIqama, @mobile , @personalEmail , @gender , @ERROR OUT)

            dbFun.ExecSelectProc("CheckAndGetDataForOthersLogin", userName);

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DSet;
        }
        #endregion

        //public static DataView IncidentData(string RefNumber, string UnitID, string TreatmentLocationID, string IncidentDateTime, string OtherLocation, string IncidentDesc, string IsAnyPersonInjured, string InjuryDesc, string IsTreatmentProvided, string ActionDesc, string AdditionalInfo, string Imgname, string IncidentSubCategoryID, string OtherIncident, string CorrectiveActionID)

        public static DataView IncidentData(string roleID, string UnitID, string TreatmentLocationID, string IncidentDateTime, string OtherLocation, string IncidentDesc, string IsAnyPersonInjured, string InjuryDesc, string IsTreatmentProvided, string ActionDesc, string AdditionalInfo, string Imgname, string IncidentSubCategoryID, string OtherIncident, string CorrectiveActionID, string PersonID, string Historystatus, string Comments)
        {                                    //@PersonRolesID        @UnitID,        @TreatmentLocationID,       @IncidentDateTime,        @OtherLocation,		 @IncidentDesc,		  @IsAnyPersonInjured,		@InjuryDesc,	    @IsTreatmentProvided,	   @ActionDesc,		 @AdditionalInfo,		@Imgname        @IncidentSubCategoryID,        @OtherIncident       @CorrectiveActionID                         @Historystatus

            if (IsTreatmentProvided == "N")
            {
                TreatmentLocationID = "4";
            }
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("IncidentFormData", roleID + "#~~#" + UnitID + "#~~#" + TreatmentLocationID + "#~~#" + IncidentDateTime + "#~~#" + OtherLocation + "#~~#" + IncidentDesc + "#~~#" + IsAnyPersonInjured + "#~~#" + InjuryDesc + "#~~#" +
                                                    IsTreatmentProvided + "#~~#" + ActionDesc + "#~~#" + AdditionalInfo + "#~~#" + Imgname + "#~~#" + IncidentSubCategoryID + "#~~#" + OtherIncident + "#~~#" + CorrectiveActionID + "#~~#" + PersonID + "#~~#" + Historystatus + "#~~#" + Comments);

            //dbFun.ExecSelectProc("IncidentFormData", UnitID + "#~~#" + OtherLocation + "#~~#" + IncidentDesc + "#~~#" + IsAnyPersonInjured + "#~~#" + InjuryDesc + "#~~#" +
            //                           IsTreatmentProvided + "#~~#" + ActionDesc + "#~~#");


            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }



        // ----------------------------------------------- GetInvestigationData ---------------------------------------------------------------------



        // @PersonID ,      @IncidentID,     @IncidentSubInvestigationCausesIDs,        @OtherInvestigation,       @IncidentStatus,      @Comments)
        public static DataView InvestigationData(string PersonID, string IncidentID, string IncidentSubInvestigationCausesIDs, string OtherInvestigation, string IncidentStatus, string Comments)
        {

            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("InvestigationFormData", PersonID + "#~~#" + IncidentID + "#~~#" + IncidentSubInvestigationCausesIDs + "#~~#" + OtherInvestigation + "#~~#" + IncidentStatus + "#~~#" + Comments);


            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }


        //        CREATE PROCEDURE DoActionPlan( @Incident_Id int, @StringData VARCHAR(8000), @rowDeliminater CHAR(1), @colDeliminater CHAR(1))

        public static DataView DoActionPlan(string IncidentID, string StringData, string rowDeliminater, string colDeliminater)
        {

            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("DoActionPlan", IncidentID + "#~~#" + StringData + "#~~#" + rowDeliminater + "#~~#" + colDeliminater);


            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }

        // ----------------------------------------------- Insert_VDQ Comments & Status ---------------------------------------------------------------------

        public static DataView InsertVDQCommentStatus(string PersonID, string IncidentID, string IncidentStatus, string Comments)
        {

            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("InsertVDQCommentStatus", PersonID + "#~~#" + IncidentID + "#~~#" + IncidentStatus + "#~~#" + Comments);


            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }



        // ----------------------------------------------- GetAllInformation ---------------------------------------------------------------------



        public static DataSet GetAllInformation(string RefNumber)
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetAllInformation", RefNumber);


            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            //DeleteTopEmptyRecord(dbFun.DSet);
            return dbFun.DSet;
        }


        /*
        public static DataView GetAllInformation(string RefNumber)
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetAllInformation", RefNumber);

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }
*/
        // ----------------------------------------------- GetRefNumber ---------------------------------------------------------------------
        public static DataView GetReferenceNumber()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetRefNumber", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }
        // ----------------------------------------------- GetRefNumber ---------------------------------------------------------------------
        public static DataView DeleteActionPlanByID(string actionPlanID)
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);
            dbFun.ExecActionProc("DeleteActionPlanByID", actionPlanID);


            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }

        // ----------------------------------------------- Incident History ---------------------------------------------------------------------
        public static DataView GET_IncidentsForPerson(string personID)
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GET_IncidentsForPerson", personID);

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }
        // ----------------------------------------------- Incident History ---------------------------------------------------------------------
        public static DataView GET_InvestigationHistory_Search(string refNumber, string incidentStatus, string isAnyPersonInjured, string isTreatmentProvided, string fromDate, string toDate, int personID)
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GET_InvestigationHistory_Search", refNumber + "#~~#" + incidentStatus + "#~~#" + isAnyPersonInjured + "#~~#" + isTreatmentProvided + "#~~#" + fromDate + "#~~#" + toDate + "#~~#" + personID);

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }
        // ----------------------------------------------- Incident History ---------------------------------------------------------------------
        public static DataView GET_InvestigationHistory_Search_IMO(string refNumber, string incidentStatus, string isAnyPersonInjured, string isTreatmentProvided, string fromDate, string toDate, int unitID)
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GET_InvestigationHistory_Search_IMO", refNumber + "#~~#" + incidentStatus + "#~~#" + isAnyPersonInjured + "#~~#" + isTreatmentProvided + "#~~#" + fromDate + "#~~#" + toDate + "#~~#" + unitID);

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }

        // --------------------------------------------------------  Incident Investigation  ---------------------------------------------------------------------

        public static DataView GetTypeofIncident()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetTypeofIncident", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }

        public static DataView GetEvaluationofLoss()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetEvaluationofLoss", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }

        public static DataView GetProbabilityofReoccurrence()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetProbabilityofReoccurrence", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }

        public static DataView GetEnvironmentEquipment()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetEnvironmentEquipment", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }

        public static DataView GetHumanWork()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetHumanWork", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }

        public static DataView GetInvestigationOthers()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetInvestigationOthers", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }


        // ----------------------------------------------- Nature of Incident ---------------------------------------------------------------------
        public static DataView GetTreatmentLocation()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetTreatmentLocation", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }
        public static DataView GetHealthSafety()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetHealthSafety", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }
        //_________________________________________________________________________________
        public static DataView GetInformationSystem()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetInformationSystem", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }
        //_________________________________________________________________________________
        public static DataView GetAcademicIntegrity()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetAcademicIntegrity", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }
        //_________________________________________________________________________________
        public static DataView GetRegulationsEthics()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetRegulationsEthics", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }

        public static DataView GetOthersIncident()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetOthers", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }
        public static DataView GetCorrectiveAction()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetCorrectiveAction", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }

        public static DataView GetIncidentLocation()
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);

            dbFun.ExecSelectProc("GetLocation", "");

            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }
        public static DataView ActivatePersonRole(string personRoleID)
        {
            DBFun.dbFunctions dbFun = new DBFun.dbFunctions(_connString);
            dbFun.ExecSelectProc("ActivatePersonRole", personRoleID);
            if (dbFun.blnError)
            {
                _blnError = dbFun.blnError;
                _strError = dbFun.strError;
                return null;
            }
            return dbFun.DV;
        }

       
    }
}