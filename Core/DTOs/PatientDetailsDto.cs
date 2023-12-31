﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static API.Models.ApplicationUser;

namespace Core.DTOs
{
    public class PatientDetailsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
     
        public GenderType Gender { get; set; }

    }
}
