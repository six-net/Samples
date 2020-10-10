using System;
using System.Collections.Generic;
using EZNEW.Develop.Entity;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 权限分组
    /// </summary>
    [Serializable]
    [Entity("Sys_PermissionGroup", "Sys", "权限分组")]
    public class PermissionGroupEntity : BaseEntity<PermissionGroupEntity>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [EntityField(Description = "编号", PrimaryKey = true)]
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
        /// 排序
        /// </summary>
        [EntityField(Description = "排序")]
        public int Sort
        {
            get => GetPropertyValue<int>(nameof(Sort));
            set => SetPropertyValue(nameof(Sort), value);
        }

        /// <summary>
        /// 上级分组
        /// </summary>
        [EntityField(Description = "上级分组")]
        public long Parent
        {
            get => GetPropertyValue<long>(nameof(Parent));
            set => SetPropertyValue(nameof(Parent), value);
        }

        /// <summary>
        /// 说明
        /// </summary>
        [EntityField(Description = "说明")]
        public string Remark
        {
            get => GetPropertyValue<string>(nameof(Remark));
            set => SetPropertyValue(nameof(Remark), value);
        }
    }
}