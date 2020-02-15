using System;
using EZNEW.Develop.Entity;
using EZNEW.Framework.Extension;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 角色权限
    /// </summary>
    [Serializable]
    [Entity("Sys_RoleAuthorize", "Sys", "角色权限")]
    public class RoleAuthorizeEntity : BaseEntity<RoleAuthorizeEntity>
    {
        #region	字段

        /// <summary>
        /// 角色
        /// </summary>
        [EntityField(Description = "角色", PrimaryKey = true)]
        [EntityRelation(typeof(RoleEntity), "SysNo")]
        [EntityRelation(typeof(UserRoleEntity), "RoleSysNo")]
        public long RoleSysNo
        {
            get { return valueDict.GetValue<long>("RoleSysNo"); }
            set { valueDict.SetValue("RoleSysNo", value); }
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