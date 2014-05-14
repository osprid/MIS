using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using TMS.Repository;
using TMS.DataModel;
namespace TMS
{
    public partial class institute_branch : System.Web.UI.Page
    {
        static GeneralFuctions.mode mode;
        String Messages;
        GeneralFuctions general = new GeneralFuctions();
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        BranchModel objBranchModel = new BranchModel();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Session["Mode"] = GeneralFuctions.mode.Default;
                    mode = GeneralFuctions.mode.Default;
                    div_NewEditBranch.Visible = false;

                    general.ButtonBehavior(mode, GeneralFuctions.ClickedButton.Load, btnNew, btnSave, btnClear, btnEdit, btnCancel);
                    FillGrid();
                }
                ltrlMessage1.InnerHtml = "";
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }

        private void FillGrid()
        {
            dgv_allBranch.DataSource = objBranchModel.GetBranchList();
            dgv_allBranch.DataBind();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                div_All_Branch.Visible = false;
                div_NewEditBranch.Visible = true;

                mode = GeneralFuctions.mode.New;
                Session["Mode"] = mode;
                ClearControls();
                general.ButtonBehavior(mode, GeneralFuctions.ClickedButton.New, btnNew, btnSave, btnClear, btnEdit, btnCancel);

            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
        private void ClearControls()
        {
            general.ClearFields(this.Controls);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                mode = GeneralFuctions.mode.Edit;
                Session["Mode"] = mode;
                general.ButtonBehavior(mode, GeneralFuctions.ClickedButton.Edit, btnNew, btnSave, btnClear, btnEdit, btnCancel);
                general.ReadonlyFields(div_NewEditBranch.Controls, false);
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                div_All_Branch.Visible = true;
                div_NewEditBranch.Visible = false;
                HideBranchID.Value = "";
                general.ButtonBehavior(GeneralFuctions.mode.Edit, GeneralFuctions.ClickedButton.Cancel, btnNew, btnSave, btnClear, btnEdit, btnCancel);
                FillGrid();
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
                    SaveBranch();
                    ltrlMessage1.InnerHtml = MessageHelper.ShowMessage(Messages);
                }
                else if (mode == GeneralFuctions.mode.Edit)
                {
                    UpdateBranch();
                    ltrlMessage1.InnerHtml = MessageHelper.ShowMessage(Messages);
                }
                FillGrid();
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }

        private void UpdateBranch()
        {
            try
            {
                tbl_InstituteBranch objBranch = new tbl_InstituteBranch();
                objBranch.BranchID = Convert.ToInt32(HideBranchID.Value);
                objBranch.BranchName = txtBranchName.Text.Trim();
                objBranch.description = txtDescription.Text.Trim();
                tbl_Address objAddress = new tbl_Address();

                objAddress.Address1 = txtAddress1.Text;
                objAddress.Address2 = txtAddress2.Text;
                objAddress.Landmark = txtLandmark.Text;
                objAddress.Street = txtStreet.Text;
                objAddress.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                objAddress.StateID = Convert.ToInt32(ddlState.SelectedValue);
                objAddress.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
                objAddress.Postalcode = txtPincode.Text;

                Messages = objBranchModel.updateBranch(objBranch, objAddress);

                div_All_Branch.Visible = true;
                div_NewEditBranch.Visible = false;
                FillGrid();
                general.ButtonBehavior(GeneralFuctions.mode.Edit, GeneralFuctions.ClickedButton.Cancel, btnNew, btnSave, btnClear, btnEdit, btnCancel);
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }

        private string SaveBranch()
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
                objAddress.Postalcode = txtPincode.Text;
                tbl_InstituteBranch objInstituteBranch = new tbl_InstituteBranch();
                objInstituteBranch.BranchName = txtBranchName.Text;
                objInstituteBranch.description = txtDescription.Text;
                objInstituteBranch.status = "A";
                Messages = objBranchModel.SaveBranch(objInstituteBranch,objAddress);
                if (!Messages.Equals("Data_save_error") || !Messages.Equals("Data_update_error"))
                {
                    div_All_Branch.Visible = true;
                    div_NewEditBranch.Visible = false;
                    general.ButtonBehavior(GeneralFuctions.mode.Edit, GeneralFuctions.ClickedButton.Cancel, btnNew, btnSave, btnClear, btnEdit, btnCancel);
                }
            }
            catch (Exception ex) { log.Error(ex.Message); }
            return Messages;
        }

        protected void linkView_Click(object sender, EventArgs e)
        {
            try
            {
                div_All_Branch.Visible = false;
                div_NewEditBranch.Visible = true;
                GridViewRow clickedrow = ((LinkButton)sender).NamingContainer as GridViewRow;
                int BranchID = Convert.ToInt32(dgv_allBranch.DataKeys[clickedrow.RowIndex].Value);
                HideBranchID.Value = BranchID.ToString();
                objBranchModel.getGovernBodyDetails(BranchID);
                AssignBranchData();

                general.ReadonlyFields(div_NewEditBranch.Controls, true);
                general.ButtonBehavior(mode, GeneralFuctions.ClickedButton.View, btnNew, btnSave, btnClear, btnEdit, btnCancel);
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }

        private void AssignBranchData()
        {
            try
            {
                if (objBranchModel != null)
                {
                    txtBranchName.Text = objBranchModel.BranchName;
                    txtDescription.Text = objBranchModel.BranchDesc;
                    txtAddress1.Text = objBranchModel.Address.Address1;
                    txtAddress2.Text = objBranchModel.Address.Address2;
                    txtLandmark.Text = objBranchModel.Address.Landmark;
                    txtStreet.Text = objBranchModel.Address.Street;
                    ddlCity.SelectedValue = ddlCity.Items.FindByValue(objBranchModel.Address.CityID.ToString()).Value;
                    ddlState.SelectedValue = ddlState.Items.FindByValue(objBranchModel.Address.StateID.ToString()).Value;
                    ddlCountry.SelectedValue = ddlCountry.Items.FindByValue(objBranchModel.Address.CountryID.ToString()).Value;
                    txtPincode.Text = objBranchModel.Address.PostalCode;
                }
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }

    }
}