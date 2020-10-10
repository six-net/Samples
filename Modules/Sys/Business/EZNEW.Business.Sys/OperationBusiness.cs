using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZNEW.BusinessContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Develop.UnitOfWork;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Parameter;
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
    /// 操作功能业务逻辑
    /// </summary>
    public class OperationBusiness : IOperationBusiness
    {
        readonly IOperationService operationService;
        readonly IPermissionOperationService permissionOperationService;

        public OperationBusiness(IOperationService operationService
            , IPermissionOperationService permissionOperationService)
        {
            this.operationService = operationService;
            this.permissionOperationService = permissionOperationService;
        }

        #region 保存操作功能

        /// <summary>
        /// 保存操作功能
        /// </summary>
        /// <param name="saveOperationDto">操作功能保存信息</param>
        /// <returns>执行结果</returns>
        public Result<OperationDto> SaveOperation(SaveOperationDto saveOperationDto)
        {
            if (saveOperationDto == null)
            {
                return Result<OperationDto>.FailedResult("操作功能信息不完整");
            }
            using (var businessWork = WorkManager.Create())
            {
                var saveResult = operationService.Save(saveOperationDto.Operation.MapTo<Operation>());
                if (!saveResult.Success)
                {
                    return Result<OperationDto>.FailedResult(saveResult.Message);
                }
                var commitResult = businessWork.Commit();
                Result<OperationDto> result = null;
                if (commitResult.EmptyResultOrSuccess)
                {
                    result = Result<OperationDto>.SuccessResult("保存成功");
                    result.Data = saveResult.Data.MapTo<OperationDto>();
                }
                else
                {
                    result = Result<OperationDto>.SuccessResult("保存失败");
                }
                return result;
            }
        }

        #endregion

        #region 获取操作功能

        /// <summary>
        /// 获取操作功能
        /// </summary>
        /// <param name="filter">操作功能筛选信息</param>
        /// <returns>返回操作功能</returns>
        public OperationDto GetOperation(OperationFilterDto filter)
        {
            var authorityOperation = operationService.Get(filter?.ConvertToFilter());
            return authorityOperation.MapTo<OperationDto>();
        }

        #endregion

        #region 获取操作功能列表

        /// <summary>
        /// 获取操作功能列表
        /// </summary>
        /// <param name="filter">操作功能筛选信息</param>
        /// <returns>返回操作功能列表</returns>
        public List<OperationDto> GetOperationList(OperationFilterDto filter)
        {
            var operationList = operationService.GetList(filter?.ConvertToFilter());
            return operationList.Select(c => c.MapTo<OperationDto>()).ToList();
        }

        #endregion

        #region 获取操作功能分页

        /// <summary>
        /// 获取操作功能分页
        /// </summary>
        /// <param name="filter">操作功能筛选信息</param>
        /// <returns>返回操作功能分页</returns>
        public IPaging<OperationDto> GetOperationPaging(OperationFilterDto filter)
        {
            var authorityOperationPaging = operationService.GetPaging(filter?.ConvertToFilter());
            return authorityOperationPaging.ConvertTo<OperationDto>();
        }

        #endregion

        #region 删除操作功能

        /// <summary>
        /// 删除操作功能
        /// </summary>
        /// <param name="removeOperationDto">操作功能删除信息</param>
        /// <returns>返回执行结果</returns>
        public Result RemoveOperation(RemoveOperationDto removeOperationDto)
        {
            #region 参数判断

            if (removeOperationDto?.Ids.IsNullOrEmpty() ?? true)
            {
                return Result.FailedResult("没有指定要删除的操作功能");
            }

            #endregion

            using (var businessWork = WorkManager.Create())
            {
                var removeResult = operationService.Remove(removeOperationDto.Ids);
                if (!removeResult.Success)
                {
                    return removeResult;
                }
                var commitResult = businessWork.Commit();
                return commitResult.ExecutedSuccess ? Result.SuccessResult("删除成功") : Result.FailedResult("删除失败");
            }
        }

        #endregion

        #region 修改操作状态

        /// <summary>
        /// 修改操作功能状态
        /// </summary>
        /// <param name="statusInfo">状态信息</param>
        /// <returns>执行结果</returns>
        public Result ModifyOperationStatus(ModifyOperationStatusDto modifyOperationStatusDto)
        {
            if (modifyOperationStatusDto?.StatusInfos.IsNullOrEmpty() ?? true)
            {
                return Result.FailedResult("没有指定任何要修改的状态信息");
            }
            using (var businessWork = WorkManager.Create())
            {
                var modifyResult = operationService.ModifyStatus(modifyOperationStatusDto.MapTo<ModifyOperationStatusParameter>());
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = businessWork.Commit();
                return commitResult.ExecutedSuccess ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 验证操作功能名是否存在

        /// <summary>
        /// 验证操作功能名是否存在
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="excludeId">排除指定的操作功能</param>
        /// <returns>返回操作功能名称是否存在</returns>
        public bool ExistOperationName(string name, long excludeId)
        {
            return operationService.ExistName(name, excludeId);
        }

        #endregion

        #region 清除操作授权的权限

        /// <summary>
        /// 清除操作授权的权限
        /// </summary>
        /// <param name="operationIds">操作功能编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearOperationPermission(IEnumerable<long> operationIds)
        {
            using (var work = WorkManager.Create())
            {
                var clearResult = permissionOperationService.ClearByOperation(operationIds);
                if (!clearResult.Success)
                {
                    return clearResult;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyResultOrSuccess ? Result.SuccessResult("清除成功") : Result.FailedResult("清除失败");
            }
        }

        #endregion

        #region 授权验证

        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="checkAuthorizationDto">授权验证信息</param>
        /// <returns>返回授权是否验证通过</returns>
        public bool CheckAuthorization(CheckAuthorizationDto checkAuthorizationDto)
        {
            return operationService.CheckAuthorization(checkAuthorizationDto.MapTo<CheckAuthorizationParameter>());
        }

        #endregion

        #region 初始化操作功能

        /// <summary>
        /// 初始化操作功能
        /// </summary>
        /// <param name="initializeOperationDto">操作功能初始化信息</param>
        /// <returns>返回执行结果</returns>
        public Result Initialize(InitializeOperationDto initializeOperationDto)
        {
            using (var work = WorkManager.Create())
            {
                var initResult = operationService.Initialize(initializeOperationDto.MapTo<InitializeOperationParameter>());
                if (!initResult.Success)
                {
                    return initResult;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyResultOrSuccess ? Result.SuccessResult("初始化成功") : Result.FailedResult("初始化失败");
            }
        }

        #endregion
    }
}
