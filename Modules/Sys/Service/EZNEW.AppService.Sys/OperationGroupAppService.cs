using System.Collections.Generic;
using EZNEW.AppServiceContract.Sys;
using EZNEW.BusinessContract.Sys;
using EZNEW.DTO.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Paging;
using EZNEW.Response;

namespace EZNEW.AppService.Sys
{
    /// <summary>
    /// 操作分组应用服务
    /// </summary>
    public class OperationGroupAppService : IOperationGroupAppService
    {
        /// <summary>
        /// 操作分组业务
        /// </summary>
        readonly IOperationGroupBusiness operationGroupBusiness;

        public OperationGroupAppService(IOperationGroupBusiness operationGroupBusiness)
        {
            this.operationGroupBusiness = operationGroupBusiness;
        }

        #region 保存授权操作组

        /// <summary>
        /// 保存授权操作组
        /// </summary>
        /// <param name="saveOperationGroupDto">操作分组保存信息</param>
        /// <returns>返回保存执行结果</returns>
        public Result<OperationGroupDto> SaveOperationGroup(SaveOperationGroupDto saveOperationGroupDto)
        {
            return operationGroupBusiness.SaveOperationGroup(saveOperationGroupDto);
        }

        #endregion

        #region 获取授权操作组

        /// <summary>
        /// 获取授权操作组
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组</returns>
        public OperationGroupDto GetOperationGroup(OperationGroupFilterDto filter)
        {
            return operationGroupBusiness.GetOperationGroup(filter);
        }

        #endregion

        #region 获取授权操作组列表

        /// <summary>
        /// 获取授权操作组列表
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组列表</returns>
        public List<OperationGroupDto> GetOperationGroupList(OperationGroupFilterDto filter)
        {
            return operationGroupBusiness.GetOperationGroupList(filter);
        }

        #endregion

        #region 获取授权操作组分页

        /// <summary>
        /// 获取授权操作组分页
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组分页</returns>
        public IPaging<OperationGroupDto> GetOperationGroupPaging(OperationGroupFilterDto filter)
        {
            return operationGroupBusiness.GetOperationGroupPaging(filter);
        }

        #endregion

        #region 删除授权操作组

        /// <summary>
        /// 删除授权操作组
        /// </summary>
        /// <param name="removeOperationGroupDto">操作分组删除信息</param>
        /// <returns>返回执行结果</returns>
        public Result RemoveOperationGroup(RemoveOperationGroupDto removeOperationGroupDto)
        {
            return operationGroupBusiness.RemoveOperationGroup(removeOperationGroupDto);
        }

        #endregion

        #region 修改授权操作组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="modifyOperationGroupSortDto">操作分组排序修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyOperationGroupSort(ModifyOperationGroupSortDto modifyOperationGroupSortDto)
        {
            return operationGroupBusiness.ModifyOperationGroupSort(modifyOperationGroupSortDto);
        }

        #endregion

        #region 检查操作分组名称是否可用

        /// <summary>
        /// 检查操作分组名称是否可用
        /// </summary>
        /// <param name="existOperationGroupNameDto">操作分组名称检查信息</param>
        /// <returns>返回操作分组名称是否存在</returns>
        public bool ExistOperationGroupName(ExistOperationGroupNameDto existOperationGroupNameDto)
        {
            return operationGroupBusiness.ExistOperationGroupName(existOperationGroupNameDto);
        }

        #endregion
    }
}
