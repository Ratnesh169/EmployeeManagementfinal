using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Security.AccessControl;

namespace EmployeeManagement
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // This block of code will only execute when the page is initially loaded
                // It will not execute on postback events like button clicks
                GetEmployeeList();
            }
        }

        protected void GridView_SelectIndexChanged(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (GridView1.SelectedRow != null)
            {
                // Populate form controls with selected row data
                txtEmpCode.Text = GridView1.SelectedRow.Cells[1].Text;
                txtEmpName.Text = GridView1.SelectedRow.Cells[2].Text;
                txtDOB.Text = GridView1.SelectedRow.Cells[3].Text;
                // Access the Label control in the selected row
                Label lblGender = (Label)GridView1.SelectedRow.FindControl("Label1");
                if (lblGender != null)
                {
                    string genderText = lblGender.Text;
                    if (genderText.Equals("Male", StringComparison.OrdinalIgnoreCase))
                    {
                        rdoGender.SelectedValue = "1"; // Male
                    }
                    else
                    {
                        rdoGender.SelectedValue = "0"; // Female
                    }
                }
                txtDeparment.Text = GridView1.SelectedRow.Cells[5].Text;
                txtDesignation.Text = GridView1.SelectedRow.Cells[6].Text;
                txtBasicSalary.Text = GridView1.SelectedRow.Cells[7].Text;
            }
        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
            int empCode = int.Parse(txtEmpCode.Text);
            string empName = txtEmpName.Text;
            string empDOB = txtDOB.Text;
            int genderValue = 1; // Default to Male
            int.TryParse(rdoGender.SelectedValue, out genderValue);
            string department = txtDeparment.Text;
            string designation = txtDesignation.Text;
            string basicSalary = txtBasicSalary.Text;

            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-MJVV7V1E; Initial Catalog =EmployeeInfo; Persist Security Info=True;User ID = sa; Password=Ratnesh@1999;Encrypt=False"))
            {
                connection.Open();
                string query = "INSERT INTO Employee (EmployeeCode, EmployeeName, DateOfBirth, Gender, Department, Designation, BasicSalary) " +
                               "VALUES (@EmployeeCode, @EmployeeName, @DateOfBirth, @Gender, @Department, @Designation, @BasicSalary)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@EmployeeCode", empCode);
                cmd.Parameters.AddWithValue("@EmployeeName", empName);
                cmd.Parameters.AddWithValue("@DateOfBirth", empDOB);
                cmd.Parameters.AddWithValue("@Gender", genderValue);
                cmd.Parameters.AddWithValue("@Department", department);
                cmd.Parameters.AddWithValue("@Designation", designation);
                cmd.Parameters.AddWithValue("@BasicSalary", basicSalary);
                cmd.ExecuteNonQuery();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully saved.');", true);
                GetEmployeeList();
            }
        }

        void GetEmployeeList()
        {
            using (SqlConnection connection = new SqlConnection("Data Source =LAPTOP-MJVV7V1E; Initial Catalog = EmployeeInfo; Persist Security Info=True;User ID = sa; Password=Ratnesh@1999;Encrypt=False"))
            {
                connection.Open();
                string query = "SELECT * FROM Employee";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                GridView1.DataBind();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int empCode = int.Parse(txtEmpCode.Text);
            string empName = txtEmpName.Text;
            string empDOB = txtDOB.Text;
            int genderValue = 1; // Default to Male
            int.TryParse(rdoGender.SelectedValue, out genderValue);
            string department = txtDeparment.Text;
            string designation = txtDesignation.Text;
            string basicSalary = txtBasicSalary.Text;

            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-MJVV7V1E; Initial Catalog=EmployeeInfo; Persist Security Info=True;User ID=sa; Password=Ratnesh@1999;Encrypt=False"))
            {
                connection.Open();
                string query = "UPDATE Employee SET EmployeeName = @EmployeeName, DateOfBirth = @DateOfBirth, Gender = @Gender, Department = @Department, Designation = @Designation, BasicSalary = @BasicSalary WHERE EmployeeCode = @EmployeeCode";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@EmployeeCode", empCode);
                cmd.Parameters.AddWithValue("@EmployeeName", empName);
                cmd.Parameters.AddWithValue("@DateOfBirth", empDOB);
                cmd.Parameters.AddWithValue("@Gender", genderValue);
                cmd.Parameters.AddWithValue("@Department", department);
                cmd.Parameters.AddWithValue("@Designation", designation);
                cmd.Parameters.AddWithValue("@BasicSalary", basicSalary);
                cmd.ExecuteNonQuery();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Record updated successfully.');", true);
                GetEmployeeList();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Calculate DearnessAllowance
                // DearnessAllowance = BasicSalary * 40%
                double basicSalary = double.Parse(e.Row.Cells[7].Text);
                double dearNessAllowance = basicSalary * 0.4;

                // Calculate ConveyanceAllowance
                // ConveyanceAllowance = DearnessAllowance * 10% or 250 (which ever is lower)
                double conveyanceAllowance = Math.Min(dearNessAllowance * 0.1, 250);

                // Calculate HouseRentAllowance
                // HouseRentAllowance = BasicSalary * 25%   or 1500 (which ever is higher)
                double houseRentAllowance = Math.Max(basicSalary * 0.25, 1500);

                // Calculate GrossSalary
                // GrossSalary = BasicSalary + DearnessAllowance + ConveyanceAllowance + HouseRentAllowance (Do not display Gross Salary)
                double grossSalary = basicSalary + dearNessAllowance + conveyanceAllowance + houseRentAllowance;

                // Calculate PT
                // Deductions : PT = (GrossSalary <= 3000 then 100, GrossSalary > 3000 and GrossSalary<= 6000 then 150 else 200)
                double pt = (grossSalary <= 3000) ? 100 : ((grossSalary > 3000 && grossSalary <= 6000) ? 150 : 200);

                // Calculate TotalSalary
                // TotalSalary = BasicSalary + DearnessAllowance + ConveyanceAllowance + HouseRentAllowance - PT or GrossSalary - PT
                double totalSalary = grossSalary - pt;

                // Set the calculated values to the labels in the GridView
                ((Label)e.Row.FindControl("lblDearnessAllowance")).Text = dearNessAllowance.ToString();
                ((Label)e.Row.FindControl("lblConveyanceAllowance")).Text = conveyanceAllowance.ToString();
                ((Label)e.Row.FindControl("lblHouseRentAllowance")).Text = houseRentAllowance.ToString();
                ((Label)e.Row.FindControl("lblTotalSalary")).Text = totalSalary.ToString();
            }
        }

    }
}