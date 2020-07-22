using System;
using System.ComponentModel.DataAnnotations;

namespace TimeRecordWeb.Models
{
    public class TimeRecordModel
    {
        public int Id { get; set; }
        [Display(Name = "User ID")]
        public int UserId { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Clock In")]
        public DateTime ClockIn { get; set; }
        [Display(Name = "Clock Out")]
        public DateTime ClockOut { get; set; }
        public bool Active { get; set; }
        public TimeRecordModel()
        {
        }
    }
}
