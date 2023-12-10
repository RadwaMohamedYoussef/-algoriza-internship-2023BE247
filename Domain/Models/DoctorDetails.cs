using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DoctorDetails :ApplicationUser
    {
        public string DoctorId { get; set; }
        public string price { get; set; }
       public Specialization Specialization { get; set; }
        public ICollection<Appointment> Appointments { get; set; } 
    }
}
