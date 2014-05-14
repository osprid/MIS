using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace TMS
{
    public class MessageHelper
    {
        static string title = null;
        public static string ShowErrorMessage(string message,string title)
        {
            StringBuilder strResult = new StringBuilder("");
            strResult.Append("<div class='alert alert-danger alert-dismissable width_100'>");
            strResult.Append("<i class='fa fa-ban'></i>");
            strResult.Append("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>");
            if (!string.IsNullOrEmpty(message))
            {
                strResult.Append("<strong>" + title + "</strong> ");
            }
            strResult.Append("</div>");
            return strResult.ToString();
        }

        public static string ShowSuccessMessage(string message,string title)
        {
            StringBuilder strResult = new StringBuilder("");
            strResult.Append("<div class='alert alert-success alert-dismissable width_100'>");
            strResult.Append("<i class='fa fa-check'></i>");
            strResult.Append("<button type='button' class='close' data-dismiss='alert' aria-hidden='true' >×</button>");
            if (!string.IsNullOrEmpty(message))
            {
                strResult.Append("<strong>" + title + "</strong> ");
            }
            strResult.Append("</div>");
            return strResult.ToString();
        }
        
        public static string ShowInfoMessage(string message,string title)
        {
            StringBuilder strResult = new StringBuilder("");
            strResult.Append("<div class='alert alert-info alert-dismissable width_100'>");
            strResult.Append("<i class='fa fa-info'></i>");
            strResult.Append("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>");
            if (!string.IsNullOrEmpty(message))
            {
                strResult.Append("<strong>" + title + "</strong> ");
            }
            strResult.Append("</div>");
            return strResult.ToString();
        }

        public static string ShowWarningMessage(string message,string title)
        {
            StringBuilder strResult = new StringBuilder("");
            strResult.Append("<div class='alert alert-danger alert-dismissable width_100'>");
            strResult.Append("<i class='fa fa-ban'></i>");
            strResult.Append("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>");
            if (!string.IsNullOrEmpty(message))
            {
                strResult.Append("<strong>" + title + "</strong> ");
            }
            strResult.Append("</div>");
            return strResult.ToString();
        }

        public static string ShowMessage(string message)
        {
            string msg=null;
            switch (message)
            {
                case "Data_save_success":
                    title = "Data saved successfully!!";
                     msg=ShowSuccessMessage(message,title);
                    break;
                case "Data_update_success":
                    title = "Data updated successfully!!";
                    msg = ShowSuccessMessage(message,title);
                    break;
                case "Data_delete_success":
                    title = "Data deleted successfully!!";
                    msg = ShowSuccessMessage(message,title);
                    break;
                case "Data_save_error":
                    title = "There is problem while saving data!!";
                    msg = ShowErrorMessage(message,title);
                    break;
                case "Data_update_error":
                    title = "There is problem while updating data!!";
                    msg = ShowErrorMessage(message,title);
                    break;
            }
            return msg;
        }
    }
}
