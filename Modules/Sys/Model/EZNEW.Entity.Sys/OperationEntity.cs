using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EZNEW.Develop.Entity;
using EZNEW.Module.Sys;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 操作功能
    /// </summary>
    [Serializable]
    [Entity("Sys_Operation", "Sys", "操作功能")]
    public class OperationEntity : BaseEntity<OperationEntity>
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        [EntityField(Description = "主键编号", Role = FieldRole.PrimaryKey)]
        public long Id
        {
            get => GetPropertyValue<long>(nameof(Id));
            set => SetPropertyValue(nameof(Id), value);
        }

        /// <summary>
        /// 控制器
        /// </summary>
        [EntityField(Description = "控制器", IsRequired = true)]
        public string ControllerCode
        {
            get => GetPropertyValue<string>(nameof(ControllerCode));
            set => SetPropertyValue(nameof(ControllerCode), value);
        }

        /// <summary>
        /// 操作方法
        /// </summary>
        [EntityField(Description = "操作方法")]
        public string ActionCode
        {
            get => GetPropertyValue<string>(nameof(ActionCode));
            set => SetPropertyValue(nameof(ActionCode), value);
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
        public int Status
        {
            get => GetPropertyValue<int>(nameof(Status));
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
        /// 操作分组
        /// </summary>
        [EntityField(Description = "操作分组")]
        [EntityRelation(typeof(OperationGroupEntity), nameof(OperationGroupEntity.Id))]
        public long Group
        {
            get => GetPropertyValue<long>(nameof(Group));
            set => SetPropertyValue(nameof(Group), value);
        }

        /// <summary>
        /// 访问级别
        /// </summary>
        [EntityField(Description = "访问级别")]
        public OperationAccessLevel AccessLevel
        {
            get => GetPropertyValue<OperationAccessLevel>(nameof(AccessLevel));
            set => SetPropertyValue(nameof(AccessLevel), value);
        }

        /// <summary>
        /// 方法描述
        /// </summary>
        [EntityField(Description = "方法描述")]
        public string Remark
        {
            get => GetPropertyValue<string>(nameof(Remark));
            set => SetPropertyValue(nameof(Remark), value);
        }
    }
}