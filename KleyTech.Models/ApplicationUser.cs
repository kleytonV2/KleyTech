using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KleyTech.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "The Name is required")]
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}
