using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Develop.CQuery;
using EZNEW.DependencyInjection;
using EZNEW.Domain.Sys.Model;
using EZNEW.Paging;
using EZNEW.Response;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Entity.Sys;
using EZNEW.Domain.Sys.Parameter;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Module.Sys;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 操作功能服务
    /// </summary>
    public class OperationService : IOperationService
    {
        static readonly IOperationRepository operationRepository = ContainerManager.Resolve<IOperationRepository>();
        static readonly IOperationGroupService operationGroupService = ContainerManager.Resolve<IOperationGroupService>();
        static readonly IUserService userService = ContainerManager.Resolve<IUserService>();

        #region 保存操作功能

        /// <summary>
        /// 保存操作功能
        /// </summary>
        /// <param name="operation">操作功能对象</param>
        /// <returns>返回执行结果</returns>
        public Result<Operation> Save(Operation operation)
        {
            return operation?.Save() ?? Result<Operation>.FailedResult("操作功能保存失败");
        }

        #endregion

        #region 删除操作功能

        /// <summary>
        /// 删除操作功能
        /// </summary>
        /// <param name="operationIds">操作功能编号</param>
        public Result Remove(IEnumerable<long> operationIds)
        {
            if (operationIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何要删除的信息");
            }
            IQuery delQuery = QueryManager.Create<OperationEntity>(c => operationIds.Contains(c.Id));
            operationRepository.Remove(delQuery);
            return Result.SuccessResult("删除成功");
        }

        #endregion

        #region 获取操作功能

        /// <summary>
        /// 获取操作功能
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns>返回操作功能</returns>
        Operation Get(IQuery query)
        {
            var operation = operationRepository.Get(query);
            return operation;
        }

        /// <summary>
        /// 获取操作功能
        /// </summary>
        /// <param name="operationId">操作功能编号</param>
        /// <returns>返回操作功能</returns>
        public Operation Get(long operationId)
        {
            if (operationId <= 0)
            {
                return null;
            }
            IQuery query = QueryManager.Create<OperationEntity>(c => c.Id == operationId);
            return Get(query);
        }

        /// <summary>
        /// 获取操作功能
        /// </summary>
        /// <param name="controllerCode">操作控制器编码（不区分大小写）</param>
        /// <param name="actionCode">操作方法编码（不区分大小写）</param>
        /// <returns>返回操作功能</returns>
        public Operation Get(string controllerCode, string actionCode)
        {
            if (string.IsNullOrWhiteSpace(controllerCode) || string.IsNullOrWhiteSpace(actionCode))
            {
                return null;
            }
            Operation operation = Operation.Create(controllerCode: controllerCode, actionCode: actionCode);
            IQuery query = QueryManager.Create<OperationEntity>(c => c.ControllerCode == operation.ControllerCode && c.ActionCode == operation.ActionCode);
            return Get(query);
        }

        /// <summary>
        /// 获取操作功能
        /// </summary>
        /// <param name="operationFilter">操作功能筛选器</param>
        /// <returns>返回操作功能</returns>
        public Operation Get(OperationFilter operationFilter)
        {
            return Get(operationFilter?.CreateQuery());
        }

        #endregion

        #region 获取操作功能列表

        /// <summary>
        /// 获取操作功能列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns>返回操作功能列表</returns>
        List<Operation> GetList(IQuery query)
        {
            var operationList = operationRepository.GetList(query);
            operationList = LoadOtherData(operationList, query);
            return operationList;
        }

        /// <summary>
        /// 获取操作功能列表
        /// </summary>
        /// <param name="ids">操作功能编号</param>
        /// <returns></returns>
        public List<Operation> GetList(IEnumerable<long> ids)
        {
            if (ids.IsNullOrEmpty())
            {
                return new List<Operation>(0);
            }
            IQuery query = QueryManager.Create<OperationEntity>(c => ids.Contains(c.Id));
            return GetList(query);
        }

        /// <summary>
        /// 获取操作功能列表
        /// </summary>
        /// <param name="operationFilter">操作功能筛选信息</param>
        /// <returns>返回操作功能列表</returns>
        public List<Operation> GetList(OperationFilter operationFilter)
        {
            return GetList(operationFilter?.CreateQuery());
        }

        #endregion

        #region 获取操作功能分页

        /// <summary>
        /// 获取操作功能分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        IPaging<Operation> GetPaging(IQuery query)
        {
            var operationPaging = operationRepository.GetPaging(query);
            var operationList = LoadOtherData(operationPaging, query);
            return new DefaultPaging<Operation>(operationPaging.Page, operationPaging.PageSize, operationPaging.TotalCount, operationList);
        }

        /// <summary>
        /// 获取操作功能分页
        /// </summary>
        /// <param name="operationFilter">操作功能筛选信息</param>
        /// <returns>获取操作功能分页</returns>
        public IPaging<Operation> GetPaging(OperationFilter operationFilter)
        {
            return GetPaging(operationFilter?.CreateQuery());
        }

        #endregion

        #region 修改操作功能状态

        /// <summary>
        /// 修改操作功能状态
        /// </summary>
        /// <param name="statusInfo">状态信息</param>
        public Result ModifyStatus(ModifyOperationStatusParameter modifyOperationStatus)
        {
            if (modifyOperationStatus?.StatusInfos.IsNullOrEmpty() ?? true)
            {
                return Result.FailedResult("没有指定任何要修改的状态信息");
            }
            var operationIds = modifyOperationStatus?.StatusInfos.Keys;
            var operatioList = GetList(operationIds);
            if (operatioList.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何要修改状态的操作信息");
            }
            foreach (var operation in operatioList)
            {
                if (operation == null || !modifyOperationStatus.StatusInfos.TryGetValue(operation.Id, out var newStatus))
                {
                    continue;
                }
                operation.Status = newStatus;
                operation.Save();
            }
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 验证操作名称是否存在

        /// <summary>
        /// 验证操作名称是否存在
        /// </summary>
        /// <param name="name">操作名称</param>
        /// <param name="excludeId">需要排除的操作功能编号</param>
        /// <returns>返回操作名称是否存在</returns>
        public bool ExistName(string name, long excludeId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            IQuery query = QueryManager.Create<OperationEntity>(c => c.Name == name && c.Id != excludeId);
            return operationRepository.Exist(query);
        }

        #endregion

        #region 验证用户操作授权

        /// <summary>
        /// 验证用户操作授权
        /// </summary>
        /// <param name="checkOperationAuthorization">用户操作功能授权信息</param>
        /// <returns>返回用户是否已授权</returns>
        public bool CheckAuthorization(CheckAuthorizationParameter checkOperationAuthorization)
        {
            if (checkOperationAuthorization?.Operation == null)
            {
                throw new ApplicationException("没有指定要检查的操作功能信息");
            }
            if (checkOperationAuthorization?.UserId < 1)
            {
                throw new ApplicationException("没有指定要检查的用户");
            }

            #region 用户信息验证

            User nowUser = userService.Get(checkOperationAuthorization.UserId);//当前用户
            if (nowUser == null)
            {
                return false;
            }
            //超级用户不受权限控制
            if (nowUser.SuperUser)
            {
                return true;
            }

            #endregion

            var operationQuery = new UserOperationFilter()
            {
                UserFilter = new UserFilter()
                {
                    Ids = new List<long>() { checkOperationAuthorization.UserId }
                },
                ControllerCode = checkOperationAuthorization.Operation.ControllerCode,
                ActionCode = checkOperationAuthorization.Operation.ActionCode
            }.CreateQuery();
            return operationRepository.Exist(operationQuery);
        }

        #endregion

        #region 初始化操作功能

        /// <summary>
        /// 初始化操作功能
        /// 将清除当前所有的操作功能
        /// </summary>
        /// <param name="initializeOperation">操作功能初始化信息</param>
        /// <returns>返回执行结果</returns>
        public Result Initialize(InitializeOperationParameter initializeOperation)
        {
            //清除当前信息
            operationRepository.Remove(QueryManager.Create<OperationEntity>());
            //初始化操作分组
            if (!initializeOperation?.OperationGroups.IsNullOrEmpty() ?? false)
            {
                operationGroupService.Initialize(initializeOperation.OperationGroups);
            }
            //初始化操作
            if (!initializeOperation?.Operations.IsNullOrEmpty() ?? false)
            {
                var groupNames = initializeOperation.Operations.Select(c => c.Group?.Name ?? string.Empty).Distinct();
                var groupList = operationGroupService.GetListByNames(groupNames);
                foreach (var operation in initializeOperation.Operations)
                {
                    var nowGroup = groupList.FirstOrDefault(c => c.Name == operation.Group?.Name);
                    if (nowGroup == null)
                    {
                        continue;
                    }
                    operation.SetGroup(nowGroup);
                    Save(operation);
                }
            }
            return Result.SuccessResult("初始化成功");
        }

        #endregion

        #region 加载其它数据

        /// <summary>
        /// 加载其它数据
        /// </summary>
        /// <param name="operations">操作信息</param>
        /// <param name="query">查询对象</param>
        /// <returns>返回操作功能列表</returns>
        List<Operation> LoadOtherData(IEnumerable<Operation> operations, IQuery query)
        {
            if (operations.IsNullOrEmpty())
            {
                return new List<Operation>(0);
            }
            if (query == null)
            {
                return operations.ToList();
            }

            #region 操作分组

            List<OperationGroup> groupList = null;
            if (query.AllowLoad<Operation>(c => c.Group))
            {
                var groupIds = operations.Select(c => c.Group?.Id ?? 0).Distinct().ToList();
                groupList = operationGroupService.GetList(groupIds);
            }

            #endregion

            foreach (var operation in operations)
            {
                if (operation == null)
                {
                    continue;
                }
                if (!groupList.IsNullOrEmpty())
                {
                    operation.SetGroup(groupList.FirstOrDefault(c => c.Id == operation.Group?.Id));
                }
            }
            return operations.ToList();
        }

        #endregion
    }
}
