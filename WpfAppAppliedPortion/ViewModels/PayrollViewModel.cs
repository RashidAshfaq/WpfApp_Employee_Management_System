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
    public class PayrollViewModel : INotifyPropertyChanged
    {
        private readonly PayrollRepository payrollRepo;
        private ObservableCollection<Payroll> payrolls;

        public PayrollViewModel()
        {
            string connectionString = "Server=DESKTOP-96DHFOL\\MSSQLSERVER;Database=PROG32356ExamEmployee;Integrated Security=True";
            payrollRepo = new PayrollRepository(connectionString);
            LoadPayrolls();

            AddCommand = new RelayCommand(AddPayroll);
            UpdateCommand = new RelayCommand(UpdatePayroll);
            DeleteCommand = new RelayCommand(DeletePayroll);
            RefreshCommand = new RelayCommand(LoadPayrolls);
        }

        public ObservableCollection<Payroll> Payrolls
        {
            get { return payrolls; }
            set { payrolls = value; OnPropertyChanged("Payrolls"); }
        }

        public ICommand AddCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        private void LoadPayrolls()
        {
            DataTable dt = payrollRepo.GetAllPayrolls();
            Payrolls = new ObservableCollection<Payroll>();
            foreach (DataRow row in dt.Rows)
            {
                Payrolls.Add(new Payroll
                {
                    ID = Convert.ToInt32(row["ID"]),
                    EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                    HoursWorked = Convert.ToInt32(row["HoursWorked"]),
                    HourlyRate = Convert.ToDecimal(row["HourlyRate"]),
                    Date = Convert.ToDateTime(row["Date"])
                });
            }
        }

        private void AddPayroll(object obj)
        {
            PayrollForm payrollForm = new PayrollForm();
            if (payrollForm.ShowDialog() == true)
            {
                payrollRepo.AddPayroll(payrollForm.Payroll);
                LoadPayrolls();
            }
        }

        private void UpdatePayroll(object obj)
        {
            if (obj is Payroll selectedPayroll)
            {
                PayrollForm payrollForm = new PayrollForm(selectedPayroll);
                if (payrollForm.ShowDialog() == true)
                {
                    payrollRepo.UpdatePayroll(payrollForm.Payroll);
                    LoadPayrolls();
                }
            }
        }

        private void DeletePayroll(object obj)
        {
            if (obj is Payroll selectedPayroll)
            {
                payrollRepo.DeletePayroll(selectedPayroll.ID);
                LoadPayrolls();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
