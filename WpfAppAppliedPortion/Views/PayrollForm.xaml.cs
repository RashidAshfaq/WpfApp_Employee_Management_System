using System;
using System.Windows;
using WpfAppAppliedPortion.Models;

namespace WpfAppAppliedPortion.Views
{
    public partial class PayrollForm : Window
    {
        public Payroll Payroll { get; private set; }

        public PayrollForm(Payroll payroll = null)
        {
            InitializeComponent();
            Payroll = payroll ?? new Payroll();
            DataContext = Payroll;
            if (payroll != null)
            {
                EmployeeIDTextBox.Text = payroll.EmployeeID.ToString();
                HoursWorkedTextBox.Text = payroll.HoursWorked.ToString();
                HourlyRateTextBox.Text = payroll.HourlyRate.ToString();
                DatePicker.SelectedDate = payroll.Date;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Payroll.EmployeeID = int.Parse(EmployeeIDTextBox.Text);
            Payroll.HoursWorked = int.Parse(HoursWorkedTextBox.Text);
            Payroll.HourlyRate = decimal.Parse(HourlyRateTextBox.Text);
            Payroll.Date = DatePicker.SelectedDate.Value;
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
