using System;
using System.ComponentModel.DataAnnotations;

namespace FASM_EN.User
{
    public class Roles
    {
        [Required]
        [Display(Name = "Role Id")]
        [Range(Int32.MinValue, Int32.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int32 RoleId { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        [StringLength(maximumLength: 50)]
        public string RoleName { get; set; }

        [Required]
        [Display(Name = "Is Sys Admin")]
        public bool IsSysAdmin { get; set; }

        public bool isLoad { get; set; }

        public System.Data.DataTable dtRoles { get; set; }

    }
}
