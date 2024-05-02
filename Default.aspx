<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EmployeeManagement._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table class="w-100">
            <tr>
                <td id="lblEmpInfo">Employee Information</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <div>

        <table class="w-100">
            <tr>
                <td id="lblEmpCode" style="width: 450px">Employee Code</td>
                <td style="width: 507px">
                    <asp:TextBox ID="txtEmpCode" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td id="lblEmpName" style="height: 24px; width: 450px">Employee Name</td>
                <td style="height: 24px; width: 507px">
                    <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox>
                </td>
                <td style="height: 24px"></td>
            </tr>
            <tr>
                <td id="lblDOB" style="width: 450px">DOB</td>
                <td style="width: 507px">
                    <asp:TextBox ID="txtDOB" runat="server" TextMode="Date"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td id="lblGender" style="width: 450px">Gender</td>
                <td style="width: 507px">
                    <asp:RadioButtonList ID="rdoGender" runat="server">
                        <asp:ListItem Value="1">Male</asp:ListItem>
                        <asp:ListItem Value="0">Female</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td id="lblDepart" style="height: 24px; width: 450px">Department</td>
                <td style="height: 24px; width: 507px">
                    <asp:TextBox ID="txtDeparment" runat="server"></asp:TextBox>
                </td>
                <td style="height: 24px"></td>
            </tr>
            <tr>
                <td id="lblDesignation" style="width: 450px">Designation</td>
                <td style="width: 507px">
                    <asp:TextBox ID="txtDesignation" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td id="lblBS" style="width: 450px">Basic Salary </td>
                <td style="width: 507px">
                    <asp:TextBox ID="txtBasicSalary" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 450px">&nbsp;</td>
                <td style="width: 507px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 450px">&nbsp;</td>
                <td style="width: 507px">
                    <asp:Button ID="btnEnter" runat="server" Text="Enter" OnClick="btnEnter_Click" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 450px">&nbsp;</td>
                <td style="width: 507px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>

    </div>
    <div>
        <table class="w-100">
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView_SelectIndexChanged" DataKeyNames="EmployeeCode" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="EmployeeCode" HeaderText="EmployeeCode" SortExpression="EmployeeCode" />
                            <asp:BoundField DataField="EmployeeName" HeaderText="EmployeeName" SortExpression="EmployeeName" />
                            <asp:BoundField DataField="DateOfBirth" HeaderText="DateOfBirth" SortExpression="DateOfBirth" DataFormatString="{0:MM/dd/yyyy}" />
                            <%--<asp:CheckBoxField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />--%>
                            <asp:TemplateField HeaderText="Gender" SortExpression="Gender">
                                <ItemTemplate>
                                    <%-- <asp:Label ID="lblGender" runat="server" Text='<%# Eval("Gender") == true ? "Male" : "Female" %>'></asp:Label>--%>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Gender") != DBNull.Value && Convert.ToBoolean(Eval("Gender")) ? "Male" : "Female" %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                            <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation" />
                            <asp:BoundField DataField="BasicSalary" HeaderText="BasicSalary" SortExpression="BasicSalary" />
                            <asp:TemplateField HeaderText="DearnessAllowance">
                                <ItemTemplate>
                                    <asp:Label ID="lblDearnessAllowance" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ConveyanceAllowance">
                                <ItemTemplate>
                                    <asp:Label ID="lblConveyanceAllowance" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HouseRentAllowance">
                                <ItemTemplate>
                                    <asp:Label ID="lblHouseRentAllowance" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TotalSalary">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalSalary" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EmployeeInfoConnectionString %>" ProviderName="<%$ ConnectionStrings:EmployeeInfoConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [Employee]"></asp:SqlDataSource>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>