﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Collections;
//using ClassLibrary.BusinessLayer.Entities;
using System.Configuration;
using DBFun;
using UDIRS.Entities;
using System.Web.UI.WebControls;

namespace UDIRS.Classes
{
    public static class Utility
    {
        public static void ClearControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if ((c.GetType() == typeof(TextBox)))  //Clear TextBox
                {
                    ((TextBox)(c)).Text = "";
                }
                if ((c.GetType() == typeof(DropDownList)))  //Clear DropDownList
                {
                    ((DropDownList)(c)).ClearSelection();
                }
                if ((c.GetType() == typeof(CheckBox)))  //Clear CheckBox
                {
                    ((CheckBox)(c)).Checked = false;
                }
                if ((c.GetType() == typeof(CheckBoxList)))  //Clear CheckBoxList
                {
                    ((CheckBoxList)(c)).ClearSelection();
                }
                if ((c.GetType() == typeof(RadioButton)))  //Clear RadioButton
                {
                    ((RadioButton)(c)).Checked = false;
                }
                if ((c.GetType() == typeof(RadioButtonList)))  //Clear RadioButtonList
                {
                    ((RadioButtonList)(c)).ClearSelection();
                }
                if ((c.GetType() == typeof(HiddenField)))  //Clear HiddenField
                {
                    ((HiddenField)(c)).Value = "";
                }
                //if ((c.GetType() == typeof(Label)))  //Clear Label
                //{
                //    ((Label)(c)).Text = "";
                //}
                if (c.HasControls())
                {
                    ClearControls(c);
                }
            }
        }
       
    }
}