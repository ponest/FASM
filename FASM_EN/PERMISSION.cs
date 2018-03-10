using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FASM_EN
{
    [Table("Permissions")]
    public partial class PERMISSION
    {
        public PERMISSION()
        {
            ROLES = new HashSet<ROLE>();
        }

        [Key]
        public int PermissionId { get; set; }

        [Required]
        [StringLength(100)]
        public string PermissionDescription { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public virtual ICollection<ROLE> ROLES { get; set; }
    }
}
