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
        private IOrganizationUnitRepository _organizationUnitRepository;

        public UserManager(IUserRepository userRepository, IRoleRepository roleRepository, IPermissionInfoRepository permissionInfoRepository, IPermissionRoleUserOrganizationUnitRepository permissionRoleUserOrganizationUnit, IUserRoleRepository userRoleRepository, IOrganizationUnitUserRepository organizationUnitUserRepository, IOrganizationUnitRoleRepository organizationUnitRoleRepository, IOrganizationUnitRepository organizationUnitRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _permissionInfoRepository = permissionInfoRepository;
            _permissionRoleUserOrganizationUnit = permissionRoleUserOrganizationUnit;
            _userRoleRepository = userRoleRepository;
            _organizationUnitUserRepository = organizationUnitUserRepository;
            _organizationUnitRoleRepository = organizationUnitRoleRepository;
            _organizationUnitRepository = organizationUnitRepository;
        }

        public User FindById(string id)
        {
           return _userRepository.Get(id);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public bool SetRole(string userId,params string[] roleIds)
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
            return true;
        }

        public List<Role> GetRoles(string userId)
        {
            var all = _userRoleRepository.GetAll().Where(u => u.UserId == userId).Select(r=>r.RoleId).ToList();
            if (all.Count <= 0) return new List<Role>();
            var roles=_roleRepository.GetAll().Where(u => all.Contains(u.Id)).ToList();
            return roles;
        }

        public bool SetPermission(string userId, params string[] permissionIds)
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
            return true;
        }
        /// <summary>
        /// 获取该用户的直接权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetPermissions(string userId)
        {
            var all = _permissionRoleUserOrganizationUnit.GetAll().Where(u => u.UserId == userId).Select(r => r.PermissionId).ToList();
            if (all.Count <= 0) return new List<PermissionInfo>();
            var permissionInfos = _permissionInfoRepository.GetAll().Where(u => all.Contains(u.Id)).ToList();
            return permissionInfos;
        }
        /// <summary>
        /// 设置部门
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="organizationUnitIds"></param>
        /// <returns></returns>
        public bool SetOrganizationUnit(string userId, params string[] organizationUnitIds)
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
            return true;
        }

        public List<OrganizationUnit> GetOrganizationUnit(string userId)
        {
            var all = _organizationUnitUserRepository.GetAll().Where(u => u.UserId == userId).Select(r => r.OrganizationUnitId).ToList();
            if (all.Count <= 0) return new List<OrganizationUnit>();
            var organization = _organizationUnitRepository.GetAll().Where(u => all.Contains(u.Id)).ToList();
            return organization;
        }

        public List<IviewTree> GetOrganizationTree(string userId)
        {
            var seleteids = _organizationUnitUserRepository.GetAll().Where(u => u.UserId == userId).Select(r => r.OrganizationUnitId).ToList();
            var all=_organizationUnitRepository.GetAll().ToList();
           return IviewTree.RecursiveQueries(all,seleteids);
        }
        public List<IviewTree> GetPermissionTree(string userId)
        {
            var seleteids = _permissionRoleUserOrganizationUnit.GetAll().Where(u => u.UserId == userId).Select(r => r.PermissionId).ToList();
            var all = _permissionInfoRepository.GetAll().ToList();
            return IviewTree.RecursiveQueries(all, seleteids);
        }
        public List<IviewTree> GetRoleTree(string userId)
        {
            var seleteids = _userRoleRepository.GetAll().Where(u => u.UserId == userId).Select(r => r.RoleId).ToList();
            var all = _roleRepository.GetAll().ToList();
            return IviewTree.LinearQueries(all, seleteids);
        }
        /// <summary>
        /// 获取该用户下的所有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetAllPermissions(string userId)
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
            return permissionInfos;
        }

        public IQueryable<User> GetAll()
        {
           return _userRepository.GetAll();
        }

        public void Save(User entity)
        {
            _userRepository.Update(entity);
        }
    }
}
