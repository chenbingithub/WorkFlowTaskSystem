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
    public class OrganizationUnitManager:DomainService
    {
        private IOrganizationUnitRoleRepository _organizationUnitRoleRepository;
        private IOrganizationUnitUserRepository _organizationUnitUserRepository;
        private IPermissionRoleUserOrganizationUnitRepository _permissionRoleUserOrganizationUnitRepository;
        private IRoleRepository _roleRepository;
        private IUserRepository _userRepository;
        private IPermissionInfoRepository _permissionInfoRepository;
        private IOrganizationUnitRepository _organizationUnitRepository;
        public OrganizationUnitManager(IOrganizationUnitRoleRepository organizationUnitRoleRepository, IOrganizationUnitUserRepository organizationUnitUserRepository, IPermissionRoleUserOrganizationUnitRepository permissionRoleUserOrganizationUnitRepository, IRoleRepository roleRepository, IUserRepository userRepository, IPermissionInfoRepository permissionInfoRepository, IOrganizationUnitRepository organizationUnitRepository)
        {
            _organizationUnitRoleRepository = organizationUnitRoleRepository;
            _organizationUnitUserRepository = organizationUnitUserRepository;
            _permissionRoleUserOrganizationUnitRepository = permissionRoleUserOrganizationUnitRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _permissionInfoRepository = permissionInfoRepository;
            _organizationUnitRepository = organizationUnitRepository;
        }

        public Task<bool> SetRole(string organizationUnitId, params string[] roleIds)
        {
            var all = _organizationUnitRoleRepository.GetAll().Where(u => u.OrganizationUnitId == organizationUnitId).ToList();
            if (all.Count > 0 && roleIds.Length >= 0)
            {
                var remove = all.Where(u => !roleIds.Contains(u.RoleId)).ToList();
                if (remove.Count > 0)
                {
                    foreach (var userRole in remove)
                    {
                        _organizationUnitRoleRepository.Delete(userRole);
                    }
                }
            }

            foreach (var roleId in roleIds)
            {
                if (all.Exists(u => u.RoleId == roleId)) continue;
                OrganizationUnitRole userRole = new OrganizationUnitRole()
                {
                    RoleId = roleId,
                    OrganizationUnitId = organizationUnitId
                };
                _organizationUnitRoleRepository.Insert(userRole);
            }
            return Task.FromResult(true);
        }

        public Task<List<Role>> GetRoles(string organizationUnitId)
        {
            var all = _organizationUnitRoleRepository.GetAll().Where(u => u.OrganizationUnitId == organizationUnitId).Select(r => r.RoleId).ToList();
            if (all.Count <= 0) return Task.FromResult(new List<Role>());
            var roles = _roleRepository.GetAll().Where(u => all.Contains(u.Id)).ToList();
            return Task.FromResult(roles);
        }

        public Task<bool> SetPermission(string organizationUnitId, params string[] permissionIds)
        {
            var all = _permissionRoleUserOrganizationUnitRepository.GetAll().Where(u => u.OrganizationUnitId == organizationUnitId).ToList();
            if (all.Count > 0 && permissionIds.Length >= 0)
            {
                var remove = all.Where(u => !permissionIds.Contains(u.PermissionId)).ToList();
                if (remove.Count > 0)
                {
                    foreach (var permission in remove)
                    {
                        _permissionRoleUserOrganizationUnitRepository.Delete(permission);
                    }
                }
            }

            foreach (var permissionId in permissionIds)
            {
                if (all.Exists(u => u.PermissionId == permissionId)) continue;
                PermissionRoleUserOrganizationUnit permission = new PermissionRoleUserOrganizationUnit()
                {
                    PermissionId = permissionId,
                    OrganizationUnitId = organizationUnitId
                };
                _permissionRoleUserOrganizationUnitRepository.Insert(permission);
            }
            return Task.FromResult(true);
        }

        /// <summary>
        /// 获取该部门的直接权限
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <returns></returns>
        public Task<List<PermissionInfo>> GetPermissions(string organizationUnitId)
        {
            var all = _permissionRoleUserOrganizationUnitRepository.GetAll().Where(u => u.OrganizationUnitId == organizationUnitId).Select(r => r.PermissionId).ToList();
            if (all.Count <= 0) return Task.FromResult(new List<PermissionInfo>());
            var permissionInfos = _permissionInfoRepository.GetAll().Where(u => all.Contains(u.Id)).ToList();
            return Task.FromResult(permissionInfos);
        }
        
        /// <summary>
        /// 获取当前部门下的用户
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <returns></returns>
        public IQueryable<User> GetUsers(string organizationUnitId)
        {

            var all = _organizationUnitUserRepository.GetAll().Where(u => u.OrganizationUnitId == organizationUnitId).Select(r => r.UserId).ToList();
            if (all.Count <= 0) return _userRepository.GetAll().Where(u=>false);
            var users = _userRepository.GetAll().Where(u => all.Contains(u.Id));
            return users;
        }
        /// <summary>
        /// 获取当前部门下以及子部门下所有用户
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <returns></returns>
        public IQueryable<User> GetChildrenUsers(string organizationUnitId)
        {
           var child= _organizationUnitRepository.GetAll().Where(u => u.Path.Contains(organizationUnitId)).Select(u => u.Id)
                .ToList();
            var all = _organizationUnitUserRepository.GetAll().Where(u => u.OrganizationUnitId == organizationUnitId|| child.Contains(u.OrganizationUnitId)).Select(r => r.UserId).ToList();
            if (all.Count <= 0) return _userRepository.GetAll().Where(u => false);
            var users = _userRepository.GetAll().Where(u => all.Contains(u.Id));
            return users;
        }
        /// <summary>
        /// 获取该部门下的所有权限
        /// </summary>
        /// <returns></returns>
        public Task<List<PermissionInfo>> GetAllPermissions(string organizationUnitId)
        {
            var roles = _organizationUnitRoleRepository.GetAll().Where(u => u.OrganizationUnitId == organizationUnitId).Select(u => u.RoleId).ToList();
            var all = _permissionRoleUserOrganizationUnitRepository.GetAll().Where(u => u.OrganizationUnitId == organizationUnitId || roles.Contains(u.RoleId)).Select(r => r.PermissionId).ToList();
            var permissionInfos = _permissionInfoRepository.GetAll().Where(u => all.Contains(u.Id)).ToList();
            return Task.FromResult(permissionInfos);
        }

        public List<IviewTree> GetPermissionTree(string organizationUnitId)
        {
            var seleteids = _permissionRoleUserOrganizationUnitRepository.GetAll().Where(u => u.OrganizationUnitId == organizationUnitId).Select(r => r.PermissionId).ToList();
            var all = _permissionInfoRepository.GetAll().ToList();
            return IviewTree.RecursiveQueries(all, seleteids);
        }
        public List<IviewTree> GetRoleTree(string organizationUnitId)
        {
            var seleteids = _organizationUnitRoleRepository.GetAll().Where(u => u.OrganizationUnitId == organizationUnitId).Select(r => r.RoleId).ToList();
            var all = _roleRepository.GetAll().ToList();
            return IviewTree.LinearQueries(all, seleteids);
        }

        public void Insert(OrganizationUnit entity)
        {
            _organizationUnitRepository.Insert(entity);
        }
        public void Save(OrganizationUnit entity)
        {
            _organizationUnitRepository.Update(entity);
        }
    }
}
