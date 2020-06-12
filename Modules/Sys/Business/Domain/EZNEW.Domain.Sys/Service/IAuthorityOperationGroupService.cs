using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Paging;
using EZNEW.Response;
using System.Collections.Generic;

namespace EZNEW.Domain.Sys.Service
{
    /// <summary>
    /// 删除操作分组服务
    /// </summary>
    public interface IAuthorityOperationGroupService
    {
        #region 删除操作分组

        /// <summary>
        /// 删除操作分组
        /// </summary>
        /// <param name="deleteInfo">删除信息</param>
        Result DeleteAuthorityOperationGroup(IEnumerable<long> groupIds);

        #endregion

        #region 保存授权操作组

        /// <summary>
        /// 保存授权操作组
        /// </summary>
        /// <param name="authorityOperationGroup">授权操作组对象</param>
        /// <returns>执行结果</returns>
        Result<AuthorityOperationGroup> SaveAuthorityOperationGroup(AuthorityOperationGroup authorityOperationGroup);

        #endregion

        #region 获取授权操作组

        /// <summary>
        /// 获取授权操作组
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        AuthorityOperationGroup GetAuthorityOperationGroup(IQuery query);

        /// <summary>
        /// 获取授权操作分组
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <returns></returns>
        AuthorityOperationGroup GetAuthorityOperationGroup(long groupId);

        #endregion

        #region 获取授权操作组列表

        /// <summary>
        /// 获取授权操作组列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        List<AuthorityOperationGroup> GetAuthorityOperationGroupList(IQuery query);

        /// <summary>
        /// 获取授权操作组列表
        /// </summary>
        /// <param name="groupIds">分组编号</param>
        /// <returns></returns>
        List<AuthorityOperationGroup> GetAuthorityOperationGroupList(IEnumerable<long> groupIds);

        #endregion

        #region 获取授权操作组分页

        /// <summary>
        /// 获取授权操作组分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        IPaging<AuthorityOperationGroup> GetAuthorityOperationGroupPaging(IQuery query);

        #endregion

        #region 修改授权操作组排序

        /// <summary>
        /// 修改授权操作分组排序
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <param name="newSort">新的排序</param>
        /// <returns></returns>
        Result ModifySort(long groupId, int newSort);

        #endregion

        #region 检查操作分组名称是否可用

        /// <summary>
        /// 检查操作分组名称是否可用
        /// </summary>
        /// <param name="groupName">分组名称</param>
        /// <param name="excludeId">排除验证的分组编号</param>
        /// <returns></returns>
        bool ExistGroupName(string groupName, long excludeId);

        #endregion
    }
}
