﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace UDIRS.Classes
{
    
    public static class Message
    {
        public static void Success(string messageText, Label messageControl)
        {
            messageControl.Text += messageText;
            messageControl.Attributes.Add("style", "color:green;font-family:arial;font-size:25px;");
        }
        public static void Success(string messageText, Literal messageControl)
        {
            messageControl.Text = messageText;
        }
        public static void Failure(string messageText, Label messageControl)
        {
            messageControl.Text = messageText;
            messageControl.Attributes.Add("style", "color:red;font-family:arial;font-size:25px;");
        }
        public static void Failure(string messageText, Literal messageControl)
        {
            messageControl.Text = messageText;
        }
    }
}