using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BOL;

namespace EMSstartbit.Models
{
    public class permissionReceive
    {
        [Required]
        public int employee_id { get; set; }
        [Required]
        public List<permission> permisionlist{get;set;}
    }
}
