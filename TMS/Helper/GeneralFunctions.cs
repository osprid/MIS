using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS.DataModel;
using TMS.Repository;
using System.Data;
using System.Transactions;

namespace TMS
{
    public class GeneralFuctions
    {
        public enum ClickedButton
        {
            EmptyLoad,
            Load,
            New,
            Save,
            Clear,
            Edit,
            Delete,
            Cancel,
            View
        }

        public enum mode
        {
            Edit,
            New,
            Default
        }

        public void ButtonBehavior(mode strmode, ClickedButton clicked, Button btnNew, Button btnSave, Button btnClear, Button btnEdit, Button btnCancel)
        {

            if (clicked == ClickedButton.EmptyLoad)
            {
                btnNew.Visible = true;
                btnSave.Visible = false;
                btnClear.Visible = false;
                btnEdit.Visible = false;
                btnCancel.Visible = false;
            }
            if (clicked == ClickedButton.Load)
            {
                btnNew.Visible = true;
                btnSave.Visible = false;
                btnClear.Visible = false;
                btnEdit.Visible = false;
                btnCancel.Visible = false;
            }
            else if (clicked == ClickedButton.New)
            {
                btnNew.Visible = false;
                btnSave.Visible = true;
                btnClear.Visible = true;
                btnEdit.Visible = false;
                btnCancel.Visible = true;
            }
            else if (clicked == ClickedButton.Save)
            {
                btnNew.Visible = true;
                btnSave.Visible = false;
                btnClear.Visible = false;
                btnEdit.Visible = true;
                btnCancel.Visible = false;
            }
            else if (clicked == ClickedButton.Edit)
            {
                btnNew.Visible = false;
                btnSave.Visible = true;
                btnClear.Visible = false;
                btnEdit.Visible = false;
                btnCancel.Visible = true;
            }
            else if (clicked == ClickedButton.Cancel)
            {
                if (strmode == mode.Edit)
                {
                    btnNew.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnEdit.Visible = false;
                    btnCancel.Visible = false;
                }
                else
                {
                    btnNew.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnEdit.Visible = false;
                    btnCancel.Visible = false;
                }
            }
            else if (clicked == ClickedButton.View)
            {
                btnNew.Visible = true;
                btnSave.Visible = false;
                btnClear.Visible = false;
                btnEdit.Visible = true;
                btnCancel.Visible = true;
            }
        }

        public void GetComboFill(DropDownList DDL,string tableName, string Value_field, String Display_field, Boolean is_Select, Boolean is_All, Boolean IsManual)
        {
            List<object> obj_list = new List<object>();
            TMSEntities context = new TMSEntities();
           

            DDL.DataSource = obj_list;
            DDL.DataBind();
            DDL.Items.Insert(0, new ListItem("- - - Select - - -", "0"));
        }

        public List<object> GetComboFill(string tableName)
        {
            List<object> obj_list = new List<object>();

            switch (tableName)
            {
                //case "City":
                //    CityModel citymodel = new CityModel();
                //    obj_list = citymodel._GetCity();

                //    break;

                //case "Project":
                //    ProjectModel projectmodel = new ProjectModel();
                //    obj_list = projectmodel.GetProjectList();

                //    break;

                //case "UnitType":
                //    UnitTypeModel unittypemodel = new UnitTypeModel();
                //    obj_list = unittypemodel.GetUnitTypeList();

                //    break;
            }



            return obj_list;
        }



        public void ClearFields(ControlCollection pageControls)
        {
            foreach (Control contl in pageControls)
            {
                string strCntName = (contl.GetType()).Name;
                switch (strCntName)
                {
                    case "TextBox":
                        TextBox tbSource = (TextBox)contl;
                        tbSource.Text = "";
                        break;
                    case "RadioButtonList":
                        RadioButtonList rblSource = (RadioButtonList)contl;
                        rblSource.SelectedIndex = -1;
                        break;
                    case "DropDownList":
                        DropDownList ddlSource = (DropDownList)contl;
                        ddlSource.SelectedIndex = -1;
                        break;
                    case "ListBox":
                        ListBox lbsource = (ListBox)contl;
                        lbsource.SelectedIndex = -1;
                        break;
                }
                ClearFields(contl.Controls);
            }
        }

        public void FindStringInComboByCode(DropDownList ddl, int Code)
        {

            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(Code.ToString()));


        }

        public string FindStringInCombo(DropDownList ddl, int Code)
        {
            string str = "";

            if (Code.ToString() != "0")
            {
                str = ddl.Items.FindByValue(Code.ToString()).ToString();
            }

            return str;
        }
        public void ReadonlyFields(ControlCollection pageControls,bool state)
        {
            foreach (Control contl in pageControls)
            {
                string strCntName = (contl.GetType()).Name;
                switch (strCntName)
                {
                    case "TextBox":
                        TextBox tbSource = (TextBox)contl;
                        tbSource.ReadOnly = state;
                        break;
                    case "RadioButtonList":
                        RadioButtonList rblSource = (RadioButtonList)contl;
                        rblSource.Enabled = (!state);
                        break;
                    case "DropDownList":
                        DropDownList ddlSource = (DropDownList)contl;
                        ddlSource.Enabled = (!state);
                        break;
                    case "ListBox":
                        ListBox lbsource = (ListBox)contl;
                        lbsource.Enabled = state;
                        break;
                }
            }
        }

        public void FillAddressCombo(DropDownList ddlCity,DropDownList ddlState,DropDownList ddlCountry)
        {
 
        }
    }
}