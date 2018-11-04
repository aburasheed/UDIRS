using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Collections;
using ClassLibrary.BusinessLayer.Entities;
using System.Configuration;
using UDIRS.Classes;

namespace UDIRS.Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        DataView oDV = new DataView();
       
        private void Page_Init(object sender, EventArgs e)
        {
            //btnSubmit.Click += (btnSubmit_Click);
            //btnLogIn.Click += (btnLogIn_Click);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            oDV = AdoUDIRS.GetHealthSafety();
            string fruits = "";
            for (int i = 0; i < oDV.Count; i++)
            {
               
                
            }

                //oDV = AdoUDIRS.Regist("Visitor", "saleem.mas@gmail.com", "j789rt", "Saleem", "Abdul", "23326666",
                //                      "05341039", "saleem.mas@gmail.com", "Male", "");


                if (AdoUDIRS.BlnError)
                {
                    lblMsg.Text = "Error 40, sorry, an error occured, " + AdoUDIRS.StrError;

                }

           // Sub_cat.Text = oDV[0]["Title"].ToString();
        }

        private void CreateAttributes()
        {

            //btnSubmit.Attributes.Add("onclick", "javascript:return validateForm();");

        }
    }
}