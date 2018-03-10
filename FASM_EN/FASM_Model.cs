using System.Data.Entity;

namespace FASM_EN
{
    public partial class FASM_Model: DbContext
    {
        public FASM_Model()
            : base("name=DBFASM")
        {
        }

        public virtual DbSet<PERMISSION> PERMISSIONS { get; set; }
        public virtual DbSet<ROLE> ROLES { get; set; }
        public virtual DbSet<USER> USERS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PERMISSION>()
                .HasMany(e => e.ROLES)
                .WithMany(e => e.PERMISSIONS)
                .Map(m => m.ToTable("MapRolePermission").MapLeftKey("PermissionId").MapRightKey("RoleId"));

            modelBuilder.Entity<ROLE>()
                .HasMany(e => e.USERS)
                .WithMany(e => e.ROLES)
                .Map(m => m.ToTable("MapUserRole").MapLeftKey("RoleId").MapRightKey("UserId"));
        }
    }
}
