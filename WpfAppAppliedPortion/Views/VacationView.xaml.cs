using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WpfAppAppliedPortion.DataAccess;
using WpfAppAppliedPortion.Models;

namespace WpfAppAppliedPortion.Views
{
    public partial class VacationView : UserControl
    {
        private readonly VacationDaysRepository vacationRepo;

        public VacationView()
        {
            InitializeComponent();
            string connectionString = "Server=DESKTOP-96DHFOL;Database=PROG32356ExamEmployee;Integrated Security=True;";
            vacationRepo = new VacationDaysRepository(connectionString);
            LoadVacationDays();
        }

        private void LoadVacationDays()
        {
            DataTable dt = vacationRepo.GetAllVacationDays();
            VacationDataGrid.ItemsSource = dt.DefaultView;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            VacationForm vacationForm = new VacationForm();
            if (vacationForm.ShowDialog() == true)
            {
                vacationRepo.UpdateVacationDays(vacationForm.VacationDays);
                LoadVacationDays();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (VacationDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)VacationDataGrid.SelectedItem;
                VacationDays vacationDays = new VacationDays
                {
                    ID = Convert.ToInt32(row["ID"]),
                    EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                    NumberOfDays = Convert.ToInt32(row["NumberOfDays"])
                };
                VacationForm vacationForm = new VacationForm(vacationDays);
                if (vacationForm.ShowDialog() == true)
                {
                    vacationRepo.UpdateVacationDays(vacationForm.VacationDays);
                    LoadVacationDays();
                }
            }
            else
            {
                MessageBox.Show("Please select a vacation entry to update.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (VacationDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)VacationDataGrid.SelectedItem;
                int vacationID = Convert.ToInt32(row["ID"]);
                vacationRepo.DeleteVacationDays(vacationID);
                LoadVacationDays();
            }
            else
            {
                MessageBox.Show("Please select a vacation entry to delete.");
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadVacationDays();
        }
    }
}
