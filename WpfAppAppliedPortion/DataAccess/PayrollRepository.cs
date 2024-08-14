using System;
using System.Data;
using WpfAppAppliedPortion.Models;

namespace WpfAppAppliedPortion.DataAccess
{
    public class PayrollRepository
    {
        private readonly DatabaseHelper db;

        public PayrollRepository(string connectionString)
        {
            db = new DatabaseHelper(connectionString);
        }

        public DataTable GetAllPayrolls()
        {
            return db.ExecuteQuery("SELECT * FROM Payroll");
        }

        public void AddPayroll(Payroll payroll)
        {
            string query = $"INSERT INTO Payroll (EmployeeID, HoursWorked, HourlyRate, Date) VALUES ({payroll.EmployeeID}, {payroll.HoursWorked}, {payroll.HourlyRate}, '{payroll.Date:yyyy-MM-dd}')";
            db.ExecuteNonQuery(query);

            // Increment vacation days
            db.ExecuteNonQuery($"UPDATE VacationDays SET NumberOfDays = NumberOfDays + 1 WHERE EmployeeID = {payroll.EmployeeID}");
        }

        public void UpdatePayroll(Payroll payroll)
        {
            string query = $"UPDATE Payroll SET EmployeeID = {payroll.EmployeeID}, HoursWorked = {payroll.HoursWorked}, HourlyRate = {payroll.HourlyRate}, Date = '{payroll.Date:yyyy-MM-dd}' WHERE ID = {payroll.ID}";
            db.ExecuteNonQuery(query);
        }

        public void DeletePayroll(int id)
        {
            db.ExecuteNonQuery($"DELETE FROM Payroll WHERE ID = {id}");
        }
    }
}
