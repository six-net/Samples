using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Paging;
using EZNEW.Response;
using EZNEW.Domain.Sys.Repository;
using EZNEW.DependencyInjection;
using EZNEW.Entity.Sys;
using EZNEW.Domain.Sys.Parameter;
using EZNEW.Module.Sys;
using EZNEW.Domain.Sys.Parameter.Filter;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public class PermissionService : IPermissionService
    {
        static readonly IPermissionRepository permissionRepository = ContainerManager.Resolve<IPermissionRepository>();
        static readonly IPermissionGroupService permissionGroupService = ContainerManager.Resolve<IPermissionGroupService>();

        #region 修改权限状态

        /// <summary>
        /// 修改权限状态
        /// </summary>
        /// <param name="modifyPermissionStatus">权限状态修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyStatus(ModifyPermissionStatus modifyPermissionStatus)
        {
            #region 参数判断

            if (modifyPermissionStatus?.StatusInfos.IsNullOrEmpty() ?? true)
            {
                return Result.FailedResult("没有指定要操作的权限信息");
            }

            #endregion

            IEnumerable<long> permissionIds = modifyPermissionStatus.StatusInfos.Keys;
            var permissionList = GetList(permissionIds);
            if (permissionList.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定要操作的权限信息");
            }
            foreach (var permission in permissionList)
            {
                if (permission == null || !modifyPermissionStatus.StatusInfos.TryGetValue(permission.Id, out var newStatus))
                {
                    continue;
                }
                permission.Status = newStatus;
                permission.Save();
            }
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 删除权限

        /// <summary>
        /// 根据系统编号删除权限数据
        /// </summary>
        /// <param name="ids">权限系统编号</param>
        /// <returns>返回执行结果</returns>
        public void Remove(IEnumerable<long> ids)
        {
            if (ids.IsNullOrEmpty())
            {
                throw new Exception("没有指定任何要删除的权限");
            }
            IQuery delQuery = QueryManager.Create<PermissionEntity>(a => ids.Contains(a.Id));
            permissionRepository.Remove(delQuery);
        }

        #endregion

        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="permission">权限对象</param>
        /// <returns>执行结果</returns>
        public Result<Permission> Save(Permission permission)
        {
            if (permission == null)
            {
                return Result<Permission>.FailedResult("权限信息为空");
            }
            //权限分组
            if (permission.Group == null || permission.Group.Id < 1)
            {
                return Result<Permission>.FailedResult("请设置正确的权限组");
            }
            if (!permissionGroupService.Exist(permission.Group.Id))
            {
                return Result<Permission>.FailedResult("请设置正确的权限组");
            }
            Permission nowPermission = null;
            if (permission.Id > 0)
            {
                nowPermission = Get(permission.Id); ;
            }
            if (nowPermission == null)
            {
                nowPermission = permission;
                nowPermission.Type = PermissionType.Management;
                nowPermission.CreateDate = DateTime.Now;
                nowPermission.Sort = 0;
            }
            else
            {
                nowPermission.Code = permission.Code;
                nowPermission.Name = permission.Name;
                nowPermission.Status = permission.Status;
                nowPermission.Remark = permission.Remark;
            }
            nowPermission.Save();
            var result = Result<Permission>.SuccessResult("保存成功");
            result.Data = nowPermission;
            return result;
        }

        #endregion

        #region 获取权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns>返回权限</returns>
        Permission Get(IQuery query)
        {
            var permission = permissionRepository.Get(query);
            return permission;
        }

        /// <summary>
        /// 根据权限编号获取权限
        /// </summary>
        /// <param name="permissionCode">权限编码</param>
        /// <returns>返回权限</returns>
        public Permission Get(string permissionCode)
        {
            if (string.IsNullOrWhiteSpace(permissionCode))
            {
                return null;
            }
            IQuery permissionQuery = QueryManager.Create<PermissionEntity>(a => a.Code == permissionCode);
            return Get(permissionQuery);
        }

        /// <summary>
        /// 根据系统编号获取权限
        /// </summary>
        /// <param name="id">权限系统编号</param>
        /// <returns>权限对象</returns>
        public Permission Get(long id)
        {
            if (id < 1)
            {
                return null;
            }
            IQuery query = QueryManager.Create<PermissionEntity>(a => a.Id == id);
            return Get(query);
        }

        /// <summary>
        /// 获取权限对象
        /// </summary>
        /// <param name="permissionFilter">权限对象筛选信息</param>
        /// <returns></returns>
        public Permission Get(PermissionFilter permissionFilter)
        {
            return Get(permissionFilter?.CreateQuery());
        }

        #endregion

        #region 获取权限列表

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns>返回权限列表</returns>
        List<Permission> GetList(IQuery query)
        {
            var permissionList = permissionRepository.GetList(query);
            permissionList = LoadOtherObjectData(permissionList, query);
            return permissionList;
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="permissionCodes">权限码</param>
        /// <returns>返回权限列表</returns>
        public List<Permission> GetList(IEnumerable<string> permissionCodes)
        {
            if (permissionCodes.IsNullOrEmpty())
            {
                return new List<Permission>(0);
            }
            IQuery query = QueryManager.Create<PermissionEntity>(c => permissionCodes.Contains(c.Code));
            return GetList(query);
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="ids">权限编号</param>
        /// <returns>返回权限列表</returns>
        public List<Permission> GetList(IEnumerable<long> ids)
        {
            if (ids.IsNullOrEmpty())
            {
                return new List<Permission>(0);
            }
            IQuery query = QueryManager.Create<PermissionEntity>(c => ids.Contains(c.Id));
            return GetList(query);
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="permissionFilter">权限筛选信息</param>
        /// <returns>返回权限列表</returns>
        public List<Permission> GetList(PermissionFilter permissionFilter)
        {
            return GetList(permissionFilter?.CreateQuery());
        }

        #endregion

        #region 获取权限分页

        /// <summary>
        /// 获取权限分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns>返回权限分页</returns>
        IPaging<Permission> GetPaging(IQuery query)
        {
            var permissionPaging = permissionRepository.GetPaging(query);
            var permissionList = LoadOtherObjectData(permissionPaging, query);
            return new DefaultPaging<Permission>(permissionPaging.Page, permissionPaging.PageSize, permissionPaging.TotalCount, permissionList);
        }

        /// <summary>
        /// 返回权限分页
        /// </summary>
        /// <param name="permissionFilter">权限筛选信息</param>
        /// <returns>返回权限分页</returns>
        public IPaging<Permission> GetPaging(PermissionFilter permissionFilter)
        {
            return GetPaging(permissionFilter?.CreateQuery());
        }

        #endregion

        #region 检查权限编码是否存在

        /// <summary>
        /// 检查权限编码是否存在
        /// </summary>
        /// <param name="code">权限编码</param>
        /// <param name="excludeId">需要排除的权限系统编号</param>
        /// <returns>返回权限编码是否存在</returns>
        public bool ExistCode(string code, long excludeId)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return false;
            }
            IQuery query = QueryManager.Create<PermissionEntity>(c => c.Code == code && c.Id != excludeId);
            return permissionRepository.Exist(query);
        }

        #endregion

        #region 检查权限名称是否存在

        /// <summary>
        /// 检查权限名称是否存在
        /// </summary>
        /// <param name="name">权限名称</param
        /// <param name="excludeId">需要排除的权限系统编号</param>
        /// <returns>返回权限名称是否存在</returns>
        public bool ExistName(string name, long excludeId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            IQuery query = QueryManager.Create<PermissionEntity>(c => c.Name == name && c.Id != excludeId);
            return permissionRepository.Exist(query);
        }

        #endregion

        #region 加载其它数据

        /// <summary>
        /// 加载其它数据
        /// </summary>
        /// <param name="authoritys">权限数据</param>
        /// <param name="query">筛选条件</param>
        /// <returns>返回权限列表</returns>
        List<Permission> LoadOtherObjectData(IEnumerable<Permission> authoritys, IQuery query)
        {
            if (authoritys.IsNullOrEmpty())
            {
                return new List<Permission>(0);
            }
            if (query == null)
            {
                return authoritys.ToList();
            }

            #region 权限分组

            List<PermissionGroup> groupList = null;
            if (query.AllowLoad<Permission>(c => c.Group))
            {
                var groupIds = authoritys.Select(c => c.Group?.Id ?? 0).Distinct().ToList();
                groupList = permissionGroupService.GetList(groupIds);
            }

            #endregion

            foreach (var auth in authoritys)
            {
                if (auth == null)
                {
                    continue;
                }
                if (!groupList.IsNullOrEmpty())
                {
                    auth.SetGroup(groupList.FirstOrDefault(c => c.Id == auth.Group?.Id));
                }
            }

            return authoritys.ToList();
        }

        #endregion
    }
}
