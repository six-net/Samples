using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Development.Entity;
using EZNEWApp.Module.Sys;

namespace EZNEWApp.Domain.Sys.Model
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Serializable]
    [Entity(ObjectName = "Sys_Menu", Group = "Sys", Description = "菜单")]
    public class Menu : ModelRecordEntity<Menu>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [EntityField(Description = "编号", Role = FieldRole.PrimaryKey)]
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [EntityField(Description = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [EntityField(Description = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [EntityField(Description = "地址")]
        public string Path { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        [EntityField(Description = "组件")]
        public string Component { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [EntityField(Description = "状态")]
        public MenuStatus Status { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        [EntityField(Description = "用途")]
        public MenuUsage Usage { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        [EntityField(Description = "上级菜单")]
        public long Parent { get; set; }

        /// <summary>
        /// 激活的菜单项
        /// </summary>
        [EntityField(Description = "激活的菜单项")]
        public long ActiveMenu { get; set; }

        /// <summary>
        /// 导航跳转
        /// </summary>
        [EntityField(Description = "导航跳转")]
        public string Redirect { get; set; }

        /// <summary>
        /// 是否显示根解点
        /// </summary>
        [EntityField(Description = "是否显示根解点")]
        public bool AlwaysShow { get; set; }

        /// <summary>
        /// 是否固定不允许删除
        /// </summary>
        [EntityField(Description = "是否固定不允许删除")]
        public bool Affix { get; set; }

        /// <summary>
        /// 小红框提示内容
        /// </summary>
        [EntityField(Description = "小红框提示内容")]
        public string Badge { get; set; }

        /// <summary>
        /// 是否在面包屑中显示
        /// </summary>
        [EntityField(Description = "是否在面包屑中显示")]
        public bool BreadCrumb { get; set; }

        /// <summary>
        /// 路由图标
        /// </summary>
        [EntityField(Description = "路由图标")]
        public string Icon { get; set; }

        /// <summary>
        /// 路由小图标
        /// </summary>
        [EntityField(Description = "路由小图标")]
        public string RemixIcon { get; set; }

        /// <summary>
        /// 是否不缓存路由
        /// </summary>
        [EntityField(Description = "是否不缓存路由")]
        public bool NoKeepAlive { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [EntityField(Description = "排序")]
        public int Sequence { get; set; }
    }
}
