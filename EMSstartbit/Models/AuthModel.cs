using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EMSstartbit.Models
{
    public class AuthModel
    {
       [Required]
        public string name { get; set; }
       [Required]
        public string password{get; set;}
    }
}
