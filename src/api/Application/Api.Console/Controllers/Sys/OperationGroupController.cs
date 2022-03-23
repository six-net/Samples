using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EZNEWApp.Domain.Sys.Model;
using EZNEW.Model;
using EZNEW.Web.Security.Authorization;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEWApp.Domain.Sys.Parameter;

namespace Api.Console.Controllers.Sys
{
    /// <summary>
    /// 功能操作分组
    /// </summary>
    public class OperationGroupController : ApiBaseController
    {
        readonly IOperationGroupAppService operationGroupAppService;

        public OperationGroupController(IOperationGroupAppService operationGroupAppService)
        {
            this.operationGroupAppService = operationGroupAppService;
        }

        #region 保存操作功能组
        
        /// <summary>
        /// 保存操作分组
        /// </summary>
        /// <param name="operationGroup">分组信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        public Result<OperationGroup> EditOperationGroup(OperationGroup operationGroup)
        {
            SaveOperationGroupParameter saveInfo = new SaveOperationGroupParameter()
            {
                OperationGroup = operationGroup
            };
            return operationGroupAppService.SaveOperationGroup(saveInfo);
        }

        #endregion

        #region 删除操作功能组

        /// <summary>
        /// 删除功能分组
        /// </summary>
        /// <param name="removeOperationGroupParameter">删除功能分组参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public Result DeleteOperationGroup(RemoveOperationGroupParameter removeOperationGroupParameter)
        {
            return operationGroupAppService.RemoveOperationGroup(removeOperationGroupParameter);
        }

        #endregion

        #region 查询操作功能分组数据

        /// <summary>
        /// 查询功能分组
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        [Route("query")]
        [HttpPost]
        public List<OperationGroup> QueryOperationGroup(OperationGroupFilter filter)
        {
            return operationGroupAppService.GetOperationGroupList(filter).Select(c => c).OrderBy(r => r.Sort).ToList();
        }

        #endregion

        #region 检查操作分组名称是否可用

        /// <summary>
        /// 检查功能分组名
        /// </summary>
        /// <param name="group">功能分组信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("check-name")]
        public bool CheckOperationGroupName(OperationGroup group)
        {
            ExistOperationGroupNameParameter existInfo = new ExistOperationGroupNameParameter()
            {
                Name = group.Name,
                ExcludeId = group.Id
            };
            return !operationGroupAppService.ExistOperationGroupName(existInfo);
        }

        #endregion
    }
}
