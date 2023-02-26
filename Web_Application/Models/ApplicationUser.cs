using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Web_Application.Models
{
    public class ApplicationUser : IdentityUser
    {

      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[] ProfilePicture { get; set; }

        public ICollection<Visiting> Visitings { get; set; }


    }
}
