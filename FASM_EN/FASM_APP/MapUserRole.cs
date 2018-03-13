using System;
using System.ComponentModel.DataAnnotations;

namespace FASM_EN.User
{
    public class MapUserRole
    {
        [Required]
        [Display(Name = "User Id")]
        [Range(Int32.MinValue, Int32.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int32 UserId { get; set; }

        public Int32 RoleId { get; set; }

    }
}

