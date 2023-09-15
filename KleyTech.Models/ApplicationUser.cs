using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KleyTech.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "The Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Direction is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "The City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "The Country is required")]
        public string Country { get; set; }
    }
}
