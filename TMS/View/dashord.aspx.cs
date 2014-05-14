using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TMS.DataModel;
using TMS.Repository;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using log4net;
namespace TMS
{
    public partial class dashbord1 : System.Web.UI.Page
    {
        static GeneralFuctions.mode mode;
        GeneralFuctions general = new GeneralFuctions();
        String Messages;
        InstituteModel objInstituteModel = new InstituteModel();
        AddressModel objAddressModel = new AddressModel();
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Session["Mode"] = GeneralFuctions.mode.Default;
                    mode = GeneralFuctions.mode.Default;


                    general.ButtonBehavior(mode, GeneralFuctions.ClickedButton.Load, btnNew, btnSave, btnClear, btnEdit, btnCancel);

                    dgv_allInstitute.DataSource = objInstituteModel.GetInstituteList();
                    dgv_allInstitute.DataBind();

                    
                }
                ltrlMessage1.InnerHtml = "";
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                div_All_Institute.Visible = false;
                div_NewEditInstitute.Visible = true;

                mode = GeneralFuctions.mode.New;
                Session["Mode"] = mode;
                ClearControls();
                general.ButtonBehavior(mode, GeneralFuctions.ClickedButton.New, btnNew, btnSave, btnClear, btnEdit, btnCancel);
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
        public void ClearControls()
        {
            general.ClearFields(this.Controls);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                div_All_Institute.Visible = true;
                div_NewEditInstitute.Visible = false;
                HideInstituteID.Value = "";
                general.ButtonBehavior(GeneralFuctions.mode.Edit, GeneralFuctions.ClickedButton.Cancel, btnNew, btnSave, btnClear, btnEdit, btnCancel);
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                mode = (GeneralFuctions.mode)Session["Mode"];
                if (mode == GeneralFuctions.mode.New)
                {
                    SaveInstitute();
                    ltrlMessage1.InnerHtml = MessageHelper.ShowMessage(Messages);
                }
                else if (mode == GeneralFuctions.mode.Edit)
                {
                    UpdateInstitute();
                    ltrlMessage1.InnerHtml = MessageHelper.ShowMessage(Messages);

                }
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
        public string SaveInstitute()
        {

            try
            {
                tbl_Address objAddress = new tbl_Address();
                objAddress.Address1 = txtAddress1.Text;
                objAddress.Address2 = txtAddress2.Text;
                objAddress.Landmark = txtLandmark.Text;
                objAddress.Street = txtStreet.Text;
                objAddress.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                objAddress.StateID = Convert.ToInt32(ddlState.SelectedValue);
                objAddress.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);

                tbl_Institute objInstitute = new tbl_Institute();
                objInstitute.Name = txtInstituteName.Text;
               
                objInstitute.Status = "A";
                Messages = objInstituteModel.saveCompleteTrans(objInstitute, objAddress);
                    
                div_All_Institute.Visible = true;
                div_NewEditInstitute.Visible = false;
                dgv_allInstitute.DataSource = objInstituteModel.GetInstituteList();
                dgv_allInstitute.DataBind();
                general.ButtonBehavior(GeneralFuctions.mode.Edit, GeneralFuctions.ClickedButton.Cancel, btnNew, btnSave, btnClear, btnEdit, btnCancel);
                
            }
            catch (Exception ex){log.Error(ex.Message);}
            finally
            {

            }
            return Messages;
        }
        protected void linkView_Click(object sender, EventArgs e)
        {
            try
            {               
                div_All_Institute.Visible = false;
                div_NewEditInstitute.Visible = true;
                GridViewRow clickedrow = ((LinkButton)sender).NamingContainer as GridViewRow;
                int Institute =Convert.ToInt32(dgv_allInstitute.DataKeys[clickedrow.RowIndex].Value);
                HideInstituteID.Value = Institute.ToString();
                objInstituteModel.getCustomerDetails(Institute);
                AssignInstituteData();               

                general.ReadonlyFields(div_NewEditInstitute.Controls, true);
                general.ButtonBehavior(mode, GeneralFuctions.ClickedButton.View, btnNew, btnSave, btnClear, btnEdit, btnCancel);
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
        public void AssignInstituteData()
        {
            try
            {

                if (objAddressModel != null)
                {
                    txtInstituteName.Text = objInstituteModel.Name;

                    txtAddress1.Text = objInstituteModel.Address.Address1;
                    txtAddress2.Text = objInstituteModel.Address.Address2;
                    txtLandmark.Text = objInstituteModel.Address.Landmark;
                    txtStreet.Text = objInstituteModel.Address.Street;

                    ddlCity.SelectedValue = ddlCity.Items.FindByValue(objInstituteModel.Address.CityID.ToString()).Value;
                    ddlState.SelectedValue = ddlState.Items.FindByValue(objInstituteModel.Address.StateID.ToString()).Value;
                    ddlCountry.SelectedValue = ddlCountry.Items.FindByValue(objInstituteModel.Address.CountryID.ToString()).Value;

                    txtPincode.Text = objInstituteModel.Address.PostalCode;
                }
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                mode = GeneralFuctions.mode.Edit;
                Session["Mode"] = mode;
                general.ButtonBehavior(mode, GeneralFuctions.ClickedButton.Edit, btnNew, btnSave, btnClear, btnEdit, btnCancel);
                general.ReadonlyFields(div_NewEditInstitute.Controls, false);
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        public string UpdateInstitute()
        {
            try
            {
                tbl_Institute objInstitute = new tbl_Institute();
                objInstitute.Institute = Convert.ToInt32(HideInstituteID.Value);
                objInstitute.Name = txtInstituteName.Text;
                objInstitute.Status = "A";

                tbl_Address objAddress = new tbl_Address();

                objAddress.Address1 = txtAddress1.Text;
                objAddress.Address2 = txtAddress2.Text;
                objAddress.Landmark = txtLandmark.Text;
                objAddress.Street = txtStreet.Text;
                objAddress.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                objAddress.StateID = Convert.ToInt32(ddlState.SelectedValue);
                objAddress.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
                objAddress.Postalcode = txtPincode.Text;
                
                Messages = objInstituteModel.updateCompleteTrans(objInstitute, objAddress);

                div_All_Institute.Visible = true;
                div_NewEditInstitute.Visible = false;
                dgv_allInstitute.DataSource = objInstituteModel.GetInstituteList();
                dgv_allInstitute.DataBind();
                general.ButtonBehavior(GeneralFuctions.mode.Edit, GeneralFuctions.ClickedButton.Cancel, btnNew, btnSave, btnClear, btnEdit, btnCancel);
            }
            catch (Exception ex) { log.Error(ex.Message); }
            return Messages;
        }
        protected override void InitializeCulture()
        {
            
                //for UI elements
                UICulture = "en";

                //for region specific formatting
                //Culture = Request.Form["DropDownList1"];
            
            base.InitializeCulture();
        }
    }
}