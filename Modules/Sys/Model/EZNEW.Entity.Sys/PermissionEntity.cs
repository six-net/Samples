using System;
using System.Collections.Generic;
using EZNEW.Develop.Entity;
using EZNEW.Module.Sys;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 权限
    /// </summary>
    [Serializable]
    [Entity("Sys_Permission", "Sys", "权限")]
    public class PermissionEntity : BaseEntity<PermissionEntity>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [EntityField(Description = "编号", Role = FieldRole.PrimaryKey)]
        public long Id
        {
            get => GetPropertyValue<long>(nameof(Id));
            set => SetPropertyValue(nameof(Id), value);
        }

        /// <summary>
        /// 权限编码
        /// </summary>
        [EntityField(Description = "权限编码")]
        public string Code
        {
            get => GetPropertyValue<string>(nameof(Code));
            set => SetPropertyValue(nameof(Code), value);
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
        /// 权限类型
        /// </summary>
        [EntityField(Description = "权限类型")]
        public PermissionType Type
        {
            get => GetPropertyValue<PermissionType>(nameof(Type));
            set => SetPropertyValue(nameof(Type), value);
        }

        /// <summary>
        /// 状态
        /// </summary>
        [EntityField(Description = "状态")]
        public PermissionStatus Status
        {
            get => GetPropertyValue<PermissionStatus>(nameof(Status));
            set => SetPropertyValue(nameof(Status), value);
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
        /// 权限分组
        /// </summary>
        [EntityField(Description = "权限分组")]
        [EntityRelation(typeof(PermissionGroupEntity), nameof(PermissionGroupEntity.Id))]
        public long Group
        {
            get => GetPropertyValue<long>(nameof(Group));
            set => SetPropertyValue(nameof(Group), value);
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

        /// <summary>
        /// 添加时间
        /// </summary>
        [EntityField(Description = "添加时间")]
        public DateTime CreateDate
        {
            get => GetPropertyValue<DateTime>(nameof(CreateDate));
            set => SetPropertyValue(nameof(CreateDate), value);
        }
    }
}