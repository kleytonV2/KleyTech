using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleyTech.Models
{
    public class Header
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The title of the header is required")]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Logo")]
        public string LogoURL { get; set; }
        
        [Display(Name = "Element Id")]
        public string HTML_Id { get; set; }
        
        //[Display(Name = "Class")]
        //public string HTML_Class { get; set; }
        
        [Display(Name = "Creation date")]
        public string CreationDate { get; set; }

        //[Required]
        //public int MenuId { get; set; }

        //[ForeignKey("CategoryId")]
        //public Menu Menu { get; set; }
    }
}
