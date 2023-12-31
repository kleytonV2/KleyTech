﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KleyTech.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The name of the article is required")]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The description is required")]
        public string Description { get; set; }

        [Display(Name = "Creation date")]
        public string CreationDate { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string ImageURL { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
