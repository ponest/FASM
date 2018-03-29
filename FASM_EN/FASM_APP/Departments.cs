using System;
using System.ComponentModel.DataAnnotations;

namespace FASM_EN.Setups
{
    public class Departments
    {
        [Required]
        [Display(Name = "Department Id")]
        [Range(Int32.MinValue, Int32.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int32 DepartmentId { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        [StringLength(maximumLength: 100)]
        public string DepartmentName { get; set; }

        public bool isLoad { get; set; }

        public System.Data.DataTable dtDepartment { get; set; }

    }
}
