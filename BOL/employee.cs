using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL
{
    [Table(name: "employee", Schema = "public")]
    public class employee
    {
        [Key]
        public int employee_id { get; set; }
        [Required]
        public string firstname { get; set; }
        public string middlename { get; set; }
        [Required]
        public string lastname { get; set; }
        public string personalemail { get; set; }
        public string officeemail { get; set; }
        public string skypeid { get; set; }
        public string currentaddressline1 { get; set; }
        public string currentaddressline2 { get; set; }

        public string currentaddressline3 { get; set; }
        public string currentcity { get; set; }
        public string currentstate { get; set; }
        public string permanentaddreaaline1 { get; set; }
        public string permanentaddreaaline2 { get; set; }
        public string permanentaddreaaline3 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }


        public string alternatephone { get; set; }
        public string panno { get; set; }
        public string pfno { get; set; }
        public DateTime? doj { get; set; }
        public DateTime? dob { get; set; }
        [Required]
        public int designation_id { get; set; }
       
        [ForeignKey("designation_id ")]
        
        public virtual designation designations { get; set; }
        [Required]
        public int department_id { get; set; }
        
        [ForeignKey("department_id  ")]
        public virtual department departments { get; set; }
        [Required]
        public int role_id { get; set; }

        [ForeignKey("role_id  ")]
        public virtual role roles { get; set; }
        [Required]
        public int shift_id { get; set; }

        [ForeignKey("shift_id")]
        public virtual shift shifts { get; set; }

        public bool is_active { get; set; }
        public string emergencycontact { get; set; }


        public string emergencycontactperson { get; set; }
        public string bloodgroup { get; set; }
        public string personrelation { get; set; }
        public double? basicpay { get; set; }
        public double? currentsalary { get; set; }
        
        public bool? billingstatus { get; set; }
        public string fathername { get; set; }
        public string mothername { get; set; }
        public DateTime?  dor { get; set; }
        public DateTime created_date { get; set; }

    }
}
