using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public enum GenderType { 
            female, 
            male
        }
        public GenderType Gender { get; set; }


    }
}
