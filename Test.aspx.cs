﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Collections;
//using ClassLibrary.BusinessLayer.Entities;
using System.Configuration;
using UDIRS.Classes;

namespace UDIRS
{
    public partial class Test : System.Web.UI.Page
    {
        DataView oDV = new DataView();

        protected void Page_Load(object sender, EventArgs e)
        {
            oDV = AdoUDIRS.GetHealthSafety();
                                 
        }


       
    }
}