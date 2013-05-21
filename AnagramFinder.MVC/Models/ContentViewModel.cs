using System.Collections.Generic;

namespace AnagramFinder.MVC.Models
{
    public class ContentViewModel
    {
        public string FileName { get; set; }
        public string Content { get; set; }
        public List<ResultViewModel> Results { get; set; } 
    }
}