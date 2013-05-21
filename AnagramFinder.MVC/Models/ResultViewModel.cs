using System;
using System.Collections.Generic;

namespace AnagramFinder.MVC.Models
{
    public class ResultViewModel
    {
        public long Elapsed { get; set; }
        public List<string> Anagrams { get; set; }
        public string Term { get; set; }
        public DateTime Created { get; set; }
    }
}