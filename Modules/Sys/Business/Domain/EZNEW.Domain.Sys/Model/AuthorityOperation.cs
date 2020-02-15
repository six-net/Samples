using System;
using EZNEW.Develop.Domain.Aggregation;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Develop.CQuery;
using EZNEW.Query.Sys;
using EZNEW.Framework.Extension;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Framework.ValueType;
using EZNEW.Framework;
using EZNEW.Application.Identity.Auth;
using EZNEW.Application.Identity;
using EZNEW.Framework.Code;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 授权操作
    /// </summary>
    public class AuthorityOperation : AggregationRoot<AuthorityOperation>
    {
        #region	字段

        /// <summary>
        /// 主键编号
        /// </summary>
        protected long sysNo;

        /// <summary>
        /// 控制器
        /// </summary>
        protected string controllerCode;

        /// <summary>
        /// 操作方法
        /// </summary>
        protected string actionCode;

        /// <summary>
        /// 名称
        /// </summary>
        protected string name;

        /// <summary>
        /// 状态
        /// </summary>
        protected AuthorityOperationStatus status;

        /// <summary>
        /// 排序
        /// </summary>
        protected int sort;

        /// <summary>
        /// 操作分组
        /// </summary>
        protected LazyMember<AuthorityOperationGroup> group;

        /// <summary>
        /// 授权类型
        /// </summary>
        protected AuthorityOperationAuthorizeType authorizeType;

        /// <summary>
        /// 方法描述
        /// </summary>
        protected string remark;

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
            get
            {
                return sysNo;
            }
            set
            {
                sysNo = value;
            }
        }

        /// <summary>
        /// 控制器
        /// </summary>
        public string ControllerCode
        {
            get
            {
                return controllerCode;
            }
            set
            {
                controllerCode = value?.ToUpper();
            }
        }

        /// <summary>
        /// 操作方法
        /// </summary>
        public string ActionCode
        {
            get
            {
                return actionCode;
            }
            set
            {
                actionCode = value?.ToUpper();
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public AuthorityOperationStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            get
            {
                return sort;
            }
            set
            {
                sort = value;
            }
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
            get
            {
                return authorizeType;
            }
            set
            {
                authorizeType = value;
            }
        }

        /// <summary>
        /// 方法描述
        /// </summary>
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
            }
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
            sysNo = GenerateAuthorityOperationId();
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
            return this.Instance<IAuthorityOperationGroupRepository>().Get(QueryFactory.Create<AuthorityOperationGroupQuery>(r => r.SysNo == group.CurrentValue.SysNo));
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
            if (group.CurrentValue == null || group.CurrentValue.SysNo <= 0)
            {
                throw new Exception("请设置操作所属分组");
            }
            IQuery groupQuery = QueryFactory.Create<AuthorityOperationGroupQuery>(c => c.SysNo == group.CurrentValue.SysNo);
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
            IQuery query = QueryFactory.Create();
            IQuery bindQuery = QueryFactory.Create<AuthorityBindOperationQuery>();
            bindQuery.AddQueryFields<AuthorityBindOperationQuery>(a => a.AuthoritySysNo);
            bindQuery.And<AuthorityBindOperationQuery>(a => a.AuthorityOperationSysNo == sysNo);
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
            return sysNo <= 0;
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
            return SerialNumber.GetSerialNumber(IdentityApplicationHelper.GetIdGroupCode(IdentityGroup.授权操作));
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

        protected override string GetIdentityValue()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #endregion
    }
}