using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppAppliedPortion.Models
{
    public class Payroll
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime Date { get; set; }
    }
}
