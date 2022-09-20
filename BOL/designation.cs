﻿using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL
{
    [Table(name: "designation", Schema = "public")]
    public class designation
    {
        [Key]

        public int designation_id { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
    }
}
