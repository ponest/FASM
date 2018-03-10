using System.ComponentModel.DataAnnotations;

namespace FASM_EN.User
{
    public class Login
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(maximumLength: 50)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(maximumLength: 50)]
        public string Password { get; set; }

        public System.Data.DataTable dtLogin { get; set; }
    }
}
