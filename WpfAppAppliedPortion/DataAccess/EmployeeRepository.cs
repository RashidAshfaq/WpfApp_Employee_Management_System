using System;
using System.Data;
using WpfAppAppliedPortion.Models;

namespace WpfAppAppliedPortion.DataAccess
{
    public class EmployeeRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public EmployeeRepository(string connectionString)
        {
            _dbHelper = new DatabaseHelper(connectionString);
        }

        public DataTable GetAllEmployees()
        {
            return _dbHelper.ExecuteQuery("SELECT * FROM Employee");
        }

        public void AddEmployee(Employee employee)
        {
            string query = $"INSERT INTO Employee (Name, Address, Email, Phone, Role) VALUES ('{employee.Name}', '{employee.Address}', '{employee.Email}', '{employee.Phone}', '{employee.Role}')";
            _dbHelper.ExecuteNonQuery(query);

            // Adding initial vacation days
            int employeeID = GetLatestEmployeeID();
            _dbHelper.ExecuteNonQuery($"INSERT INTO VacationDays (EmployeeID, NumberOfDays) VALUES ({employeeID}, 14)");
        }

        public void UpdateEmployee(Employee employee)
        {
            string query = $"UPDATE Employee SET Name = '{employee.Name}', Address = '{employee.Address}', Email = '{employee.Email}', Phone = '{employee.Phone}', Role = '{employee.Role}' WHERE ID = {employee.ID}";
            _dbHelper.ExecuteNonQuery(query);
        }

        public void DeleteEmployee(int id)
        {
            _dbHelper.ExecuteNonQuery($"DELETE FROM Employee WHERE ID = {id}");
            _dbHelper.ExecuteNonQuery($"DELETE FROM VacationDays WHERE EmployeeID = {id}");
            _dbHelper.ExecuteNonQuery($"DELETE FROM Payroll WHERE EmployeeID = {id}");
        }

        private int GetLatestEmployeeID()
        {
            DataTable dt = _dbHelper.ExecuteQuery("SELECT MAX(ID) AS ID FROM Employee");
            return Convert.ToInt32(dt.Rows[0]["ID"]);
        }
    }
}
