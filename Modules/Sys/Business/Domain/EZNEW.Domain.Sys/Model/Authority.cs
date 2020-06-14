using System;
using EZNEW.Develop.Domain.Aggregation;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Query.Sys;
using EZNEW.Module.Sys;
using EZNEW.Develop.CQuery;
using EZNEW.ValueType;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Authority : AggregationRoot<Authority>
    {
        #region	字段

        /// <summary>
        /// 权限分组
        /// </summary>
        protected LazyMember<AuthorityGroup> group;

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化一个权限对象
        /// </summary>
        public Authority()
        {
            group = new LazyMember<AuthorityGroup>(LoadAuthorityGroup);
            repository = this.Instance<IAuthorityRepository>();
        }

        #endregion

        #region	属性

        public long SysNo
        {
            get; protected set;
        }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string Code
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
        /// 权限类型
        /// </summary>
        public AuthorityType AuthorityType
        {
            get; set;
        } = AuthorityType.管理;

        /// <summary>
        /// 状态
        /// </summary>
        public AuthorityStatus Status
        {
            get; set;
        } = AuthorityStatus.启用;

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            get; set;
        }

        /// <summary>
        /// 权限分组
        /// </summary>
        public AuthorityGroup Group
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
        /// 说明
        /// </summary>
        public string Remark
        {
            get; set;
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateDate
        {
            get; set;
        } = DateTime.Now;

        #endregion

        #region 方法

        #region 功能方法

        #region 设置分组

        /// <summary>
        /// 设置分组
        /// </summary>
        /// <param name="group">权限分组</param>
        /// <param name="init">是否初始化</param>
        public void SetGroup(AuthorityGroup group, bool init = true)
        {
            this.group.SetValue(group, init);
        }

        #endregion

        #region 初始化对象标识值

        /// <summary>
        /// 初始化对象标识值
        /// </summary>
        public override void InitIdentityValue()
        {
            SysNo = SysManager.GetId(SysModuleObject.Authority);
        }

        #endregion

        #endregion

        #region 内部方法

        #region 加载权限分组

        /// <summary>
        /// 加载权限分组
        /// </summary>
        /// <returns></returns>
        AuthorityGroup LoadAuthorityGroup()
        {
            if (!AllowLazyLoad(r => r.Group))
            {
                return group.CurrentValue;
            }
            if (group.CurrentValue == null || group.CurrentValue.SysNo <= 0)
            {
                return group.CurrentValue;
            }
            return this.Instance<IAuthorityGroupRepository>().Get(QueryManager.Create<AuthorityGroupQuery>(r => r.SysNo == group.CurrentValue.SysNo));
        }

        #endregion

        #region 验证对象标识信息是否未设置

        /// <summary>
        /// 判断对象标识信息是否未设置
        /// </summary>
        /// <returns></returns>
        public override bool IdentityValueIsNone()
        {
            return SysNo < 1;
        }

        #endregion

        #region 获取权限标识信息

        /// <summary>
        /// 获取权限标识信息
        /// </summary>
        /// <returns>权限对象标识</returns>
        protected override string GetIdentityValue()
        {
            return SysNo.ToString();
        }

        #endregion

        #endregion

        #region 静态方法

        /// <summary>
        /// 创建一个权限对象
        /// </summary>
        /// <param name="code">授权码</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static Authority CreateAuthority(long sysNo, string code = "", string name = "")
        {
            var authority = new Authority()
            {
                SysNo = sysNo,
                Code = code,
                Name = name
            };
            return authority;
        }

        #endregion

        #endregion
    }
}