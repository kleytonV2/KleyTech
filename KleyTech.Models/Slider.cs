using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleyTech.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The name of the slider is required")]
        [Display(Name = "Title")]
        public string Name { get; set; }
        [Display(Name = "Active")]
        public bool Status { get; set; }
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
    }
}
