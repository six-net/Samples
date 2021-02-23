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
        public long Id { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        [EntityField(Description = "控制器", IsRequired = true)]
        public string ControllerCode { get; set; }

        /// <summary>
        /// 操作方法
        /// </summary>
        [EntityField(Description = "操作方法")]
        public string ActionCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [EntityField(Description = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [EntityField(Description = "状态")]
        public int Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [EntityField(Description = "排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 操作分组
        /// </summary>
        [EntityField(Description = "操作分组")]
        [EntityRelation(typeof(OperationGroupEntity), nameof(OperationGroupEntity.Id))]
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
    }
}