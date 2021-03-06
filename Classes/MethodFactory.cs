﻿using System.Text;
using System.Web.UI;
using System;

using System.Net.Mail;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
// GemBox is free only if less than 150 rows

namespace UDIRS.Classes
{
    public class MethodFactory
    {
        private static bool _blnError = false;
        private static string _strError = "";
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
        protected static void ClearSelectBox(ref DropDownList list)
        {
            for (int x = list.Items.Count - 1; x > -1; x--)
            {
                list.Items.RemoveAt(x);
            }
            //list.Items.Clear();
        }
        public static string GetMonthName(int month, bool abbreviate, IFormatProvider provider)
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(provider);
            if (abbreviate) return info.GetAbbreviatedMonthName(month);
            return info.GetMonthName(month);
        }
        public static string GetMonthName(int month, bool isAbbrev)
        {
            return GetMonthName(month, isAbbrev, null);
        }
        public static string GetMonthName(int month, IFormatProvider provider)
        {
            return GetMonthName(month, false, provider);
        }
        public static string GetMonthName(int month)
        {
            return GetMonthName(month, false, null);
        }
        public static void BindStatus(ref DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--Select--", "0"));
            ddl.Items.Add(new ListItem(StatusResource.PE, "PE"));
            ddl.Items.Add(new ListItem(StatusResource.IP, "IP"));
            ddl.Items.Add(new ListItem(StatusResource.EV, "EV"));
            ddl.Items.Add(new ListItem(StatusResource.CL, "CL"));
        }
        public static void BindStatusSearch(ref DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--Select--", "0"));
            ddl.Items.Add(new ListItem(StatusResource.PE, "PE"));
            ddl.Items.Add(new ListItem(StatusResource.IP, "IP"));
            ddl.Items.Add(new ListItem(StatusResource.EV, "EV"));
            ddl.Items.Add(new ListItem(StatusResource.CL, "CL"));
            ddl.Items.Add(new ListItem(StatusResource.RP, "RP"));
        }
    }
}
