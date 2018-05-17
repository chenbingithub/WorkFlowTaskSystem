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
    public class RoleManager:DomainService
    {
        private IRoleRepository _roleRepository;
        private IPermissionRoleUserOrganizationUnitRepository _permissionRoleUserOrganizationUnit;
        private IPermissionInfoRepository _permissionInfoRepository;
        public RoleManager(IPermissionRoleUserOrganizationUnitRepository permissionRoleUserOrganizationUnit, IPermissionInfoRepository permissionInfoRepository, IRoleRepository roleRepository)
        {
            _permissionRoleUserOrganizationUnit = permissionRoleUserOrganizationUnit;
            _permissionInfoRepository = permissionInfoRepository;
            _roleRepository = roleRepository;
        }

        public Task<bool> SetPermission(string roleId, params string[] permissionIds)
        {
            var all = _permissionRoleUserOrganizationUnit.GetAll().Where(u => u.RoleId == roleId).ToList();
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
                    RoleId = roleId
                };
                _permissionRoleUserOrganizationUnit.Insert(permission);
            }
            return Task.FromResult(true);
        }

        public Task<List<PermissionInfo>> GetPermissions(string roleId)
        {
            var all = _permissionRoleUserOrganizationUnit.GetAll().Where(u => u.RoleId == roleId).Select(r => r.PermissionId).ToList();
            if (all.Count <= 0) return Task.FromResult(new List<PermissionInfo>());
            var permissionInfos = _permissionInfoRepository.GetAll().Where(u => all.Contains(u.Id)).ToList();
            return Task.FromResult(permissionInfos);
        }

        public List<IviewTree> GetPermissionTree(string roleId)
        {
            var seleteids = _permissionRoleUserOrganizationUnit.GetAll().Where(u => u.RoleId == roleId).Select(r => r.PermissionId).ToList();
            var all = _permissionInfoRepository.GetAll().ToList();
            return IviewTree.RecursiveQueries(all, seleteids);
        }

    }
}
