using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KleyTech.Models
{
    public class Block
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        [Display(Name = "HTML")]
        public string HTML_Content { get; set; }
        public int Order { get; set; }
        [Required]
        public int PageClassId { get; set; }
        [ForeignKey("PageClassId")]
        public PageClass PageClass { get; set; }

    }
}
