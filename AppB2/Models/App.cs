using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppB2.Models
{
    public class App
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]      
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}