using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EZNEWApp.Module.Sys;
using EZNEW.Model;
using EZNEW.Web.Mvc;
using EZNEW.Web.Security.Authorization;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEWApp.AppServiceContract.Sys;
using EZNEW.Paging;
using Api.Console.Model.Response;

namespace Api.Console.Controllers.Sys
{
    /// <summary>
    /// 功能操作管理
    /// </summary>
    public class OperationController : ApiBaseController
    {
        readonly IOperationAppService operationAppService;

        public OperationController(IOperationAppService operationAppService)
        {
            this.operationAppService = operationAppService;
        }

        #region 保存功能操作

        /// <summary>
        /// 保存功能操作
        /// </summary>
        /// <param name="operation">功能操作信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        public Result<Operation> SaveOperation(Operation operation)
        {
            SaveOperationParameter saveInfo = new SaveOperationParameter()
            {
                Operation = operation
            };
            return operationAppService.SaveOperation(saveInfo);
        }

        #endregion

        #region 获取功能操作配置

        /// <summary>
        /// 获取功能操作配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("config")]
        public GetOperationConfigurationResponse GetOperationConfiguration()
        {
            return new GetOperationConfigurationResponse()
            {
                StatusCollection = OperationStatus.Enable.GetEnumKeyValueCollection(),
                AccessLevelCollection = OperationAccessLevel.Authorized.GetEnumKeyValueCollection()
            };
        }

        #endregion

        #region 删除功能操作

        /// <summary>
        /// 删除功能操作
        /// </summary>
        /// <param name="removeOperationParameter">删除功能操作参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public Result DeleteOperation(RemoveOperationParameter removeOperationParameter)
        {
            return operationAppService.RemoveOperation(removeOperationParameter);
        }

        #endregion

        #region 查询操作功能

        /// <summary>
        /// 查询操作功能
        /// </summary>
        /// <param name="filter">操作功能筛选条件</param>
        /// <returns></returns>
        [HttpPost]
        [Route("query")]
        public PagingInfo<Operation> QueryOperation(OperationFilter filter)
        {
            return operationAppService.GetOperationPaging(filter);
        }

        #endregion

        #region 检查功能操作名称

        /// <summary>
        /// 检查功能操作名称
        /// </summary>
        /// <param name="operation">功能操作信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("check-name")]
        public bool CheckOperationName(Operation operation)
        {
            if (string.IsNullOrWhiteSpace(operation?.Name))
            {
                return false;
            }
            return !operationAppService.ExistOperationName(operation.Name, operation.Id);
        }

        #endregion

        #region 初始化默认功能操作

        /// <summary>
        /// 初始化默认功能操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResolveDefaultAuthorizationOperation()
        {
            var operationGroups = AuthorizationManager.ResolveDefaultAuthorizations();
            if (!operationGroups.IsNullOrEmpty())
            {
                List<OperationGroup> operationGroupParameters = new List<OperationGroup>();
                List<Operation> operationParameters = new List<Operation>();
                foreach (var group in operationGroups)
                {
                    var nowGroup = new OperationGroup() { Name = group.Name };
                    nowGroup.InitIdentityValue();
                    operationGroupParameters.Add(nowGroup);
                    if (!group.ChildGroups.IsNullOrEmpty())
                    {
                        foreach (var childGroup in group.ChildGroups)
                        {
                            var nowChildGroup = new OperationGroup() { Name = childGroup.Name, Parent = nowGroup.Id };
                            nowChildGroup.InitIdentityValue();
                            operationGroupParameters.Add(nowChildGroup);
                            if (!childGroup.Actions.IsNullOrEmpty())
                            {
                                foreach (var operation in childGroup.Actions)
                                {
                                    operationParameters.Add(new Operation()
                                    {
                                        Name = operation.Name,
                                        Action = operation.Action,
                                        Controller = operation.Controller,
                                        Group = nowChildGroup.Id,
                                        Status = OperationStatus.Enable,
                                        AccessLevel = operation.Public ? OperationAccessLevel.Public : OperationAccessLevel.Authorized
                                    });
                                }
                            }
                        }
                    }
                    if (!group.Actions.IsNullOrEmpty())
                    {
                        foreach (var operation in group.Actions)
                        {
                            operationParameters.Add(new Operation()
                            {
                                Name = operation.Name,
                                Action = operation.Action,
                                Controller = operation.Controller,
                                Group = nowGroup.Id,
                                Status = OperationStatus.Enable,
                                AccessLevel = operation.Public ? OperationAccessLevel.Public : OperationAccessLevel.Authorized
                            });
                        }
                    }
                }
                var initParameter = new InitializeOperationParameter()
                {
                    OperationGroups = operationGroupParameters,
                    Operations = operationParameters
                };
                operationAppService.Initialize(initParameter);
            }
            return Json(Result.SuccessResult("解析成功"));
        }

        #endregion
    }
}
