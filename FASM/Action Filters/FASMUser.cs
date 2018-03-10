using FASM_EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FASM.Action_Filters
{
    public class FASMUser
    {
        public Int64 UserId { get; set; }
        public bool IsSysAdmin { get; set; }
        public string Username { get; set; }
        private List<UserRole> Roles = new List<UserRole>();

        public FASMUser(string _username)
        {
            this.Username = _username;
            this.IsSysAdmin = false;
            GetDatabaseUserRolesPermissions();
        }

        private void GetDatabaseUserRolesPermissions()
        {
            using (FASM_Model _data = new FASM_Model())
            {
                USER _user = _data.USERS.Where(u => u.Username == this.Username).FirstOrDefault();
                if (_user != null)
                {
                    this.UserId = _user.UserId;
                    foreach (ROLE _role in _user.ROLES)
                    {
                        UserRole _userRole = new UserRole { RoleId = _role.RoleId, RoleName = _role.RoleName };
                        foreach (PERMISSION _permission in _role.PERMISSIONS)
                        {
                            _userRole.Permissions.Add(new RolePermission { PermissionId = _permission.PermissionId, PermissionDescription = _permission.PermissionDescription });
                        }
                        this.Roles.Add(_userRole);

                        if (!this.IsSysAdmin)
                            this.IsSysAdmin = _role.IsSysAdmin;
                    }
                }
            }
        }

        public bool HasPermission(string requiredPermission)
        {
            bool bFound = false;
            foreach (UserRole role in this.Roles)
            {
                bFound = (role.Permissions.Where(p => p.PermissionDescription.ToLower() == requiredPermission.ToLower()).ToList().Count > 0);
                if (bFound)
                    break;
            }
            return bFound;
        }

        public bool HasRole(string role)
        {
            return (Roles.Where(p => p.RoleName == role).ToList().Count > 0);
        }

        public bool HasRoles(string roles)
        {
            bool bFound = false;
            string[] _roles = roles.ToLower().Split(';');
            foreach (UserRole role in this.Roles)
            {
                try
                {
                    bFound = _roles.Contains(role.RoleName.ToLower());
                    if (bFound)
                        return bFound;
                }
                catch (Exception)
                {
                }
            }
            return bFound;
        }
    }

    public class UserRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RolePermission> Permissions = new List<RolePermission>();
    }

    public class RolePermission
    {
        public int PermissionId { get; set; }
        public string PermissionDescription { get; set; }
    }
}