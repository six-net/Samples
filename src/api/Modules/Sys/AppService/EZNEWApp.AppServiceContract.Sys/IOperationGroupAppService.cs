using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter.Filter;

namespace EZNEWApp.AppServiceContract.Sys
{
    /// <summary>
    /// 操作功能分组业务逻辑
    /// </summary>
    public interface IOperationGroupAppService
    {
        #region 保存授权操作组

        /// <summary>
        /// 保存授权操作组
        /// </summary>
        /// <param name="saveOperationGroupParameter">操作分组保存信息</param>
        /// <returns>返回保存执行结果</returns>
        Result<OperationGroup> SaveOperationGroup(SaveOperationGroupParameter saveOperationGroupParameter);

        #endregion

        #region 获取授权操作组

        /// <summary>
        /// 获取授权操作组
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组</returns>
        OperationGroup GetOperationGroup(OperationGroupFilter filter);

        #endregion

        #region 获取授权操作组列表

        /// <summary>
        /// 获取授权操作组列表
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组列表</returns>
        List<OperationGroup> GetOperationGroupList(OperationGroupFilter filter);

        #endregion

        #region 获取授权操作组分页

        /// <summary>
        /// 获取授权操作组分页
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组分页</returns>
        PagingInfo<OperationGroup> GetOperationGroupPaging(OperationGroupFilter filter);

        #endregion

        #region 删除授权操作组

        /// <summary>
        /// 删除授权操作组
        /// </summary>
        /// <param name="removeOperationGroupParameter">操作分组删除信息</param>
        /// <returns>返回执行结果</returns>
        Result RemoveOperationGroup(RemoveOperationGroupParameter removeOperationGroupParameter);

        #endregion

        #region 修改授权操作组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="modifyOperationGroupSortParameter">操作分组排序修改信息</param>
        /// <returns>返回执行结果</returns>
        Result ModifyOperationGroupSort(ModifyOperationGroupSortParameter modifyOperationGroupSortParameter);

        #endregion

        #region 检查操作分组名称是否可用

        /// <summary>
        /// 检查操作分组名称是否可用
        /// </summary>
        /// <param name="existOperationGroupNameParameter">操作分组名称检查信息</param>
        /// <returns>返回操作分组名称是否存在</returns>
        bool ExistOperationGroupName(ExistOperationGroupNameParameter existOperationGroupNameParameter);

        #endregion
    }
}
