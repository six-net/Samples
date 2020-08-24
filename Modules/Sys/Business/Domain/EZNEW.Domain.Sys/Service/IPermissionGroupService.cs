using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Paging;
using EZNEW.Response;

namespace EZNEW.Domain.Sys.Service
{
    /// <summary>
    /// 权限分组服务
    /// </summary>
    public interface IPermissionGroupService
    {
        #region 删除权限分组

        /// <summary>
        /// 删除权限分组
        /// </summary>
        /// <param name="groupIds">分组编号</param>
        /// <returns>返回删除执行结果</returns>
        Result Remove(IEnumerable<long> groupIds);

        #endregion

        #region 验证权限分组是否存在

        /// <summary>
        /// 验证权限分组是否存在
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <returns>返回权限分组是否存在</returns>
        bool Exist(long groupId);

        #endregion

        #region 保存权限分组

        /// <summary>
        /// 保存权限分组
        /// </summary>
        /// <param name="authorityGroup">权限分组对象</param>
        /// <returns>返回执行结果</returns>
        Result<PermissionGroup> Save(PermissionGroup authorityGroup);

        #endregion

        #region 获取权限分组

        /// <summary>
        /// 获取权限分组
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <returns>返回权限分组</returns>
        PermissionGroup Get(long groupId);

        /// <summary>
        /// 获取权限分组
        /// </summary>
        /// <param name="permissionGroupFilter">权限分组筛选信息</param>
        /// <returns>返回权限分组</returns>
        PermissionGroup Get(PermissionGroupFilter permissionGroupFilter);

        #endregion

        #region 获取权限分组列表

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="groupIds">分组编号</param>
        /// <returns>返回权限分组列表</returns>
        List<PermissionGroup> GetList(IEnumerable<long> groupIds);

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="permissionGroupFilter">权限分组筛选信息</param>
        /// <returns>返回权限分组列表</returns>
        List<PermissionGroup> GetList(PermissionGroupFilter permissionGroupFilter);

        #endregion

        #region 获取权限分组分页

        /// <summary>
        /// 获取权限分组分页
        /// </summary>
        /// <param name="permissionGroupFilter">权限分组筛选信息</param>
        /// <returns>返回权限分组分页</returns>
        IPaging<PermissionGroup> GetPaging(PermissionGroupFilter permissionGroupFilter);

        #endregion

        #region 修改分组排序

        /// <summary>
        /// 修改权限分组排序
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <param name="newSort">新的排序</param>
        /// <returns>返回排序修改结果</returns>
        Result ModifySort(long groupId, int newSort);

        #endregion

        #region 验证权限分组名是否存在

        /// <summary>
        /// 验证权限分组名是否存在
        /// </summary>
        /// <param name="name">分组名称</param>
        /// <param name="excludeId">验证时需要排除的分组编号</param>
        /// <returns>返回权限分组名称是否存在</returns>
        bool ExistName(string name, long excludeId);

        #endregion
    }
}
