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
    public partial class governing_body : System.Web.UI.Page
    {
        static GeneralFuctions.mode mode;
        String Messages;
        GeneralFuctions general = new GeneralFuctions();
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        governingBodyModel objgoverningBodyModel = new governingBodyModel();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Session["Mode"] = GeneralFuctions.mode.Default;
                    mode = GeneralFuctions.mode.Default;
                    div_NewEditBoards.Visible = false;

                    general.ButtonBehavior(mode, GeneralFuctions.ClickedButton.Load, btnNew, btnSave, btnClear, btnEdit, btnCancel);
                    FillGrid();
                }
                ltrlMessage1.InnerHtml = "";
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                div_All_Boards.Visible = false;
                div_NewEditBoards.Visible = true;

                mode = GeneralFuctions.mode.New;
                Session["Mode"] = mode;
                ClearControls();
                general.ButtonBehavior(mode, GeneralFuctions.ClickedButton.New, btnNew, btnSave, btnClear, btnEdit, btnCancel);
                
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
        public void FillGrid()
        {
            dgv_allGeverningUni.DataSource = objgoverningBodyModel.GetGoverningList();
            dgv_allGeverningUni.DataBind();
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
                general.ReadonlyFields(div_NewEditBoards.Controls, false);
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                div_All_Boards.Visible = true;
                div_NewEditBoards .Visible = false;
                HideBoardID.Value = "";
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
                    SaveBoard();
                    ltrlMessage1.InnerHtml = MessageHelper.ShowMessage(Messages);
                }
                else if (mode == GeneralFuctions.mode.Edit)
                {
                    UpdateBoard();
                    ltrlMessage1.InnerHtml = MessageHelper.ShowMessage(Messages);
                }
                FillGrid();
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
        public string SaveBoard()
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
                tbl_GoverningBody objGoverningBody = new tbl_GoverningBody();
                objGoverningBody.Name=txtBoardName.Text;
                if (rblSubType.SelectedIndex == 0)
                {
                    objGoverningBody.IsSubType = "A";
                    objGoverningBody.ParentGoverningBody = 0;
                }
                else { objGoverningBody.IsSubType = "DA"; objGoverningBody.ParentGoverningBody = 1; }
                objGoverningBody.status = "A";
                Messages= objgoverningBodyModel.saveGoverning(objGoverningBody,objAddress);
                if (!Messages.Equals("Data_save_error") || !Messages.Equals("Data_update_error"))
                {
                    div_All_Boards.Visible = true;
                    div_NewEditBoards.Visible = false;
                    general.ButtonBehavior(GeneralFuctions.mode.Edit, GeneralFuctions.ClickedButton.Cancel, btnNew, btnSave, btnClear, btnEdit, btnCancel);
                }
            }
            catch (Exception ex) { log.Error(ex.Message); }
            return Messages;
        }
        public string UpdateBoard()
        {
            try
            {
                tbl_GoverningBody objGovern = new tbl_GoverningBody();
                objGovern.GoverningBodyID = Convert.ToInt32(HideBoardID.Value);
                objGovern.Name = txtBoardName.Text.Trim();

                tbl_Address objAddress = new tbl_Address();

                objAddress.Address1 = txtAddress1.Text;
                objAddress.Address2 = txtAddress2.Text;
                objAddress.Landmark = txtLandmark.Text;
                objAddress.Street = txtStreet.Text;
                objAddress.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                objAddress.StateID = Convert.ToInt32(ddlState.SelectedValue);
                objAddress.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
                objAddress.Postalcode = txtPincode.Text;

                Messages = objgoverningBodyModel.updateGovernBodies(objGovern, objAddress);

                div_All_Boards.Visible = true;
                div_NewEditBoards.Visible = false;
                FillGrid();
                general.ButtonBehavior(GeneralFuctions.mode.Edit, GeneralFuctions.ClickedButton.Cancel, btnNew, btnSave, btnClear, btnEdit, btnCancel);
            }
            catch (Exception ex) { log.Error(ex.Message); }
            return Messages;
        }
        protected void linkView_Click(object sender, EventArgs e)
        {
            try
            {
                div_All_Boards.Visible = false;
                div_NewEditBoards.Visible = true;
                GridViewRow clickedrow = ((LinkButton)sender).NamingContainer as GridViewRow;
                int GOVID = Convert.ToInt32(dgv_allGeverningUni.DataKeys[clickedrow.RowIndex].Value);
                HideBoardID.Value = GOVID.ToString();
                objgoverningBodyModel.getGovernBodyDetails(GOVID);
                AssignGovernBodiesData();

                general.ReadonlyFields(div_NewEditBoards.Controls, true);
                general.ButtonBehavior(mode, GeneralFuctions.ClickedButton.View, btnNew, btnSave, btnClear, btnEdit, btnCancel);
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
        private void AssignGovernBodiesData()
        {
            try
            {

                if (objgoverningBodyModel != null)
                {
                    txtBoardName.Text = objgoverningBodyModel.GovName;

                    txtAddress1.Text = objgoverningBodyModel.Address.Address1;
                    txtAddress2.Text = objgoverningBodyModel.Address.Address2;
                    txtLandmark.Text = objgoverningBodyModel.Address.Landmark;
                    txtStreet.Text = objgoverningBodyModel.Address.Street;
                    ddlCity.SelectedValue = ddlCity.Items.FindByValue(objgoverningBodyModel.Address.CityID.ToString()).Value;
                    ddlState.SelectedValue = ddlState.Items.FindByValue(objgoverningBodyModel.Address.StateID.ToString()).Value;
                    ddlCountry.SelectedValue = ddlCountry.Items.FindByValue(objgoverningBodyModel.Address.CountryID.ToString()).Value;
                    txtPincode.Text = objgoverningBodyModel.Address.PostalCode;
                }
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
    }
}