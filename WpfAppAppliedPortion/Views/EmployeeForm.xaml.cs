using System;
using System.Windows;
using WpfAppAppliedPortion.Models;

namespace WpfAppAppliedPortion.Views
{
    public partial class EmployeeForm : Window
    {
        public Employee Employee { get; private set; }

        public EmployeeForm(Employee employee = null)
        {
            InitializeComponent();
            Employee = employee ?? new Employee();
            DataContext = Employee;
            if (employee != null)
            {
                NameTextBox.Text = employee.Name;
                AddressTextBox.Text = employee.Address;
                EmailTextBox.Text = employee.Email;
                PhoneTextBox.Text = employee.Phone;
                RoleTextBox.Text = employee.Role;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Employee.Name = NameTextBox.Text;
            Employee.Address = AddressTextBox.Text;
            Employee.Email = EmailTextBox.Text;
            Employee.Phone = PhoneTextBox.Text;
            Employee.Role = RoleTextBox.Text;
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
