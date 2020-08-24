using EZNEW.DTO.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Paging;
using EZNEW.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.BusinessContract.Sys
{
    /// <summary>
    /// 操作功能分组业务逻辑
    /// </summary>
    public interface IOperationGroupBusiness
    {
        #region 保存授权操作组

        /// <summary>
        /// 保存授权操作组
        /// </summary>
        /// <param name="saveOperationGroupDto">操作分组保存信息</param>
        /// <returns>返回保存执行结果</returns>
        Result<OperationGroupDto> SaveOperationGroup(SaveOperationGroupDto saveOperationGroupDto);

        #endregion

        #region 获取授权操作组

        /// <summary>
        /// 获取授权操作组
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组</returns>
        OperationGroupDto GetOperationGroup(OperationGroupFilterDto filter);

        #endregion

        #region 获取授权操作组列表

        /// <summary>
        /// 获取授权操作组列表
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组列表</returns>
        List<OperationGroupDto> GetOperationGroupList(OperationGroupFilterDto filter);

        #endregion

        #region 获取授权操作组分页

        /// <summary>
        /// 获取授权操作组分页
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组分页</returns>
        IPaging<OperationGroupDto> GetOperationGroupPaging(OperationGroupFilterDto filter);

        #endregion

        #region 删除授权操作组

        /// <summary>
        /// 删除授权操作组
        /// </summary>
        /// <param name="removeOperationGroupDto">操作分组删除信息</param>
        /// <returns>返回执行结果</returns>
        Result RemoveOperationGroup(RemoveOperationGroupDto removeOperationGroupDto);

        #endregion

        #region 修改授权操作组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="modifyOperationGroupSortDto">操作分组排序修改信息</param>
        /// <returns>返回执行结果</returns>
        Result ModifyOperationGroupSort(ModifyOperationGroupSortDto modifyOperationGroupSortDto);

        #endregion

        #region 检查操作分组名称是否可用

        /// <summary>
        /// 检查操作分组名称是否可用
        /// </summary>
        /// <param name="existOperationGroupNameDto">操作分组名称检查信息</param>
        /// <returns>返回操作分组名称是否存在</returns>
        bool ExistOperationGroupName(ExistOperationGroupNameDto existOperationGroupNameDto);

        #endregion
    }
}
