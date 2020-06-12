using EZNEW.Domain.Sys.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Develop.CQuery;
using EZNEW.Query.Sys;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Service.Param;
using EZNEW.DependencyInjection;
using EZNEW.Paging;
using EZNEW.Response;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 授权操作服务
    /// </summary>
    public class AuthorityOperationService : IAuthorityOperationService
    {
        static readonly IAuthorityOperationRepository authorityOperationRepository = ContainerManager.Resolve<IAuthorityOperationRepository>();
        static readonly IAuthorityOperationGroupService authorityOperationGroupService = ContainerManager.Resolve<IAuthorityOperationGroupService>();

        #region 修改授权操作状态

        /// <summary>
        /// 修改授权操作状态
        /// </summary>
        /// <param name="statusInfo">状态信息</param>
        public Result ModifyStatus(params ModifyAuthorityOperationStatus[] statusInfos)
        {
            if (statusInfos.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何要修改的状态信息");
            }
            var operationIds = statusInfos.Select(c => c.OperationId).Distinct().ToList();
            var operatioList = GetAuthorityOperationList(operationIds);
            if (operatioList.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何要修改状态的操作信息");
            }
            foreach (var operation in operatioList)
            {
                if (operation == null)
                {
                    continue;
                }
                var newStatus = statusInfos.FirstOrDefault(c => c.OperationId == operation.SysNo);
                if (newStatus == null)
                {
                    continue;
                }
                operation.Status = newStatus.Status;
                operation.Save();
            }
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 删除授权操作

        /// <summary>
        /// 删除授权操作
        /// </summary>
        /// <param name="authorityOperationIds">要删除的数据编号</param>
        public Result DeleteAuthorityOperation(IEnumerable<long> authorityOperationIds)
        {
            if (authorityOperationIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何要删除的信息");
            }
            IQuery delQuery = QueryManager.Create<AuthorityOperationQuery>();
            delQuery.In<AuthorityOperationQuery>(a => a.SysNo, authorityOperationIds);
            authorityOperationRepository.Remove(delQuery);
            return Result.SuccessResult("删除成功");
        }

        #endregion

        #region 保存授权操作

        /// <summary>
        /// 保存授权操作
        /// </summary>
        /// <param name="authorityOperation">授权操作对象</param>
        /// <returns>执行结果</returns>
        public Result<AuthorityOperation> SaveAuthorityOperation(AuthorityOperation authorityOperation)
        {
            if (authorityOperation == null)
            {
                return Result<AuthorityOperation>.FailedResult("授权操作信息不完整");
            }
            return authorityOperation.SysNo > 0 ? UpdateAuthorityOperation(authorityOperation) : AddAuthorityOperation(authorityOperation);
        }

        /// <summary>
        /// 添加授权操作
        /// </summary>
        /// <param name="authorityOperation">授权操作对象</param>
        /// <returns>执行结果</returns>
        static Result<AuthorityOperation> AddAuthorityOperation(AuthorityOperation authorityOperation)
        {
            authorityOperation.Save();
            var result = Result<AuthorityOperation>.SuccessResult("添加成功");
            result.Data = authorityOperation;
            return result;
        }

        /// <summary>
        /// 更新授权操作
        /// </summary>
        /// <param name="authorityOperation">授权操作对象</param>
        /// <returns>执行结果</returns>
        static Result<AuthorityOperation> UpdateAuthorityOperation(AuthorityOperation newAuthorityOperation)
        {
            AuthorityOperation nowAuthorityOperation = authorityOperationRepository.Get(QueryManager.Create<AuthorityOperationQuery>(a => a.SysNo == newAuthorityOperation.SysNo));
            if (nowAuthorityOperation == null)
            {
                return Result<AuthorityOperation>.FailedResult("请指定要修改授权操作");
            }

            #region 操作信息修改

            nowAuthorityOperation.Name = newAuthorityOperation.Name;
            nowAuthorityOperation.ControllerCode = newAuthorityOperation.ControllerCode;
            nowAuthorityOperation.ActionCode = newAuthorityOperation.ActionCode;
            nowAuthorityOperation.Status = newAuthorityOperation.Status;
            nowAuthorityOperation.Remark = newAuthorityOperation.Remark;
            nowAuthorityOperation.AuthorizeType = newAuthorityOperation.AuthorizeType;
            nowAuthorityOperation.SetGroup(newAuthorityOperation.Group.MapTo<AuthorityOperationGroup>());

            #endregion

            nowAuthorityOperation.Save();

            var result = Result<AuthorityOperation>.SuccessResult("修改成功");
            result.Data = nowAuthorityOperation;
            return result;
        }

        #endregion

        #region 获取授权操作

        /// <summary>
        /// 获取授权操作
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public AuthorityOperation GetAuthorityOperation(IQuery query)
        {
            var authorityOperation = authorityOperationRepository.Get(query);
            return authorityOperation;
        }

        /// <summary>
        /// 获取授权操作
        /// </summary>
        /// <param name="operationId">操作编号</param>
        /// <returns></returns>
        public AuthorityOperation GetAuthorityOperation(long operationId)
        {
            if (operationId <= 0)
            {
                return null;
            }
            IQuery query = QueryManager.Create<AuthorityOperationQuery>(c => c.SysNo == operationId);
            return GetAuthorityOperation(query);
        }

        /// <summary>
        /// 获取授权操作
        /// </summary>
        /// <param name="controllerCode">操作控制器编码（不区分大小写）</param>
        /// <param name="actionCode">操作方法编码（不区分大小写）</param>
        /// <returns></returns>
        public AuthorityOperation GetAuthorityOperation(string controllerCode, string actionCode)
        {
            if (controllerCode.IsNullOrEmpty() || actionCode.IsNullOrEmpty())
            {
                return null;
            }
            AuthorityOperation operation = AuthorityOperation.CreateAuthorityOperation(controllerCode: controllerCode, actionCode: actionCode);
            IQuery query = QueryManager.Create<AuthorityOperationQuery>(c => c.ControllerCode == operation.ControllerCode && c.ActionCode == operation.ActionCode);
            return GetAuthorityOperation(query);
        }

        #endregion

        #region 获取授权操作列表

        /// <summary>
        /// 获取授权操作列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public List<AuthorityOperation> GetAuthorityOperationList(IQuery query)
        {
            var authorityOperationList = authorityOperationRepository.GetList(query);
            authorityOperationList = LoadOtherObjectData(authorityOperationList, query);
            return authorityOperationList;
        }

        /// <summary>
        /// 获取授权操作列表
        /// </summary>
        /// <param name="ids">授权操作编号</param>
        /// <returns></returns>
        public List<AuthorityOperation> GetAuthorityOperationList(IEnumerable<long> ids)
        {
            if (ids.IsNullOrEmpty())
            {
                return new List<AuthorityOperation>(0);
            }
            IQuery query = QueryManager.Create<AuthorityOperationQuery>(c => ids.Contains(c.SysNo));
            return GetAuthorityOperationList(query);
        }

        #endregion

        #region 获取授权操作分页

        /// <summary>
        /// 获取授权操作分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public IPaging<AuthorityOperation> GetAuthorityOperationPaging(IQuery query)
        {
            var authorityOperationPaging = authorityOperationRepository.GetPaging(query);
            var authorityOperationList = LoadOtherObjectData(authorityOperationPaging, query);
            return Pager.Create<AuthorityOperation>(authorityOperationPaging.Page, authorityOperationPaging.PageSize, authorityOperationPaging.TotalCount, authorityOperationList);
        }

        #endregion

        #region 验证操作名称是否存在

        /// <summary>
        /// 验证操作名称是否存在
        /// </summary>
        /// <param name="name">操作名称</param>
        /// <param name="excludeId">排除的编号</param>
        /// <returns></returns>
        public bool ExistOperationName(string name, long excludeId)
        {
            if (name.IsNullOrEmpty())
            {
                return false;
            }
            IQuery query = QueryManager.Create<AuthorityOperationQuery>(c => c.Name == name && c.SysNo != excludeId);
            return authorityOperationRepository.Exist(query);
        }

        #endregion

        #region 加载其它数据

        /// <summary>
        /// 加载其它数据
        /// </summary>
        /// <param name="operations">操作信息</param>
        /// <param name="query">筛选条件</param>
        /// <returns></returns>
        List<AuthorityOperation> LoadOtherObjectData(IEnumerable<AuthorityOperation> operations, IQuery query)
        {
            if (operations.IsNullOrEmpty())
            {
                return new List<AuthorityOperation>(0);
            }
            if (query == null)
            {
                return operations.ToList();
            }

            #region 操作分组

            List<AuthorityOperationGroup> groupList = null;
            if (query.AllowLoad<AuthorityOperation>(c => c.Group))
            {
                var groupIds = operations.Select(c => c.Group?.SysNo ?? 0).Distinct().ToList();
                groupList = authorityOperationGroupService.GetAuthorityOperationGroupList(groupIds);
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
                    operation.SetGroup(groupList.FirstOrDefault(c => c.SysNo == operation.Group?.SysNo));
                }
            }
            return operations.ToList();
        }

        #endregion
    }
}
