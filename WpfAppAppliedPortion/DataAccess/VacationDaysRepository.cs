using System.Data;
using WpfAppAppliedPortion.Models;

namespace WpfAppAppliedPortion.DataAccess
{
    public class VacationDaysRepository
    {
        private readonly DatabaseHelper db;

        public VacationDaysRepository(string connectionString)
        {
            db = new DatabaseHelper(connectionString);
        }

        public DataTable GetAllVacationDays()
        {
            return db.ExecuteQuery("SELECT * FROM VacationDays");
        }

        public void UpdateVacationDays(VacationDays vacationDays)
        {
            string query = $"UPDATE VacationDays SET NumberOfDays = {vacationDays.NumberOfDays} WHERE EmployeeID = {vacationDays.EmployeeID}";
            db.ExecuteNonQuery(query);
        }

        public void DeleteVacationDays(int id)
        {
            db.ExecuteNonQuery($"DELETE FROM VacationDays WHERE ID = {id}");
        }
    }
}
