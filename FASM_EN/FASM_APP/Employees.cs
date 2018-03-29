using System;
using System.ComponentModel.DataAnnotations;

namespace FASM_EN.Setups
{
    public class Employees
    {
        [Required]
        [Display(Name = "Employee Id")]
        [Range(Int32.MinValue, Int32.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int32 EmployeeId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(maximumLength: 30)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(maximumLength: 30)]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(maximumLength: 30)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        [StringLength(maximumLength: 1)]
        public string Gender { get; set; }

        [Display(Name = "Phone No")]
        [StringLength(maximumLength: 20)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [StringLength(maximumLength: 100)]
        public string Email { get; set; }

        public bool isLoad { get; set; }

        public System.Data.DataTable dtEmployee { get; set; }

    }
}
