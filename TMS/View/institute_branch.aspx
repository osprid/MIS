<%@ Page Title="" Language="C#" MasterPageFile="~/View/MainMaster.Master" AutoEventWireup="true" CodeBehind="institute_branch.aspx.cs" Inherits="TMS.institute_branch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="content">
        <div class="box box-primary">
            <div class="box-header padding_left_20px">
                <h3>
                    <asp:Label ID="lblHeaderInstitute" runat="server" Text="Branch" ></asp:Label>
                </h3>
                <div class="informationDiv" id="MessageDiv">
                    <literal ID="ltrlMessage1" runat="server"></literal>
                </div>
            </div>
            <div class="box-body">
             <div class="box-body-outer min-height">
                <div class="padding_bottomtop_5px header_btn_div padding_left_20px">
                    <asp:Button ID="btnNew" align="right" class="btn btn-primary btn-flat" runat="server"
                        Text="New" onclick="btnNew_Click"  />
                    <asp:Button ID="btnSave" align="right" class="btn btn-primary btn-flat " runat="server"
                        Text="Save"   OnClientClick="return SubmitClientClick();" onclick="btnSave_Click" 
                       />
                    <asp:Button ID="btnClear" align="right" class="btn btn-primary btn-flat " runat="server"
                        Text="Clear" onclick="btnClear_Click"  />
                    <asp:Button ID="btnEdit" align="right" class="btn btn-primary btn-flat " runat="server"
                        Text="Edit" onclick="btnEdit_Click" />
                    <asp:Button ID="btnCancel" align="right" class="btn btn-primary btn-flat " runat="server"
                        Text="Cancel" OnClientClick="return ToggleDiv('Cancel');" onclick="btnCancel_Click" 
                         />
                        <div class="searchDiv">
                        <asp:Label ID="lblSearch" Text="Search" runat="server" ></asp:Label>
                        <asp:TextBox ID="txtSearch" runat="server" />
                    </div>
                </div>
                <div id="div_All_Branch" runat="server" class="box-body">
                   
                    <div class="table-responsive">
                        <asp:GridView ID="dgv_allBranch" runat="server" CssClass="table table-bordered table-striped"
                            AutoGenerateColumns="False" DataKeyNames="BranchID" >
                            <Columns>
                                <asp:BoundField HeaderText="Name" DataField="BranchName"  />
                                <asp:BoundField HeaderText="Address 1" DataField="Address1" />
                                <asp:BoundField HeaderText="Address 2" DataField="Address2" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkView" runat="server" onclick="linkView_Click">view</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="6px" CssClass="sorting" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Label ID="lblNoRecords" Text="No records to display" runat="server" ForeColor="Red"
                        ></asp:Label>
                  
                </div>
                <div id="div_NewEditBranch" runat="server">
                    <div>
                        <h3>
                            <asp:Label ID="lblHeaderInReg" runat="server" Text="Branch Registration" ></asp:Label></h3>
                    </div>
                    <table>
                        <tr>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lbl1" runat="server" Text="Branch Name" ></asp:Label></span>
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control required" Width="730px"
                                    ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="Label1" runat="server" Text="Description" ></asp:Label></span>
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control required" Width="730px"
                                    TextMode="MultiLine" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lbl2" runat="server" Text="Address 1" ></asp:Label></span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control Width_300px required"
                                    TextMode="MultiLine" ></asp:TextBox>
                            </td>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lbl3" runat="server" Text="Address 2"></asp:Label></span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control Width_300px"
                                    TextMode="MultiLine" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lbl4" runat="server" Text="Landmark" ></asp:Label></span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLandmark" runat="server" CssClass="form-control Width_300px"
                                    ></asp:TextBox>
                            </td>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lbl5" runat="server" Text="Street"></asp:Label></span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control Width_300px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lblcity" runat="server" Text="City"></asp:Label></span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control Width_300px"
                                     DataSourceID="EntityDataSource1" DataTextField="Name"
                                    DataValueField="CityID">
                                </asp:DropDownList>
                                <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=TMSEntities"
                                    DefaultContainerName="TMSEntities" EnableFlattening="False" EntitySetName="tbl_City"
                                    Select="it.[CityID], it.[Name]">
                                </asp:EntityDataSource>
                            </td>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lblpin" runat="server" Text="Pincode" ></asp:Label></span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control Width_300px required" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lblstate" runat="server" Text="State"></asp:Label></span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control Width_300px"
                                    DataSourceID="EntityDataSource2" DataTextField="Name" DataValueField="StateID"
                                    >
                                </asp:DropDownList>
                                <asp:EntityDataSource ID="EntityDataSource2" runat="server" ConnectionString="name=TMSEntities"
                                    DefaultContainerName="TMSEntities" EnableFlattening="False" EntitySetName="tbl_State"
                                    Select="it.[StateID], it.[Name]">
                                </asp:EntityDataSource>
                            </td>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lblC" runat="server" Text="Country" ></asp:Label></span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control Width_300px"
                                    DataSourceID="EntityDataSource3" DataTextField="Name" DataValueField="CountryID"
                                    >
                                </asp:DropDownList>
                                <asp:EntityDataSource ID="EntityDataSource3" runat="server" ConnectionString="name=TMSEntities"
                                    DefaultContainerName="TMSEntities" EnableFlattening="False" EntitySetName="tbl_Country"
                                    Select="it.[CountryID], it.[Name]">
                                </asp:EntityDataSource>
                                <asp:HiddenField ID="HideBranchID" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
