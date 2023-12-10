using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AppointmentTime
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public Appointment Appointment { get; set; }
        
    }
}
