using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZNEWApp.BusinessContract.Sys;
using EZNEW.Development.UnitOfWork;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Service;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter.Filter;

namespace EZNEWApp.Business.Sys
{
    /// <summary>
    /// 操作功能业务逻辑
    /// </summary>
    public class OperationBusiness : IOperationBusiness
    {
        readonly IOperationService operationService;
        readonly IOperationPermissionService operationPermissionService;

        public OperationBusiness(IOperationService operationService
            , IOperationPermissionService permissionOperationService)
        {
            this.operationService = operationService;
            this.operationPermissionService = permissionOperationService;
        }

        #region 保存操作功能

        /// <summary>
        /// 保存操作功能
        /// </summary>
        /// <param name="saveOperationParameter">操作功能保存信息</param>
        /// <returns>执行结果</returns>
        public Result<Operation> SaveOperation(SaveOperationParameter saveOperationParameter)
        {
            if (saveOperationParameter is null)
            {
                throw new ArgumentNullException(nameof(saveOperationParameter));
            }

            using (var work = WorkManager.Create())
            {
                var saveResult = operationService.Save(saveOperationParameter.Operation);
                if (!saveResult.Success)
                {
                    return saveResult;
                }

                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? saveResult : Result<Operation>.SuccessResult("保存失败");
            }
        }

        #endregion

        #region 获取操作功能

        /// <summary>
        /// 获取操作功能
        /// </summary>
        /// <param name="filter">操作功能筛选信息</param>
        /// <returns>返回操作功能</returns>
        public Operation GetOperation(OperationFilter filter)
        {
            return operationService.Get(filter);
        }

        #endregion

        #region 获取操作功能列表

        /// <summary>
        /// 获取操作功能列表
        /// </summary>
        /// <param name="filter">操作功能筛选信息</param>
        /// <returns>返回操作功能列表</returns>
        public List<Operation> GetOperationList(OperationFilter filter)
        {
            return operationService.GetList(filter);
        }

        #endregion

        #region 获取操作功能分页

        /// <summary>
        /// 获取操作功能分页
        /// </summary>
        /// <param name="filter">操作功能筛选信息</param>
        /// <returns>返回操作功能分页</returns>
        public PagingInfo<Operation> GetOperationPaging(OperationFilter filter)
        {
            return operationService.GetPaging(filter);
        }

        #endregion

        #region 删除操作功能

        /// <summary>
        /// 删除操作功能
        /// </summary>
        /// <param name="removeOperationParameter">操作功能删除信息</param>
        /// <returns>返回执行结果</returns>
        public Result RemoveOperation(RemoveOperationParameter removeOperationParameter)
        {
            if (removeOperationParameter is null)
            {
                throw new ArgumentNullException(nameof(removeOperationParameter));
            }

            using (var work = WorkManager.Create())
            {
                var removeResult = operationService.Remove(removeOperationParameter.Ids);
                if (!removeResult.Success)
                {
                    return removeResult;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? Result.SuccessResult("删除成功") : Result.FailedResult("删除失败");
            }
        }

        #endregion

        #region 修改操作状态

        /// <summary>
        /// 修改操作功能状态
        /// </summary>
        /// <param name="statusInfo">状态信息</param>
        /// <returns>执行结果</returns>
        public Result ModifyOperationStatus(ModifyOperationStatusParameter modifyOperationStatusParameter)
        {
            if (modifyOperationStatusParameter is null)
            {
                throw new ArgumentNullException(nameof(modifyOperationStatusParameter));
            }

            using (var work = WorkManager.Create())
            {
                var modifyResult = operationService.ModifyStatus(modifyOperationStatusParameter);
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = work.Commit();
                return commitResult.Success ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
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
        public Result ClearPermission(IEnumerable<long> operationIds)
        {
            using (var work = WorkManager.Create())
            {
                var clearResult = operationPermissionService.ClearByOperation(operationIds);
                if (!clearResult.Success)
                {
                    return clearResult;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? Result.SuccessResult("清除成功") : Result.FailedResult("清除失败");
            }
        }

        #endregion

        #region 授权验证

        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="checkAuthorizationParameter">授权验证信息</param>
        /// <returns>返回授权是否验证通过</returns>
        public bool CheckAuthorization(CheckAuthorizationParameter checkAuthorizationParameter)
        {
            return operationService.CheckAuthorization(checkAuthorizationParameter);
        }

        #endregion

        #region 初始化操作功能

        /// <summary>
        /// 初始化操作功能
        /// </summary>
        /// <param name="initializeOperationParameter">操作功能初始化信息</param>
        /// <returns>返回执行结果</returns>
        public Result Initialize(InitializeOperationParameter initializeOperationParameter)
        {
            using (var work = WorkManager.Create())
            {
                var initResult = operationService.Initialize(initializeOperationParameter);
                if (!initResult.Success)
                {
                    return initResult;
                }

                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? Result.SuccessResult("初始化成功") : Result.FailedResult("初始化失败");
            }
        }

        #endregion
    }
}
