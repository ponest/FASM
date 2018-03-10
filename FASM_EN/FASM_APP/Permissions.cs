
using System;
using System.ComponentModel.DataAnnotations;

namespace FASM_EN.User
{
    public class Permissions
    {
        [Required]
        [Display(Name = "Permission Id")]
        [Range(Int32.MinValue, Int32.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int32 PermissionId { get; set; }

        [Required]
        [Display(Name = "Permission Description")]
        [StringLength(maximumLength: 100)]
        public string PermissionDescription { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(maximumLength: 100)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Module Name")]
        [StringLength(maximumLength: 50)]
        public string ModName { get; set; }

        public System.Data.DataTable dtPermissions { get; set; }

    }
}
