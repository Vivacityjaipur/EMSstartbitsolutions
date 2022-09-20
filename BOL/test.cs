using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL
{
    [Table(name: "test", Schema = "public")]
    public class test
    {
        [Key]

        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public int no { get; set; }
       
    }
}