using System.Collections.Generic;
using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Parameter;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Paging;
using EZNEW.Response;

namespace EZNEW.Domain.Sys.Service
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public interface IPermissionService
    {
        #region 修改权限状态

        /// <summary>
        /// 修改权限状态
        /// </summary>
        /// <param name="modifyPermissionStatus">权限状态修改信息</param>
        /// <returns>返回执行结果</returns>
        Result ModifyStatus(ModifyPermissionStatusParameter modifyPermissionStatus);

        #endregion

        #region 删除权限

        /// <summary>
        /// 根据系统编号删除权限
        /// </summary>
        /// <param name="ids">权限编号</param>
        void Remove(IEnumerable<long> ids);

        #endregion

        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="permission">权限对象</param>
        /// <returns>执行结果</returns>
        Result<Permission> Save(Permission permission);

        #endregion

        #region 获取权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="permissionCode">权限码</param>
        /// <returns>返回权限</returns>
        Permission Get(string permissionCode);

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="id">权限编号</param>
        /// <returns>返回权限</returns>
        Permission Get(long id);

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="permissionFilter">权限筛选信息</param>
        /// <returns>返回权限</returns>
        Permission Get(PermissionFilter permissionFilter);

        #endregion

        #region 获取权限列表

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="permissionCodes">权限码</param>
        /// <returns>返回权限列表</returns>
        List<Permission> GetList(IEnumerable<string> permissionCodes);

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="ids">权限编号</param>
        /// <returns>返回权限列表</returns>
        List<Permission> GetList(IEnumerable<long> ids);

        /// <summary>
        /// 返回权限列表
        /// </summary>
        /// <param name="permissionFilter">权限筛选信息</param>
        /// <returns>返回权限列表</returns>
        List<Permission> GetList(PermissionFilter permissionFilter);

        #endregion

        #region 获取权限分页

        /// <summary>
        /// 获取权限分页
        /// </summary>
        /// <param name="permissionFilter">权限筛选信息</param>
        /// <returns>返回权限分页</returns>
        IPaging<Permission> GetPaging(PermissionFilter permissionFilter);

        #endregion

        #region 检查权限编码是否存在

        /// <summary>
        /// 检查权限编码是否存在
        /// </summary>
        /// <param name="code">权限编码</param>
        /// <param name="excludeId">需要排除的权限编号</param>
        /// <returns>返回权限编码是否存在</returns>
        bool ExistCode(string code, long excludeId);

        #endregion

        #region 检查权限名称是否存在

        /// <summary>
        /// 检查权限名称是否存在
        /// </summary>
        /// <param name="name">权限名称</param>
        /// <param name="excludeId">需要排除的权限编号</param>
        /// <returns>返回权限名称是否存在</returns>
        bool ExistName(string name, long excludeId);

        #endregion
    }
}
