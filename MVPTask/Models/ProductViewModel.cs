using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVPTask.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You Must enter a Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You Must enter a Price")]
        public decimal? Price { get; set; }
    }
}