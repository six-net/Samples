using System;
using System.Collections.Generic;
using EZNEW.Develop.Entity;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 已授权的操作
    /// </summary>
    [Serializable]
    [Entity("Sys_PermissionOperation", "Sys", "已授权的操作")]
    public class PermissionOperationEntity : BaseEntity<PermissionOperationEntity>
    {
        /// <summary>
        /// 操作功能
        /// </summary>
        [EntityField(Description = "操作功能", PrimaryKey = true)]
        [EntityRelation(typeof(OperationEntity), nameof(OperationEntity.Id))]
        public long OperationId
        {
            get => GetPropertyValue<long>(nameof(OperationId));
            set => SetPropertyValue(nameof(OperationId), value);
        }

        /// <summary>
        /// 权限
        /// </summary>
        [EntityField(Description = "权限", PrimaryKey = true)]
        [EntityRelation(typeof(PermissionEntity), nameof(PermissionEntity.Id))]
        public long PermissionId
        {
            get => GetPropertyValue<long>(nameof(PermissionId));
            set => SetPropertyValue(nameof(PermissionId), value);
        }
    }
}