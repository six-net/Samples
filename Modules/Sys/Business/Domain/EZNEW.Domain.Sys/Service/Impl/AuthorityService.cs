using EZNEW.Domain.Sys.Repository;
using EZNEW.Framework.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Framework.Extension;
using EZNEW.Develop.CQuery;
using EZNEW.Query.Sys;
using EZNEW.Framework;
using EZNEW.Domain.Sys.Model;
using EZNEW.Framework.Paging;
using EZNEW.Domain.Sys.Service.Param;
using EZNEW.Application.Identity.Auth;
using EZNEW.Framework.Response;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public class AuthorityService : IAuthorityService
    {
        static readonly IAuthorityRepository authRepository = ContainerManager.Resolve<IAuthorityRepository>();
        static readonly IAuthorityGroupService authorityGroupService = ContainerManager.Resolve<IAuthorityGroupService>();

        #region 修改权限状态

        /// <summary>
        /// 修改权限状态
        /// </summary>
        /// <param name="statusInfo">状态信息</param>
        public Result ModifyAuthorityStatus(params ModifyAuthorityStatus[] statusInfos)
        {
            #region 参数判断

            if (statusInfos.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定要操作的权限信息");
            }

            #endregion

            List<string> authCodes = statusInfos.Select(c => c.Code).Distinct().ToList();
            var authorityList = GetAuthorityList(authCodes);
            if (authorityList.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定要操作的权限信息");
            }
            foreach (var auth in authorityList)
            {
                if (auth == null)
                {
                    continue;
                }
                var newStatus = statusInfos.FirstOrDefault(c => c.Code == auth.Code);
                if (newStatus == null)
                {
                    continue;
                }
                auth.Status = newStatus.Status;
                auth.Save();
            }
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 删除权限

        /// <summary>
        /// 根据系统编号删除权限数据
        /// </summary>
        /// <param name="sysNos">权限系统编号</param>
        public void DeleteAuthority(IEnumerable<long> sysNos)
        {
            if (sysNos.IsNullOrEmpty())
            {
                throw new Exception("没有指定任何要删除的权限");
            }
            IQuery delQuery = QueryFactory.Create<AuthorityQuery>(a => sysNos.Contains(a.SysNo));
            authRepository.Remove(delQuery);
        }

        #endregion

        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="authority">权限对象</param>
        /// <returns>执行结果</returns>
        public Result<Authority> SaveAuthority(Authority authority)
        {
            if (authority == null)
            {
                return Result<Authority>.FailedResult("权限信息为空");
            }
            //权限分组
            if (authority.Group == null || authority.Group.SysNo <= 0)
            {
                return Result<Authority>.FailedResult("请设置正确的权限组");
            }
            if (!authorityGroupService.ExistAuthorityGroup(authority.Group.SysNo))
            {
                return Result<Authority>.FailedResult("请设置正确的权限组");
            }
            Authority nowAuthority = GetAuthority(authority.SysNo);
            if (nowAuthority == null)
            {
                nowAuthority = authority;
                nowAuthority.AuthorityType = AuthorityType.管理;
                nowAuthority.CreateDate = DateTime.Now;
                nowAuthority.Sort = 0;
            }
            else
            {
                nowAuthority.Code = authority.Code;
                nowAuthority.Name = authority.Name;
                nowAuthority.Status = authority.Status;
                nowAuthority.Remark = authority.Remark;
            }
            nowAuthority.Save();
            var result = Result<Authority>.SuccessResult("保存成功");
            result.Data = nowAuthority;
            return result;
        }

        #endregion

        #region 获取权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public Authority GetAuthority(IQuery query)
        {
            var authority = authRepository.Get(query);
            return authority;
        }

        /// <summary>
        /// 根据权限编号获取权限
        /// </summary>
        /// <param name="authCode">权限编码</param>
        /// <returns>权限对象</returns>
        public Authority GetAuthority(string authCode)
        {
            if (authCode.IsNullOrEmpty())
            {
                return null;
            }
            IQuery authQuery = QueryFactory.Create<AuthorityQuery>(a => a.Code == authCode);
            return GetAuthority(authQuery);
        }

        /// <summary>
        /// 根据系统编号获取权限
        /// </summary>
        /// <param name="sysNo">权限系统编号</param>
        /// <returns>权限对象</returns>
        public Authority GetAuthority(long sysNo)
        {
            if (sysNo < 1)
            {
                return null;
            }
            IQuery query = QueryFactory.Create<AuthorityQuery>(a => a.SysNo == sysNo);
            return GetAuthority(query);
        }

        #endregion

        #region 获取权限列表

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public List<Authority> GetAuthorityList(IQuery query)
        {
            var authorityList = authRepository.GetList(query);
            authorityList = LoadOtherObjectData(authorityList, query);
            return authorityList;
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="codes">权限码</param>
        /// <returns></returns>
        public List<Authority> GetAuthorityList(IEnumerable<string> codes)
        {
            if (codes.IsNullOrEmpty())
            {
                return new List<Authority>(0);
            }
            IQuery query = QueryFactory.Create<AuthorityQuery>(c => codes.Contains(c.Code));
            return GetAuthorityList(query);
        }

        #endregion

        #region 获取权限分页

        /// <summary>
        /// 获取权限分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public IPaging<Authority> GetAuthorityPaging(IQuery query)
        {
            var authorityPaging = authRepository.GetPaging(query);
            var authorityList = LoadOtherObjectData(authorityPaging, query);
            return new Paging<Authority>(authorityPaging.Page, authorityPaging.PageSize, authorityPaging.TotalCount, authorityList);
        }

        #endregion

        #region 检查权限编码是否存在

        /// <summary>
        /// 检查权限编码是否存在
        /// </summary>
        /// <param name="nowSysNo">当前系统编号</param>
        /// <param name="code">权限编码</param>
        /// <returns></returns>
        public bool ExistAuthorityCode(long nowSysNo, string code)
        {
            if (code.IsNullOrEmpty())
            {
                return false;
            }
            IQuery query = QueryFactory.Create<AuthorityQuery>(c => c.Code == code && c.SysNo != nowSysNo);
            return authRepository.Exist(query);
        }

        #endregion

        #region 检查权限名称是否存在

        /// <summary>
        /// 检查权限名称是否存在
        /// </summary>
        /// <param name="nowSysNo">当前系统编号</param>
        /// <param name="name">权限名称</param
        /// <returns></returns>
        public bool ExistAuthorityName(long nowSysNo, string name)
        {
            if (name.IsNullOrEmpty())
            {
                return false;
            }
            IQuery query = QueryFactory.Create<AuthorityQuery>(c => c.Name == name && c.SysNo != nowSysNo);
            return authRepository.Exist(query);
        }

        #endregion

        #region 加载其它数据

        /// <summary>
        /// 加载其它数据
        /// </summary>
        /// <param name="authoritys">权限数据</param>
        /// <param name="query">筛选条件</param>
        /// <returns></returns>
        List<Authority> LoadOtherObjectData(IEnumerable<Authority> authoritys, IQuery query)
        {
            if (authoritys.IsNullOrEmpty())
            {
                return new List<Authority>(0);
            }
            if (query == null)
            {
                return authoritys.ToList();
            }

            #region 权限分组

            List<AuthorityGroup> groupList = null;
            if (query.AllowLoad<Authority>(c => c.Group))
            {
                var groupIds = authoritys.Select(c => c.Group?.SysNo ?? 0).Distinct().ToList();
                groupList = authorityGroupService.GetAuthorityGroupList(groupIds);
            }

            #endregion

            foreach (var auth in authoritys)
            {
                if (auth == null)
                {
                    continue;
                }
                if (!groupList.IsNullOrEmpty())
                {
                    auth.SetGroup(groupList.FirstOrDefault(c => c.SysNo == auth.Group?.SysNo));
                }
            }

            return authoritys.ToList();
        }

        #endregion
    }
}
