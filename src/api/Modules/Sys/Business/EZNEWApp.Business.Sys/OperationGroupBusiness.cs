using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZNEWApp.BusinessContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Development.UnitOfWork;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Service;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Parameter.Filter;

namespace EZNEWApp.Business.Sys
{
    /// <summary>
    /// 操作分组业务逻辑
    /// </summary>
    public class OperationGroupBusiness : IOperationGroupBusiness
    {
        static readonly IOperationGroupService operationGroupService = ContainerManager.Resolve<IOperationGroupService>();

        #region 保存操作分组

        /// <summary>
        /// 保存操作分组
        /// </summary>
        /// <param name="saveInfo">操作分组保存信息</param>
        /// <returns>返回执行结果</returns>
        public Result<OperationGroup> SaveOperationGroup(SaveOperationGroupParameter saveOperationGroupParameter)
        {
            if (saveOperationGroupParameter is null)
            {
                throw new ArgumentNullException(nameof(saveOperationGroupParameter));
            }

            using (var work = WorkManager.Create())
            {
                var saveResult = operationGroupService.Save(saveOperationGroupParameter.OperationGroup);
                if (!saveResult.Success)
                {
                    return Result<OperationGroup>.FailedResult(saveResult.Message);
                }

                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? saveResult : Result<OperationGroup>.FailedResult("保存失败");
            }
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
            return operationGroupService.Get(filter);
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
            return operationGroupService.GetList(filter);
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
            return operationGroupService.GetPaging(filter);
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
            if (removeOperationGroupParameter is null)
            {
                throw new ArgumentNullException(nameof(removeOperationGroupParameter));
            }

            using (var work = WorkManager.Create())
            {
                var removeResult = operationGroupService.Remove(removeOperationGroupParameter.Ids);
                if (!removeResult.Success)
                {
                    return removeResult;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? Result.SuccessResult("删除成功") : Result.FailedResult("删除失败");
            }
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
            if (modifyOperationGroupSortParameter is null)
            {
                throw new ArgumentNullException(nameof(modifyOperationGroupSortParameter));
            }

            using (var work = WorkManager.Create())
            {
                var modifyResult = operationGroupService.ModifySort(modifyOperationGroupSortParameter.Id, modifyOperationGroupSortParameter.NewSort);
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = work.Commit();
                return commitResult.Success ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
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
            if (existOperationGroupNameParameter == null)
            {
                return false;
            }
            return operationGroupService.ExistName(existOperationGroupNameParameter.Name, existOperationGroupNameParameter.ExcludeId);
        }

        #endregion
    }
}
