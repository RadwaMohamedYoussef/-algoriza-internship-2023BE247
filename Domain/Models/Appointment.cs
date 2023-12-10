using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public enum WeekDays
        {
            Saturday,
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday
        }
        public enum StatusOfAppointment { Confirmed, cancelled, pending, completed }
        public StatusOfAppointment Status { get; set; }

        public WeekDays Days { get; set; }
        public ICollection<AppointmentTime> Times { get; set; }
        public DoctorDetails DoctorDetails { get; set; }
        public PatientDetails PatientDetails { get; set; }



    }
}
