using System;
using EZNEW.Develop.Entity;
using EZNEW.Framework.Extension;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 权限&授权操作关联
    /// </summary>
    [Serializable]
    [Entity("Sys_AuthorityBindOperation", "Sys", "权限&授权操作关联")]
    public class AuthorityBindOperationEntity : BaseEntity<AuthorityBindOperationEntity>
    {
        #region	字段

        /// <summary>
        /// 授权操作
        /// </summary>
        [EntityField(Description = "授权操作", PrimaryKey = true)]
        [EntityRelation(typeof(AuthorityOperationEntity), "SysNo")]
        public long AuthorityOperationSysNo
        {
            get { return valueDict.GetValue<long>("AuthorityOperationSysNo"); }
            set { valueDict.SetValue("AuthorityOperationSysNo", value); }
        }

        /// <summary>
        /// 权限
        /// </summary>
        [EntityField(Description = "权限", PrimaryKey = true)]
        [EntityRelation(typeof(AuthorityEntity), "SysNo")]
        public long AuthoritySysNo
        {
            get { return valueDict.GetValue<long>("AuthoritySysNo"); }
            set { valueDict.SetValue("AuthoritySysNo", value); }
        }

        #endregion
    }
}