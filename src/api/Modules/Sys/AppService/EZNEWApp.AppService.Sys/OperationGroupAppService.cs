using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZNEWApp.AppServiceContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Development.UnitOfWork;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Service;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEWApp.BusinessContract.Sys;

namespace EZNEWApp.AppService.Sys
{
    /// <summary>
    /// 操作分组业务逻辑
    /// </summary>
    public class OperationGroupAppService : IOperationGroupAppService
    {
        IOperationGroupBusiness operationGroupBusiness;

        public OperationGroupAppService(IOperationGroupBusiness operationGroupBusiness)
        {
            this.operationGroupBusiness = operationGroupBusiness;
        }

        #region 保存操作分组

        /// <summary>
        /// 保存操作分组
        /// </summary>
        /// <param name="saveInfo">操作分组保存信息</param>
        /// <returns>返回执行结果</returns>
        public Result<OperationGroup> SaveOperationGroup(SaveOperationGroupParameter saveOperationGroupParameter)
        {
            return operationGroupBusiness.SaveOperationGroup(saveOperationGroupParameter);
        }

        #endregion

        #region 获取操作分组

        /// <summary>
        /// 获取操作分组
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组</returns>
        public OperationGroup GetOperationGroup(OperationGroupFilter filter)
        {
            return operationGroupBusiness.GetOperationGroup(filter);
        }

        #endregion

        #region 获取操作分组列表

        /// <summary>
        /// 获取操作分组列表
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组</returns>
        public List<OperationGroup> GetOperationGroupList(OperationGroupFilter filter)
        {
            return operationGroupBusiness.GetOperationGroupList(filter);
        }

        #endregion

        #region 获取操作分组分页

        /// <summary>
        /// 获取操作分组分页
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组</returns>
        public PagingInfo<OperationGroup> GetOperationGroupPaging(OperationGroupFilter filter)
        {
            return operationGroupBusiness.GetOperationGroupPaging(filter);
        }

        #endregion

        #region 删除操作分组

        /// <summary>
        /// 删除操作分组
        /// </summary>
        /// <param name="removeOperationGroupParameter">操作分组删除信息</param>
        /// <returns>返回执行结果</returns>
        public Result RemoveOperationGroup(RemoveOperationGroupParameter removeOperationGroupParameter)
        {
            return operationGroupBusiness.RemoveOperationGroup(removeOperationGroupParameter);
        }

        #endregion

        #region 修改操作分组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="modifyOperationGroupSortParameter">操作分组排序修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyOperationGroupSort(ModifyOperationGroupSortParameter modifyOperationGroupSortParameter)
        {
            return operationGroupBusiness.ModifyOperationGroupSort(modifyOperationGroupSortParameter);
        }

        #endregion

        #region 检查操作分组名称是否可用

        /// <summary>
        /// 检查操作分组名称是否可用
        /// </summary>
        /// <param name="nameInfo">操作分组名称检查信息</param>
        /// <returns>返回操作名称分组是否存在</returns>
        public bool ExistOperationGroupName(ExistOperationGroupNameParameter existOperationGroupNameParameter)
        {
            return operationGroupBusiness.ExistOperationGroupName(existOperationGroupNameParameter);
        }

        #endregion
    }
}
