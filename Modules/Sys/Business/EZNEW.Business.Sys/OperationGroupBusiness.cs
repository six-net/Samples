using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZNEW.BusinessContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Develop.UnitOfWork;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Domain.Sys.Service;
using EZNEW.DTO.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Paging;
using EZNEW.Response;

namespace EZNEW.Business.Sys
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
        public Result<OperationGroupDto> SaveOperationGroup(SaveOperationGroupDto saveOperationGroupDto)
        {
            if (saveOperationGroupDto?.OperationGroup == null)
            {
                return Result<OperationGroupDto>.FailedResult("没有指定要保存的操作分组信息");
            }
            using (var businessWork = WorkManager.Create())
            {
                var saveResult = operationGroupService.Save(saveOperationGroupDto.OperationGroup.MapTo<OperationGroup>());
                if (!saveResult.Success)
                {
                    return Result<OperationGroupDto>.FailedResult(saveResult.Message);
                }

                var commitResult = businessWork.Commit();
                Result<OperationGroupDto> result = null;
                if (commitResult.EmptyResultOrSuccess)
                {
                    result = Result<OperationGroupDto>.SuccessResult("保存成功");
                    result.Data = saveResult.Data.MapTo<OperationGroupDto>();
                }
                else
                {
                    result = Result<OperationGroupDto>.FailedResult("保存失败");
                }
                return result;
            }
        }

        #endregion

        #region 获取操作分组

        /// <summary>
        /// 获取操作分组
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组</returns>
        public OperationGroupDto GetOperationGroup(OperationGroupFilterDto filter)
        {
            var operationGroup = operationGroupService.Get(filter?.ConvertToFilter());
            return operationGroup.MapTo<OperationGroupDto>();
        }

        #endregion

        #region 获取操作分组列表

        /// <summary>
        /// 获取操作分组列表
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组</returns>
        public List<OperationGroupDto> GetOperationGroupList(OperationGroupFilterDto filter)
        {
            var operationGroupList = operationGroupService.GetList(filter?.ConvertToFilter());
            return operationGroupList.Select(c => c.MapTo<OperationGroupDto>()).ToList();
        }

        #endregion

        #region 获取操作分组分页

        /// <summary>
        /// 获取操作分组分页
        /// </summary>
        /// <param name="filter">操作分组筛选信息</param>
        /// <returns>返回操作分组</returns>
        public IPaging<OperationGroupDto> GetOperationGroupPaging(OperationGroupFilterDto filter)
        {
            var operationGroupPaging = operationGroupService.GetPaging(filter?.ConvertToFilter());
            return operationGroupPaging.ConvertTo<OperationGroupDto>();
        }

        #endregion

        #region 删除操作分组

        /// <summary>
        /// 删除操作分组
        /// </summary>
        /// <param name="removeOperationGroupDto">操作分组删除信息</param>
        /// <returns>返回执行结果</returns>
        public Result RemoveOperationGroup(RemoveOperationGroupDto removeOperationGroupDto)
        {
            #region 参数判断

            if (removeOperationGroupDto?.Ids.IsNullOrEmpty() ?? true)
            {
                return Result.FailedResult("没有指定要删除的操作分组");
            }

            #endregion

            using (var businessWork = WorkManager.Create())
            {
                var deleteResult = operationGroupService.Remove(removeOperationGroupDto.Ids);
                if (!deleteResult.Success)
                {
                    return deleteResult;
                }
                var exectVal = businessWork.Commit();
                return exectVal.ExecutedSuccess ? Result.SuccessResult("删除成功") : Result.FailedResult("删除失败");
            }
        }

        #endregion

        #region 修改操作分组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="modifyOperationGroupSortDto">操作分组排序修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyOperationGroupSort(ModifyOperationGroupSortDto modifyOperationGroupSortDto)
        {
            if (modifyOperationGroupSortDto == null)
            {
                return Result.FailedResult("没有指定任何要修改的信息");
            }
            using (var businessWork = WorkManager.Create())
            {
                var modifyResult = operationGroupService.ModifySort(modifyOperationGroupSortDto.Id, modifyOperationGroupSortDto.NewSort);
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = businessWork.Commit();
                return commitResult.ExecutedSuccess ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 检查操作分组名称是否可用

        /// <summary>
        /// 检查操作分组名称是否可用
        /// </summary>
        /// <param name="nameInfo">操作分组名称检查信息</param>
        /// <returns>返回操作名称分组是否存在</returns>
        public bool ExistOperationGroupName(ExistOperationGroupNameDto existOperationGroupNameDto)
        {
            if (existOperationGroupNameDto == null)
            {
                return false;
            }
            return operationGroupService.ExistName(existOperationGroupNameDto.Name, existOperationGroupNameDto.ExcludeId);
        }

        #endregion
    }
}
