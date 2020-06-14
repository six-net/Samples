using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Module.Sys;
using EZNEW.Code;
using EZNEW.ValueType;
using EZNEW.Develop.Domain.Aggregation;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Develop.CQuery;
using EZNEW.Query.Sys;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 授权操作
    /// </summary>
    public class AuthorityOperation : AggregationRoot<AuthorityOperation>
    {
        #region	字段

        /// <summary>
        /// 操作分组
        /// </summary>
        protected LazyMember<AuthorityOperationGroup> group;

        /// <summary>
        /// 操作对应的权限
        /// </summary>
        protected LazyMember<List<Authority>> authoritys;

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化授权操作对象
        /// </summary>
        private AuthorityOperation()
        {
            group = new LazyMember<AuthorityOperationGroup>(LoadAuthorityOperationGroup);
            authoritys = new LazyMember<List<Authority>>(LoadAuthority);
            repository = this.Instance<IAuthorityOperationRepository>();
        }

        #endregion

        #region	属性

        /// <summary>
        /// 主键编号
        /// </summary>
        public long SysNo
        {
            get; set;
        }

        /// <summary>
        /// 控制器
        /// </summary>
        public string ControllerCode
        {
            get; set;
        }

        /// <summary>
        /// 操作方法
        /// </summary>
        public string ActionCode
        {
            get; set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public AuthorityOperationStatus Status
        {
            get; set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            get; set;
        }

        /// <summary>
        /// 操作分组
        /// </summary>
        public AuthorityOperationGroup Group
        {
            get
            {
                return group.Value;
            }
            protected set
            {
                group.SetValue(value, false);
            }
        }

        /// <summary>
        /// 授权类型
        /// </summary>
        public AuthorityOperationAuthorizeType AuthorizeType
        {
            get; set;
        }

        /// <summary>
        /// 方法描述
        /// </summary>
        public string Remark
        {
            get; set;
        }

        /// <summary>
        /// 操作对应的权限
        /// </summary>
        public List<Authority> Authoritys
        {
            get
            {
                return authoritys.Value;
            }
            protected set
            {
                authoritys.SetValue(value, false);
            }
        }

        #endregion

        #region 方法

        #region 功能方法

        #region 设置操作分组

        /// <summary>
        /// 设置操作分组
        /// </summary>
        /// <param name="group">分组信息</param>
        /// <param name="init">是否初始化</param>
        public void SetGroup(AuthorityOperationGroup group, bool init = true)
        {
            this.group.SetValue(group, init);
        }

        #endregion

        #region 添加新的权限

        /// <summary>
        /// 添加新的权限
        /// </summary>
        /// <param name="newAuthoritys">新的权限信息</param>
        public void AddAuthoritys(IEnumerable<Authority> newAuthoritys)
        {
            if (newAuthoritys.IsNullOrEmpty())
            {
                return;
            }
            List<Authority> nowAuthorityList = authoritys.Value ?? new List<Authority>(0);
            foreach (var authority in newAuthoritys)
            {
                if (authority == null)
                {
                    continue;
                }
                var nowAuthority = nowAuthorityList.FirstOrDefault(c => c.Code == authority.Code);
                if (nowAuthority == null)
                {
                    var newAuthority = authority.MapTo<Authority>();//重新生成一个对象，防止修改影响原对象的值
                    newAuthority.MarkNew();
                    nowAuthorityList.Add(newAuthority);
                    continue;
                }
            }
            authoritys.SetValue(nowAuthorityList, true);
        }

        #endregion

        #region 移除权限

        /// <summary>
        /// 移除权限
        /// </summary>
        /// <param name="removeAuthoritys">要移除的权限信息</param>
        public void RemoveAuthoritys(IEnumerable<Authority> removeAuthoritys)
        {
            if (removeAuthoritys.IsNullOrEmpty())
            {
                return;
            }
            List<Authority> nowAuthorityList = authoritys.Value;
            if (nowAuthorityList.IsNullOrEmpty())
            {
                return;
            }
            foreach (var authority in removeAuthoritys)
            {
                if (authority == null)
                {
                    continue;
                }
                var nowAuthority = nowAuthorityList.FirstOrDefault(c => c.Code == authority.Code);
            }
        }

        #endregion

        #region 初始化标识信息

        /// <summary>
        /// 初始化标识信息
        /// </summary>
        public override void InitIdentityValue()
        {
            base.InitIdentityValue();
            SysNo = GenerateAuthorityOperationId();
        }

        #endregion

        #endregion

        #region 内部方法

        #region 加载操作对应的分组

        /// <summary>
        /// 加载操作对应的分组
        /// </summary>
        /// <returns></returns>
        AuthorityOperationGroup LoadAuthorityOperationGroup()
        {
            if (!AllowLazyLoad(r => r.Group))
            {
                return group.CurrentValue;
            }
            if (group.CurrentValue == null || group.CurrentValue.SysNo <= 0)
            {
                return group.CurrentValue;
            }
            return this.Instance<IAuthorityOperationGroupRepository>().Get(QueryManager.Create<AuthorityOperationGroupQuery>(r => r.SysNo == group.CurrentValue.SysNo));
        }

        #endregion

        #region 保存数据验证

        /// <summary>
        /// 保存数据验证
        /// </summary>
        /// <returns></returns>
        protected override bool SaveValidation()
        {
            bool valResult = base.SaveValidation();
            if (!valResult)
            {
                return valResult;
            }
            ControllerCode = ControllerCode?.ToUpper() ?? string.Empty;
            ActionCode = ActionCode?.ToUpper() ?? string.Empty;
            if (group.CurrentValue == null || group.CurrentValue.SysNo <= 0)
            {
                throw new Exception("请设置操作所属分组");
            }
            IQuery groupQuery = QueryManager.Create<AuthorityOperationGroupQuery>(c => c.SysNo == group.CurrentValue.SysNo);
            if (!this.Instance<IAuthorityOperationGroupRepository>().Exist(groupQuery))
            {
                throw new Exception("请设置正确的分组");
            }
            return true;
        }

        #endregion

        #region 加载操作对应的权限信息

        /// <summary>
        /// 加载操作对应的权限信息
        /// </summary>
        /// <returns></returns>
        List<Authority> LoadAuthority()
        {
            if (!AllowLazyLoad(r => r.Authoritys))
            {
                return authoritys.CurrentValue;
            }
            IQuery query = QueryManager.Create();
            IQuery bindQuery = QueryManager.Create<AuthorityBindOperationQuery>();
            bindQuery.AddQueryFields<AuthorityBindOperationQuery>(a => a.AuthoritySysNo);
            bindQuery.And<AuthorityBindOperationQuery>(a => a.AuthorityOperationSysNo == SysNo);
            query.And<AuthorityQuery>(a => a.Code, CriteriaOperator.In, bindQuery);
            return this.Instance<IAuthorityRepository>().GetList(query);
        }

        #endregion

        #region 验证对象标识信息是否未设置

        /// <summary>
        /// 判断对象标识信息是否未设置
        /// </summary>
        /// <returns></returns>
        public override bool IdentityValueIsNone()
        {
            return SysNo <= 0;
        }

        #endregion

        #region 获取对象标识值

        protected override string GetIdentityValue()
        {
            return SysNo.ToString();
        }

        #endregion

        #endregion

        #region 静态方法

        #region 生成一个操作编号

        /// <summary>
        /// 生成一个操作编号
        /// </summary>
        /// <returns></returns>
        public static long GenerateAuthorityOperationId()
        {
            return SysManager.GetId(SysModuleObject.AuthorityOperation);
        }

        #endregion

        #region 创建授权操作

        /// <summary>
        /// 创建一个授权操作对象
        /// </summary>
        /// <param name="sysNo">编号</param>
        /// <param name="name">名称</param>
        /// <param name="controllerCode">控制编码</param>
        /// <param name="actionCode">方法编码</param>
        /// <returns></returns>
        public static AuthorityOperation CreateAuthorityOperation(long sysNo = 0, string name = "", string controllerCode = "", string actionCode = "")
        {
            sysNo = sysNo <= 0 ? GenerateAuthorityOperationId() : sysNo;
            return new AuthorityOperation()
            {
                SysNo = sysNo,
                Name = name,
                ControllerCode = controllerCode,
                ActionCode = actionCode
            };
        }

        #endregion

        #endregion

        #endregion
    }
}