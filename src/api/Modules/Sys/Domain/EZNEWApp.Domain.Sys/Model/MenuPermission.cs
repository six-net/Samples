using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Development.Entity;

namespace EZNEWApp.Domain.Sys.Model
{
    /// <summary>
    /// 菜单授权
    /// </summary>
    [Serializable]
    [Entity(ObjectName = "Sys_MenuPermission", Group = "Sys", Description = "菜单授权")]
    public class MenuPermission : ModelEntity<MenuPermission>
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        [EntityField(Description = "菜单编号", Role = FieldRole.PrimaryKey)]
        [EntityRelationField(typeof(Menu), nameof(Menu.Id), RelationBehavior.CascadingRemove)]
        public long MenuId { get; set; }

        /// <summary>
        /// 权限编号
        /// </summary>
        [EntityField(Description = "权限编号", Role = FieldRole.PrimaryKey)]
        [EntityRelationField(typeof(Permission), nameof(Permission.Id), RelationBehavior.CascadingRemove)]
        public long PermissionId { get; set; }
    }
}
