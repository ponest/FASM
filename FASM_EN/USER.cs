using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FASM_EN
{
    [Table("Users")]
        public partial class USER
        {
            public USER()
            {
                ROLES = new HashSet<ROLE>();
            }

            [Key]
            public Int64 UserId { get; set; }

            [Required]
            [StringLength(50)]
            public string Username { get; set; }

            [StringLength(50)]
            public string Firstname { get; set; }

            [StringLength(50)]
            public string Lastname { get; set; }

            [StringLength(250)]
            public string Email { get; set; }

            [StringLength(50)]
            public string PhoneNumber { get; set; }

            [StringLength(50)]
            public string Password { get; set; }

            public virtual ICollection<ROLE> ROLES { get; set; }
        }
}
