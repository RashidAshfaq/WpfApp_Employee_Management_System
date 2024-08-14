using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WpfAppAppliedPortion.DataAccess;
using WpfAppAppliedPortion.Models;

namespace WpfAppAppliedPortion.Views
{
    public partial class EmployeesView : UserControl
    {
        private readonly EmployeeRepository employeeRepo;

        public EmployeesView()
        {
            InitializeComponent();
            string connectionString = "Server=DESKTOP-96DHFOL;Database=PROG32356ExamEmployee;Integrated Security=True;";
            employeeRepo = new EmployeeRepository(connectionString);
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            DataTable dt = employeeRepo.GetAllEmployees();
            EmployeesDataGrid.ItemsSource = dt.DefaultView;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeForm employeeForm = new EmployeeForm();
            if (employeeForm.ShowDialog() == true)
            {
                employeeRepo.AddEmployee(employeeForm.Employee);
                LoadEmployees();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)EmployeesDataGrid.SelectedItem;
                Employee employee = new Employee
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Name = row["Name"].ToString(),
                    Address = row["Address"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Role = row["Role"].ToString()
                };
                EmployeeForm employeeForm = new EmployeeForm(employee);
                if (employeeForm.ShowDialog() == true)
                {
                    employeeRepo.UpdateEmployee(employeeForm.Employee);
                    LoadEmployees();
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to update.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)EmployeesDataGrid.SelectedItem;
                int employeeID = Convert.ToInt32(row["ID"]);
                employeeRepo.DeleteEmployee(employeeID);
                LoadEmployees();
            }
            else
            {
                MessageBox.Show("Please select an employee to delete.");
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
        }
    }
}
