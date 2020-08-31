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
        [EntityField(Description = "角色编号", PrimaryKey = true)]
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
        /// 等级
        /// </summary>
        [EntityField(Description = "等级")]
        public int Level
        {
            get => GetPropertyValue<int>(nameof(Level));
            set => SetPropertyValue(nameof(Level), value);
        }

        /// <summary>
        /// 上级
        /// </summary>
        [EntityField(Description = "上级")]
        public long Parent
        {
            get => GetPropertyValue<long>(nameof(Parent));
            set => SetPropertyValue(nameof(Parent), value);
        }

        /// <summary>
        /// 排序
        /// </summary>
        [EntityField(Description = "排序")]
        public int Sort
        {
            get => GetPropertyValue<int>(nameof(Sort));
            set => SetPropertyValue(nameof(Sort), value);
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