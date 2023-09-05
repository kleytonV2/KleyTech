﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleyTech.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The name of the category is required")]
        [Display(Name = "Name of category")]
        public string Name { get; set; }
        [Display(Name = "Visualization order")]
        public int? Order { get; set; }

    }
}
