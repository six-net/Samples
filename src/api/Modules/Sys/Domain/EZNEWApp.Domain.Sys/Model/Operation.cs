using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EZNEW.Development.Entity;
using EZNEWApp.Module.Sys;

namespace EZNEWApp.Domain.Sys.Model
{
    /// <summary>
    /// 操作功能
    /// </summary>
    [Serializable]
    [Entity(ObjectName = "Sys_Operation", Group = "Sys", Description = "操作功能")]
    public class Operation : ModelRecordEntity<Operation>
    {
        #region 属性

        /// <summary>
        /// 主键编号
        /// </summary>
        [EntityField(Description = "主键编号", Role = FieldRole.PrimaryKey)]
        public long Id { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        [EntityField(Description = "控制器", IsRequired = true)]
        public string Controller { get; set; }

        /// <summary>
        /// 操作方法
        /// </summary>
        [EntityField(Description = "操作方法")]
        public string Action { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [EntityField(Description = "地址")]
        public string Path { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [EntityField(Description = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [EntityField(Description = "状态")]
        public OperationStatus Status { get; set; } = OperationStatus.Enable;

        /// <summary>
        /// 排序
        /// </summary>
        [EntityField(Description = "排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 操作分组
        /// </summary>
        [EntityField(Description = "操作分组")]
        [EntityRelationField(typeof(OperationGroup), nameof(OperationGroup.Id), RelationBehavior.CascadingRemove)]
        public long Group { get; set; }

        /// <summary>
        /// 访问级别
        /// </summary>
        [EntityField(Description = "访问级别")]
        public OperationAccessLevel AccessLevel { get; set; }

        /// <summary>
        /// 方法描述
        /// </summary>
        [EntityField(Description = "方法描述")]
        public string Remark { get; set; }

        #endregion
    }
}