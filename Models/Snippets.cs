using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iSnippets.Models
{
    public class Snippet
    {
        public int Id { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string snipDesc { get; set; }
        [Display(Name = "Snippet")]
        [Required]
        public string snipText { get; set; }
        [Display(Name = "Language")]
        [Required]
        public string snipLang { get; set; }
    }
}