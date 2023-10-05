using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
