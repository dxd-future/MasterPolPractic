using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterPol.Models
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Column("last_name"), Required, MaxLength(100)]
        public string LastName { get; set; }

        [Column("first_name"), Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Column("middle_name"), MaxLength(100)]
        public string MiddleName { get; set; }

        [Required, MaxLength(100)]
        public string Position { get; set; }

        [Required, MaxLength(20)]
        public string Phone { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Column("hire_date")]
        public DateTime HireDate { get; set; }

        public decimal? Salary { get; set; }
    }
}