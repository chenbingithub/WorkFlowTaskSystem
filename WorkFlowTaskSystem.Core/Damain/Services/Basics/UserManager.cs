using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;
using WorkFlowTaskSystem.Core.Damain.Values;

namespace WorkFlowTaskSystem.Core.Damain.Services.Basics
{
    public class UserManager: DomainService
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IPermissionInfoRepository _permissionInfoRepository;
        private IUserRoleRepository _userRoleRepository;
        private IPermissionRoleUserOrganizationUnitRepository _permissionRoleUserOrganizationUnit;
        private IOrganizationUnitUserRepository _organizationUnitUserRepository;
        private IOrganizationUnitRoleRepository _organizationUnitRoleRepository;

        public UserManager(IUserRepository userRepository, IRoleRepository roleRepository, IPermissionInfoRepository permissionInfoRepository, IPermissionRoleUserOrganizationUnitRepository permissionRoleUserOrganizationUnit, IUserRoleRepository userRoleRepository, IOrganizationUnitUserRepository organizationUnitUserRepository, IOrganizationUnitRoleRepository organizationUnitRoleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _permissionInfoRepository = permissionInfoRepository;
            _permissionRoleUserOrganizationUnit = permissionRoleUserOrganizationUnit;
            _userRoleRepository = userRoleRepository;
            _organizationUnitUserRepository = organizationUnitUserRepository;
            _organizationUnitRoleRepository = organizationUnitRoleRepository;
        }

        public Task<bool> SetRole(string userId,params string[] roleIds)
        {
            var all=_userRoleRepository.GetAll().Where(u => u.UserId == userId).ToList();
            if (all.Count > 0 && roleIds.Length >= 0)
            {
                var remove=all.Where(u => !roleIds.Contains(u.RoleId)).ToList();
                if (remove.Count > 0)
                {
                    foreach (var userRole in remove)
                    {
                        _userRoleRepository.Delete(userRole);
                    }
                }   
            }
            
            foreach (var roleId in roleIds)
            {
               if(all.Exists(u=>u.RoleId==roleId)) continue;
                 UserRole userRole=new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                };
                _userRoleRepository.Insert(userRole);
            }
            return Task.FromResult(true);
        }

        public Task<List<Role>> GetRoles(string userId)
        {
            var all = _userRoleRepository.GetAll().Where(u => u.UserId == userId).Select(r=>r.RoleId).ToList();
            if (all.Count <= 0) return Task.FromResult(new List<Role>());
            var roles=_roleRepository.GetAll().Where(u => all.Contains(u.Id)).ToList();
            return Task.FromResult(roles);
        }

        public Task<bool> SetPermission(string userId, params string[] permissionIds)
        {
            var all = _permissionRoleUserOrganizationUnit.GetAll().Where(u => u.UserId == userId).ToList();
            if (all.Count > 0 && permissionIds.Length >= 0)
            {
                var remove = all.Where(u => !permissionIds.Contains(u.PermissionId)).ToList();
                if (remove.Count > 0)
                {
                    foreach (var permission in remove)
                    {
                        _permissionRoleUserOrganizationUnit.Delete(permission);
                    }
                }
            }

            foreach (var permissionId in permissionIds)
            {
                if (all.Exists(u => u.PermissionId == permissionId)) continue;
                PermissionRoleUserOrganizationUnit permission = new PermissionRoleUserOrganizationUnit()
                {
                    PermissionId = permissionId,
                    UserId = userId
                };
                _permissionRoleUserOrganizationUnit.Insert(permission);
            }
            return Task.FromResult(true);
        }
        /// <summary>
        /// 获取该用户的直接权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<List<PermissionInfo>> GetPermissions(string userId)
        {
            var all = _permissionRoleUserOrganizationUnit.GetAll().Where(u => u.UserId == userId).Select(r => r.PermissionId).ToList();
            if (all.Count <= 0) return Task.FromResult(new List<PermissionInfo>());
            var permissionInfos = _permissionInfoRepository.GetAll().Where(u => all.Contains(u.Id)).ToList();
            return Task.FromResult(permissionInfos);
        }
        /// <summary>
        /// 设置部门
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="organizationUnitIds"></param>
        /// <returns></returns>
        public Task<bool> SetOrganizationUnit(string userId, params string[] organizationUnitIds)
        {
            var all = _organizationUnitUserRepository.GetAll().Where(u => u.UserId == userId).ToList();
            if (all.Count > 0 && organizationUnitIds.Length >= 0)
            {
                var remove = all.Where(u => !organizationUnitIds.Contains(u.OrganizationUnitId)).ToList();
                if (remove.Count > 0)
                {
                    foreach (var organization in remove)
                    {
                        _organizationUnitUserRepository.Delete(organization);
                    }
                }
            }

            foreach (var organizationUnitId in organizationUnitIds)
            {
                if (all.Exists(u => u.OrganizationUnitId == organizationUnitId)) continue;
                OrganizationUnitUser organizationUnitUser = new OrganizationUnitUser()
                {
                    UserId = userId,
                    OrganizationUnitId = organizationUnitId
                };
                _organizationUnitUserRepository.Insert(organizationUnitUser);
            }
            return Task.FromResult(true);
        }

        /// <summary>
        /// 获取该用户下的所有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<List<PermissionInfo>> GetAllPermissions(string userId)
        {
            var roles=_userRoleRepository.GetAll().Where(u => u.UserId == userId).Select(u => u.RoleId).ToList();
            var organizationUnits = _organizationUnitUserRepository.GetAll().Where(u => u.UserId == userId).Select(u => u.OrganizationUnitId).ToList();
            var oroles= _organizationUnitRoleRepository.GetAll().Where(u => organizationUnits.Contains(u.OrganizationUnitId))
                .Select(r => r.RoleId).ToList();
            if (oroles.Count > 0)
            {
                roles.AddRange(oroles);
            }
            var all = _permissionRoleUserOrganizationUnit.GetAll().Where(u => u.UserId == userId|| roles.Contains(u.RoleId)||organizationUnits.Contains(u.OrganizationUnitId)).Select(r => r.PermissionId).ToList();
            var permissionInfos = _permissionInfoRepository.GetAll().Where(u => all.Contains(u.Id)).ToList();
            return Task.FromResult(permissionInfos);
        }
    }
}
