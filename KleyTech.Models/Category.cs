using System.ComponentModel.DataAnnotations;

namespace KleyTech.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The name of the category is required")]
        [Display(Name = "Title")]
        public string Name { get; set; }
        [Display(Name = "Visualization order")]
        public int? Order { get; set; }

    }
}
