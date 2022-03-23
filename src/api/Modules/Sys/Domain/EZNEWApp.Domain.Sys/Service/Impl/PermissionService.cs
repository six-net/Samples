using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Development.Query;
using EZNEWApp.Domain.Sys.Model;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEW.DependencyInjection;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEW.Development.Domain.Repository;

namespace EZNEWApp.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public class PermissionService : IPermissionService
    {
        readonly IRepository<Permission> permissionRepository = ContainerManager.Resolve<IRepository<Permission>>();
        readonly IPermissionGroupService permissionGroupService = ContainerManager.Resolve<IPermissionGroupService>();

        public PermissionService(IRepository<Permission> permissionRepository,
            IPermissionGroupService permissionGroupService)
        {
            this.permissionRepository = permissionRepository;
            this.permissionGroupService = permissionGroupService;
        }

        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="permission">权限对象</param>
        /// <returns>执行结果</returns>
        public Result<Permission> Save(Permission permission)
        {
            return permission?.Save() ?? Result<Permission>.FailedResult("权限保存失败");
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
            IQuery removeQuery = QueryManager.Create<Permission>(a => ids.Contains(a.Id));
            permissionRepository.Remove(removeQuery);
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
            IQuery permissionQuery = QueryManager.Create<Permission>(a => a.Code == permissionCode);
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
            IQuery query = QueryManager.Create<Permission>(a => a.Id == id);
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
            IQuery query = QueryManager.Create<Permission>(c => permissionCodes.Contains(c.Code));
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
            IQuery query = QueryManager.Create<Permission>(c => ids.Contains(c.Id));
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
        PagingInfo<Permission> GetPaging(IQuery query)
        {
            var permissionPaging = permissionRepository.GetPaging(query);
            var permissionList = LoadOtherObjectData(permissionPaging.Items, query);
            return Pager.Create<Permission>(permissionPaging.Page, permissionPaging.PageSize, permissionPaging.TotalCount, permissionList);
        }

        /// <summary>
        /// 返回权限分页
        /// </summary>
        /// <param name="permissionFilter">权限筛选信息</param>
        /// <returns>返回权限分页</returns>
        public PagingInfo<Permission> GetPaging(PermissionFilter permissionFilter)
        {
            return GetPaging(permissionFilter?.CreateQuery());
        }

        #endregion

        #region 修改权限状态

        /// <summary>
        /// 修改权限状态
        /// </summary>
        /// <param name="modifyPermissionStatus">权限状态修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyStatus(ModifyPermissionStatusParameter modifyPermissionStatus)
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
            IQuery query = QueryManager.Create<Permission>(c => c.Code == code && c.Id != excludeId);
            return permissionRepository.Exists(query);
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
            IQuery query = QueryManager.Create<Permission>(c => c.Name == name && c.Id != excludeId);
            return permissionRepository.Exists(query);
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
                var groupIds = authoritys.Select(c => c.Group).Distinct().ToList();
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
                    //auth.SetGroup(groupList.FirstOrDefault(c => c.Id == auth.Group?.Id));
                }
            }

            return authoritys.ToList();
        }

        #endregion
    }
}
