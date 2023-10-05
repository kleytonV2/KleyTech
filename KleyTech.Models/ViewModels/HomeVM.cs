using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleyTech.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Header> HeaderList {  get; set; }
        public IEnumerable<Slider> Slider {  get; set; }
        public IEnumerable<Article> ArticleList {  get; set; }
    }
}
