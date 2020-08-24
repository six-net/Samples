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
    /// 操作功能应用服务
    /// </summary>
    public class OperationAppService : IOperationAppService
    {
        /// <summary>
        /// 操作功能业务
        /// </summary>
        readonly IOperationBusiness operationBusiness;

        public OperationAppService(IOperationBusiness operationBusiness)
        {
            this.operationBusiness = operationBusiness;
        }

        #region 保存操作功能

        /// <summary>
        /// 保存操作功能
        /// </summary>
        /// <param name="saveOperationDto">操作功能保存信息</param>
        /// <returns>返回执行结果</returns>
        public Result<OperationDto> SaveOperation(SaveOperationDto saveOperationDto)
        {
            return operationBusiness.SaveOperation(saveOperationDto);
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
            return operationBusiness.GetOperation(filter);
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
            return operationBusiness.GetOperationList(filter);
        }

        #endregion

        #region 获取操作功能分页

        /// <summary>
        /// 获取操作功能分页
        /// </summary>
        /// <param name="filter">操作功能筛选信息</param>
        /// <returns>返回操作功能列表</returns>
        public IPaging<OperationDto> GetOperationPaging(OperationFilterDto filter)
        {
            return operationBusiness.GetOperationPaging(filter);
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
            return operationBusiness.RemoveOperation(removeOperationDto);
        }

        #endregion

        #region 修改操作功能状态

        /// <summary>
        /// 修改操作功能状态
        /// </summary>
        /// <param name="modifyOperationStatusDto">操作功能状态修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyOperationStatus(ModifyOperationStatusDto modifyOperationStatusDto)
        {
            return operationBusiness.ModifyOperationStatus(modifyOperationStatusDto);
        }

        #endregion

        #region 验证操作功能名是否存在

        /// <summary>
        /// 验证操作功能名是否存在
        /// </summary>
        /// <param name="name">操作功能名称</param>
        /// <param name="excludeId">需要排除的操作功能编号</param>
        /// <returns>返回操作功能名称是否存在</returns>
        public bool ExistOperationName(string name, long excludeId)
        {
            return operationBusiness.ExistOperationName(name, excludeId);
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
            return operationBusiness.ClearOperationPermission(operationIds);
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
            return operationBusiness.CheckAuthorization(checkAuthorizationDto);
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
            return operationBusiness.Initialize(initializeOperationDto);
        }

        #endregion
    }
}
