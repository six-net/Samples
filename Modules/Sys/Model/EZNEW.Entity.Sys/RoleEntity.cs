using System;
using System.Collections.Generic;
using EZNEW.Develop.Entity;
using EZNEW.Module.Sys;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 角色
    /// </summary>
    [Serializable]
    [Entity("Sys_Role", "Sys", "角色")]
    public class RoleEntity : BaseEntity<RoleEntity>
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        [EntityField(Description = "角色编号", Role = FieldRole.PrimaryKey)]
        public long Id
        {
            get => GetPropertyValue<long>(nameof(Id));
            set => SetPropertyValue(nameof(Id), value);
        }

        /// <summary>
        /// 名称
        /// </summary>
        [EntityField(Description = "名称")]
        public string Name
        {
            get => GetPropertyValue<string>(nameof(Name));
            set => SetPropertyValue(nameof(Name), value);
        }

        /// <summary>
        /// 状态
        /// </summary>
        [EntityField(Description = "状态")]
        public RoleStatus Status
        {
            get => GetPropertyValue<RoleStatus>(nameof(Status));
            set => SetPropertyValue(nameof(Status), value);
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        [EntityField(Description = "添加时间")]
        public DateTime CreateDate
        {
            get => GetPropertyValue<DateTime>(nameof(CreateDate));
            set => SetPropertyValue(nameof(CreateDate), value);
        }

        /// <summary>
        /// 备注信息
        /// </summary>
        [EntityField(Description = "备注信息")]
        public string Remark
        {
            get => GetPropertyValue<string>(nameof(Remark));
            set => SetPropertyValue(nameof(Remark), value);
        }
    }
}