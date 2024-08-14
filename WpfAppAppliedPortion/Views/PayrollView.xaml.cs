using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WpfAppAppliedPortion.DataAccess;
using WpfAppAppliedPortion.Models;

namespace WpfAppAppliedPortion.Views
{
    public partial class PayrollView : UserControl
    {
        private readonly PayrollRepository payrollRepo;

        public PayrollView()
        {
            InitializeComponent();
            string connectionString = "Server=DESKTOP-96DHFOL;Database=PROG32356ExamEmployee;Integrated Security=True;";
            payrollRepo = new PayrollRepository(connectionString);
            LoadPayrolls();
        }

        private void LoadPayrolls()
        {
            DataTable dt = payrollRepo.GetAllPayrolls();
            PayrollDataGrid.ItemsSource = dt.DefaultView;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            PayrollForm payrollForm = new PayrollForm();
            if (payrollForm.ShowDialog() == true)
            {
                payrollRepo.AddPayroll(payrollForm.Payroll);
                LoadPayrolls();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (PayrollDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)PayrollDataGrid.SelectedItem;
                Payroll payroll = new Payroll
                {
                    ID = Convert.ToInt32(row["ID"]),
                    EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                    HoursWorked = Convert.ToInt32(row["HoursWorked"]),
                    HourlyRate = Convert.ToDecimal(row["HourlyRate"]),
                    Date = Convert.ToDateTime(row["Date"])
                };
                PayrollForm payrollForm = new PayrollForm(payroll);
                if (payrollForm.ShowDialog() == true)
                {
                    payrollRepo.UpdatePayroll(payrollForm.Payroll);
                    LoadPayrolls();
                }
            }
            else
            {
                MessageBox.Show("Please select a payroll entry to update.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PayrollDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)PayrollDataGrid.SelectedItem;
                int payrollID = Convert.ToInt32(row["ID"]);
                payrollRepo.DeletePayroll(payrollID);
                LoadPayrolls();
            }
            else
            {
                MessageBox.Show("Please select a payroll entry to delete.");
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPayrolls();
        }
    }
}
