using System;
using Microsoft.AspNetCore.Identity;

namespace SportsStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ApplicationUser()
        {
            RegistrationDate = DateTime.Now;
        }
        public ApplicationUser(string userName) : base(userName) { }
    }
}
