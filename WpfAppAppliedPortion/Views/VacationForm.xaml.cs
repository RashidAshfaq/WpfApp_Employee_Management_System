using System;
using System.Windows;
using WpfAppAppliedPortion.Models;

namespace WpfAppAppliedPortion.Views
{
    public partial class VacationForm : Window
    {
        public VacationDays VacationDays { get; private set; }

        public VacationForm(VacationDays vacationDays = null)
        {
            InitializeComponent();
            VacationDays = vacationDays ?? new VacationDays();
            DataContext = VacationDays;
            if (vacationDays != null)
            {
                EmployeeIDTextBox.Text = vacationDays.EmployeeID.ToString();
                NumberOfDaysTextBox.Text = vacationDays.NumberOfDays.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            VacationDays.EmployeeID = int.Parse(EmployeeIDTextBox.Text);
            VacationDays.NumberOfDays = int.Parse(NumberOfDaysTextBox.Text);
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
