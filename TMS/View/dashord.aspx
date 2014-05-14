<%@ Page Title="" Language="C#" MasterPageFile="~/View/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="dashord.aspx.cs" Inherits="TMS.dashbord1" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../js/UserDefined.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#<%=lblNoRecords.ClientID%>').css('display', 'none');

            $(document).on("keyup", '#<%=txtSearch.ClientID %>', function () {
                $('#<%=lblNoRecords.ClientID%>').css('display', 'none'); // Hide No records to display label.
                $("#<%=dgv_allInstitute.ClientID%> tr:has(td)").hide(); // Hide all the rows.
                var iCounter = 0;
                var sSearchTerm = $('#<%=txtSearch.ClientID%>').val(); //Get the search box value
                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#<%=dgv_allInstitute.ClientID%> tr:has(td)").show();
                    return false;
                }
                //Iterate through all the td.
                $("#<%=dgv_allInstitute.ClientID%> tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });

                if (iCounter == 0) {
                    $('#<%=lblNoRecords.ClientID%>').css('display', '');
                }
            });
        })
    </script>
    <script type="text/javascript" language="javascript">
        function SubmitClientClick() {
           return ValidateRequiredField();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


   <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        <asp:Label ID="lblHeaderInstitute" runat="server" Text="Institute Managments" meta:resourcekey="lblHeaderInstituteResource1"></asp:Label>
                        <small>Preview</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li><a href="#">Master</a></li>
                        <li class="active">Institute</li>
                    </ol>
                </section>

    <div class="content">
        <div class="box box-primary ">
            <div class="box-header padding_left_20px">
                <h3>
                    
                </h3>
                <div class="informationDiv" id="MessageDiv">
                    <literal ID="ltrlMessage1" runat="server"></literal>
                </div>
            </div>
            <div class="box-body">
            <div class="box-body-outer min-height">
                <div class="padding_bottomtop_5px header_btn_div padding_left_20px">
                    <asp:Button ID="btnNew" align="right" class="btn btn-primary btn-flat" runat="server"
                        Text="New" OnClick="btnNew_Click" meta:resourcekey="btnNewResource1" />
                    <asp:Button ID="btnSave" align="right" class="btn btn-primary btn-flat " runat="server"
                        Text="Save" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" OnClientClick="return SubmitClientClick();"/>
                    <asp:Button ID="btnClear" align="right" class="btn btn-primary btn-flat " runat="server"
                        Text="Clear" OnClick="btnClear_Click" meta:resourcekey="btnClearResource1" />
                    <asp:Button ID="btnEdit" align="right" class="btn btn-primary btn-flat " runat="server"
                        Text="Edit" OnClick="btnEdit_Click" meta:resourcekey="btnEditResource1" />
                    <asp:Button ID="btnCancel" align="right" class="btn btn-primary btn-flat " runat="server"
                        Text="Cancel" OnClientClick="return ToggleDiv('Cancel');" OnClick="btnCancel_Click"
                        meta:resourcekey="btnCancelResource1" />
                        <div class="searchDiv">
                        <asp:Label ID="lblSearch" Text="Search" runat="server" meta:resourcekey="lblSearchResource1"></asp:Label>
                        <asp:TextBox ID="txtSearch" runat="server" meta:resourcekey="txtSearchResource1" />
                    </div>
                </div>
                <div id="div_All_Institute" runat="server" class="box-body">
                    
                    <div class="table-responsive">
                        <asp:GridView ID="dgv_allInstitute" runat="server" CssClass="table table-bordered table-striped"
                            AutoGenerateColumns="False" DataKeyNames="Institute" meta:resourcekey="dgv_allInstituteResource1">
                            <Columns>
                                <asp:BoundField HeaderText="Institute Name" DataField="Name" meta:resourcekey="BoundFieldResource1" />
                                <asp:BoundField HeaderText="Address 1" DataField="Address1" meta:resourcekey="BoundFieldResource2" />
                                <asp:BoundField HeaderText="Address 2" DataField="Address2" meta:resourcekey="BoundFieldResource3" />
                                <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkView" runat="server" OnClick="linkView_Click" meta:resourcekey="linkViewResource1">view</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="6px" CssClass="sorting" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Label ID="lblNoRecords" Text="No records to display" runat="server" ForeColor="Red"
                        meta:resourcekey="lblNoRecordsResource1"></asp:Label>
                    <div class="Pager">
                    </div>
                </div>
                <div id="div_NewEditInstitute" runat="server" visible="false">
                    <div>
                        <h3>
                            <asp:Label ID="lblHeaderInReg" runat="server" Text="Institute Registration" meta:resourcekey="lblHeaderInRegResource1"></asp:Label></h3>
                    </div>
                    <table>
                        <tr>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lbl1" runat="server" Text="Institute Name" meta:resourcekey="lbl1Resource1"></asp:Label></span>
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtInstituteName" runat="server" CssClass="form-control required" Width="730px"
                                    meta:resourcekey="txtInstituteNameResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lbl2" runat="server" Text="Address 1" meta:resourcekey="lbl2Resource1"></asp:Label></span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control Width_300px required"
                                    TextMode="MultiLine" meta:resourcekey="txtAddress1Resource1"></asp:TextBox>
                            </td>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lbl3" runat="server" Text="Address 2" meta:resourcekey="lbl3Resource1"></asp:Label></span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control Width_300px"
                                    TextMode="MultiLine" meta:resourcekey="txtAddress2Resource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lbl4" runat="server" Text="Landmark" meta:resourcekey="lbl4Resource1"></asp:Label></span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLandmark" runat="server" CssClass="form-control Width_300px"
                                    meta:resourcekey="txtLandmarkResource1"></asp:TextBox>
                            </td>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lbl5" runat="server" Text="Street" meta:resourcekey="lbl5Resource1"></asp:Label></span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control Width_300px" meta:resourcekey="txtStreetResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lblcity" runat="server" Text="City" meta:resourcekey="lblcityResource1"></asp:Label></span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control Width_300px"
                                    meta:resourcekey="ddlCityResource1" DataSourceID="EntityDataSource1" DataTextField="Name"
                                    DataValueField="CityID">
                                </asp:DropDownList>
                                <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=TMSEntities"
                                    DefaultContainerName="TMSEntities" EnableFlattening="False" EntitySetName="tbl_City"
                                    Select="it.[CityID], it.[Name]">
                                </asp:EntityDataSource>
                            </td>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lblpin" runat="server" Text="Pincode" meta:resourcekey="lblpinResource1"></asp:Label></span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control Width_300px required" meta:resourcekey="txtPincodeResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lblstate" runat="server" Text="State" meta:resourcekey="lblstateResource1"></asp:Label></span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control Width_300px"
                                    DataSourceID="EntityDataSource2" DataTextField="Name" DataValueField="StateID"
                                    meta:resourcekey="ddlStateResource1">
                                </asp:DropDownList>
                                <asp:EntityDataSource ID="EntityDataSource2" runat="server" ConnectionString="name=TMSEntities"
                                    DefaultContainerName="TMSEntities" EnableFlattening="False" EntitySetName="tbl_State"
                                    Select="it.[StateID], it.[Name]">
                                </asp:EntityDataSource>
                            </td>
                            <td>
                                <span class="formLabels">
                                    <asp:Label ID="lblC" runat="server" Text="Country" meta:resourcekey="lblCResource1"></asp:Label></span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control Width_300px"
                                    DataSourceID="EntityDataSource3" DataTextField="Name" DataValueField="CountryID"
                                    meta:resourcekey="ddlCountryResource1">
                                </asp:DropDownList>
                                <asp:EntityDataSource ID="EntityDataSource3" runat="server" ConnectionString="name=TMSEntities"
                                    DefaultContainerName="TMSEntities" EnableFlattening="False" EntitySetName="tbl_Country"
                                    Select="it.[CountryID], it.[Name]">
                                </asp:EntityDataSource>
                                <asp:HiddenField ID="HideInstituteID" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            </div>
        </div>
    </div>
</asp:Content>
