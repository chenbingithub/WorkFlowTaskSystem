using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowTaskSystem.Core
{
    public static class PermissionNames
    {
        public const string Pages= "Pages";

        public const string Pages_OrganizationUnits = "Pages.OrganizationUnits";
        public const string Pages_OrganizationUnits_Nav = "Pages.OrganizationUnits.Nav";
        public const string Pages_OrganizationUnits_Create = "Pages.OrganizationUnits.Create";
        public const string Pages_OrganizationUnits_Update = "Pages.OrganizationUnits.Update";
        public const string Pages_OrganizationUnits_GetAll = "Pages.OrganizationUnits.GetAll";
        public const string Pages_OrganizationUnits_Delete = "Pages.OrganizationUnits.Delete";
        public const string Pages_OrganizationUnits_SetRole = "Pages.OrganizationUnits.SetRole";
        public const string Pages_OrganizationUnits_SetPers= "Pages.OrganizationUnits.SetPers";

        public const string Pages_Users = "Pages.Users";
        public const string Pages_Users_Nav = "Pages.Users.Nav";
        public const string Pages_Users_Create = "Pages.Users.Create";
        public const string Pages_Users_Update = "Pages.Users.Update";
        public const string Pages_Users_GetAll = "Pages.Users.GetAll";
        public const string Pages_Users_Delete = "Pages.Users.Delete";
        public const string Pages_Users_SetOrgan = "Pages.Users.SetOrgan";
        public const string Pages_Users_SetRole = "Pages.Users.SetRole";
        public const string Pages_Users_SetPers = "Pages.Users.SetPers";

        public const string Pages_Roles = "Pages.Roles";
        public const string Pages_Roles_Nav = "Pages.Roles.Nav";
        public const string Pages_Roles_Create = "Pages.Roles.Create";
        public const string Pages_Roles_Update = "Pages.Roles.Update";
        public const string Pages_Roles_GetAll = "Pages.Roles.GetAll";
        public const string Pages_Roles_Delete = "Pages.Roles.Delete";
        public const string Pages_Roles_SetPers = "Pages.Roles.SetPers";

        public const string Pages_Permissions = "Pages.Permissions";
        public const string Pages_Permissions_Nav = "Pages.Permissions.Nav";
        public const string Pages_Permissions_Create = "Pages.Permissions.Create";
        public const string Pages_Permissions_Update = "Pages.Permissions.Update";
        public const string Pages_Permissions_GetAll = "Pages.Permissions.GetAll";
        public const string Pages_Permissions_Delete = "Pages.Permissions.Delete";
    }
    public class Test {
        public static string ToName(string Name){
            if (Name == "Pages") {
                return "系统设置";
            }else if(Name == "OrganizationUnits")
            {
                return "组织部门";
            }
            else if (Name == "Users")
            {
                return "用户管理";
            }
            else if (Name == "Roles")
            {
                return "角色管理";
            }
            else if (Name == "Permissions")
            {
                return "权限管理";
            }
            else if (Name == "Create")
            {
                return "添加";
            }
            else if (Name == "Update")
            {
                return "编辑";
            }
            else if (Name == "Delete")
            {
                return "删除";
            }
            
            else if (Name == "GetAll")
            {
                return "查询";
            }
            else if (Name == "SetRole")
            {
                return "设置角色";
            }
            else if (Name == "SetPers")
            {
                return "设置权限";
            }
            else if (Name == "SetOrgan")
            {
                return "设置部门";
            }
            else if (Name == "Nav")
            {
                return "侧边栏菜单";
            }

            return Name;
        }
        public string Pages { get; set; }

        public  string Pages_OrganizationUnits { get; set; }
        public  string Pages_OrganizationUnits_Nav { get; set; }
        public  string Pages_OrganizationUnits_Create { get; set; }
        public  string Pages_OrganizationUnits_Update { get; set; }
        
        public  string Pages_OrganizationUnits_GetAll { get; set; }
        public  string Pages_OrganizationUnits_Delete { get; set; }
        public  string Pages_OrganizationUnits_SetRole { get; set; }
        public  string Pages_OrganizationUnits_SetPers { get; set; }

        public  string Pages_Users { get; set; }
        public  string Pages_Users_Nav { get; set; }
        public  string Pages_Users_Create { get; set; }
        public  string Pages_Users_Update { get; set; }
        
        public  string Pages_Users_GetAll { get; set; }
        public  string Pages_Users_Delete { get; set; }
        public  string Pages_Users_SetOrgan { get; set; }
        public  string Pages_Users_SetRole { get; set; }
        public  string Pages_Users_SetPers { get; set; }

        public  string Pages_Roles { get; set; }
        public  string Pages_Roles_Nav { get; set; }

        public  string Pages_Roles_Create { get; set; }
        public  string Pages_Roles_Update { get; set; }
        
        public  string Pages_Roles_GetAll { get; set; }
        public  string Pages_Roles_Delete { get; set; }
        public  string Pages_Roles_SetRole { get; set; }
        public  string Pages_Roles_SetPers { get; set; }

        public  string Pages_Permissions { get; set; }
        public  string Pages_Permissions_Nav { get; set; }

        public  string Pages_Permissions_Create { get; set; }
        public  string Pages_Permissions_Update { get; set; }
        
        public  string Pages_Permissions_GetAll { get; set; }
        public  string Pages_Permissions_Delete { get; set; }
    }
}
