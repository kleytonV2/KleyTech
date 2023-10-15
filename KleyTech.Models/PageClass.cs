using System.ComponentModel.DataAnnotations;

namespace KleyTech.Models
{
    public class PageClass
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The title of the header is required")]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Active")]
        public bool Status { get; set; }

    }
}
