using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Framework.Paging;
using EZNEW.Framework.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Domain.Sys.Service
{
    /// <summary>
    /// 权限分组服务
    /// </summary>
    public interface IAuthorityGroupService
    {
        #region 批量删除权限分组

        /// <summary>
        /// 批量删除权限分组
        /// </summary>
        /// <param name="groupIds">分组编号</param>
        /// <returns></returns>
        Result RemoveAuthorityGroup(IEnumerable<long> groupIds);

        #endregion

        #region 验证权限分组是否存在

        /// <summary>
        /// 验证权限分组是否存在
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <returns></returns>
        bool ExistAuthorityGroup(long groupId);

        #endregion

        #region 保存权限分组

        /// <summary>
        /// 保存权限分组
        /// </summary>
        /// <param name="authorityGroup">权限分组对象</param>
        /// <returns>执行结果</returns>
        Result<AuthorityGroup> SaveAuthorityGroup(AuthorityGroup authorityGroup);

        #endregion

        #region 获取权限分组

        /// <summary>
        /// 获取权限分组
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        AuthorityGroup GetAuthorityGroup(IQuery query);

        /// <summary>
        /// 获取权限分组
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <returns></returns>
        AuthorityGroup GetAuthorityGroup(long groupId);

        #endregion

        #region 获取权限分组列表

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        List<AuthorityGroup> GetAuthorityGroupList(IQuery query);

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="groupIds">分组编号</param>
        /// <returns></returns>
        List<AuthorityGroup> GetAuthorityGroupList(IEnumerable<long> groupIds);

        #endregion

        #region 获取权限分组分页

        /// <summary>
        /// 获取权限分组分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        IPaging<AuthorityGroup> GetAuthorityGroupPaging(IQuery query);

        #endregion

        #region 修改分组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="sortIndexInfo">排序修改信息</param>
        /// <returns></returns>
        Result ModifyAuthorityGroupSort(long groupId, int newSort);

        #endregion

        #region 验证权限分组名是否存在

        /// <summary>
        /// 验证权限分组名是否存在
        /// </summary>
        /// <param name="groupName">分组名称</param>
        /// <param name="excludeGroupId">排除验证的分组编号</param>
        /// <returns></returns>
        bool ExistGroupName(string groupName, long excludeGroupId);

        #endregion
    }
}
