using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;
using WpfAppAppliedPortion.DataAccess;
using WpfAppAppliedPortion.Models;
using WpfAppAppliedPortion.Views;

namespace WpfAppAppliedPortion.ViewModels
{
    public class VacationViewModel : INotifyPropertyChanged
    {
        private readonly VacationDaysRepository vacationRepo;
        private ObservableCollection<VacationDays> vacationDays;

        public VacationViewModel()
        {
            string connectionString = "Server=DESKTOP-96DHFOL;Database=PROG32356ExamEmployee;Integrated Security=True;";
            vacationRepo = new VacationDaysRepository(connectionString);
            LoadVacationDays();

            AddCommand = new RelayCommand(AddVacationDays);
            UpdateCommand = new RelayCommand(UpdateVacationDays);
            DeleteCommand = new RelayCommand(DeleteVacationDays);
            RefreshCommand = new RelayCommand(LoadVacationDays);
        }

        public ObservableCollection<VacationDays> VacationDays
        {
            get { return vacationDays; }
            set { vacationDays = value; OnPropertyChanged("VacationDays"); }
        }

        public ICommand AddCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        private void LoadVacationDays()
        {
            DataTable dt = vacationRepo.GetAllVacationDays();
            VacationDays = new ObservableCollection<VacationDays>();
            foreach (DataRow row in dt.Rows)
            {
                VacationDays.Add(new VacationDays
                {
                    ID = Convert.ToInt32(row["ID"]),
                    EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                    NumberOfDays = Convert.ToInt32(row["NumberOfDays"])
                });
            }
        }

        private void AddVacationDays(object obj)
        {
            VacationForm vacationForm = new VacationForm();
            if (vacationForm.ShowDialog() == true)
            {
                vacationRepo.UpdateVacationDays(vacationForm.VacationDays);
                LoadVacationDays();
            }
        }

        private void UpdateVacationDays(object obj)
        {
            if (obj is VacationDays selectedVacationDays)
            {
                VacationForm vacationForm = new VacationForm(selectedVacationDays);
                if (vacationForm.ShowDialog() == true)
                {
                    vacationRepo.UpdateVacationDays(vacationForm.VacationDays);
                    LoadVacationDays();
                }
            }
        }

        private void DeleteVacationDays(object obj)
        {
            if (obj is VacationDays selectedVacationDays)
            {
                vacationRepo.DeleteVacationDays(selectedVacationDays.ID);
                LoadVacationDays();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
