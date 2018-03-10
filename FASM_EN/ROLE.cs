using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FASM_EN
{
    [Table("Roles")]
    public partial class ROLE
    {
        public ROLE()
        {
            PERMISSIONS = new HashSet<PERMISSION>();
            USERS = new HashSet<USER>();
        }

        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
        public bool IsSysAdmin { get; set; }
        public virtual ICollection<PERMISSION> PERMISSIONS { get; set; }
        public virtual ICollection<USER> USERS { get; set; }
    }
}
