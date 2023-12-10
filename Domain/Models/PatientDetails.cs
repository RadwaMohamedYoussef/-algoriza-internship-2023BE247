using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PatientDetails :ApplicationUser
    {
        public string PatientId { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

    }
}
