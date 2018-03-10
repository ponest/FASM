using System;
using System.ComponentModel.DataAnnotations;

namespace FASM_EN.User
{
    public class Users
    {
        [Required]
        [Display(Name = "User Id")]
        [Range(Int64.MinValue, Int64.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int64 UserId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(maximumLength: 50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(maximumLength: 50)]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [StringLength(maximumLength: 250)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(maximumLength: 50)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(maximumLength: 50)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(maximumLength: 50)]
        public string Password { get; set; }

        
        [Display(Name = "Confirm Password")]
        [StringLength(maximumLength: 50)]
        public string ConfirmPassword { get; set; }

        public System.Data.DataTable dtUsers { get; set; }

    }
}
