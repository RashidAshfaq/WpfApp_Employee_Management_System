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
    public class EmployeesViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeRepository employeeRepo;
        private ObservableCollection<Employee> employees;

        public EmployeesViewModel()
        {
            string connectionString = "Server=DESKTOP-96DHFOL;Database=PROG32356ExamEmployee;Integrated Security=True;";
            employeeRepo = new EmployeeRepository(connectionString);
            LoadEmployees();

            AddCommand = new RelayCommand(AddEmployee);
            UpdateCommand = new RelayCommand(UpdateEmployee);
            DeleteCommand = new RelayCommand(DeleteEmployee);
            RefreshCommand = new RelayCommand(LoadEmployees);
        }

        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set { employees = value; OnPropertyChanged("Employees"); }
        }

        public ICommand AddCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        private void LoadEmployees()
        {
            DataTable dt = employeeRepo.GetAllEmployees();
            Employees = new ObservableCollection<Employee>();
            foreach (DataRow row in dt.Rows)
            {
                Employees.Add(new Employee
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Name = row["Name"].ToString(),
                    Address = row["Address"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Role = row["Role"].ToString()
                });
            }
        }

        private void AddEmployee(object obj)
        {
            EmployeeForm employeeForm = new EmployeeForm();
            if (employeeForm.ShowDialog() == true)
            {
                employeeRepo.AddEmployee(employeeForm.Employee);
                LoadEmployees();
            }
        }

        private void UpdateEmployee(object obj)
        {
            if (obj is Employee selectedEmployee)
            {
                EmployeeForm employeeForm = new EmployeeForm(selectedEmployee);
                if (employeeForm.ShowDialog() == true)
                {
                    employeeRepo.UpdateEmployee(employeeForm.Employee);
                    LoadEmployees();
                }
            }
        }

        private void DeleteEmployee(object obj)
        {
            if (obj is Employee selectedEmployee)
            {
                employeeRepo.DeleteEmployee(selectedEmployee.ID);
                LoadEmployees();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;
        private readonly Action executeNoParam;
        private readonly Func<bool> canExecuteNoParam;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action executeNoParam, Func<bool> canExecuteNoParam = null)
        {
            this.executeNoParam = executeNoParam;
            this.canExecuteNoParam = canExecuteNoParam;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
                return canExecute(parameter);
            if (canExecuteNoParam != null)
                return canExecuteNoParam();
            return true;
        }

        public void Execute(object parameter)
        {
            if (execute != null)
                execute(parameter);
            else
                executeNoParam();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
