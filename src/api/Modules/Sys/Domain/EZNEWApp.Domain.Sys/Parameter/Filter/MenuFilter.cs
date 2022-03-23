using System;
using System.Collections.Generic;
using EZNEW.Development.Query;
using EZNEW.Development.Domain;
using EZNEW.Paging;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Module.Sys;

namespace EZNEWApp.Domain.Sys.Parameter.Filter
{
    /// <summary>
    /// 操作功能筛选
    /// </summary>
    public class MenuFilter : PagingFilter, IDomainParameter
    {
        #region 筛选条件

		
		/// <summary>
		/// 编号
		/// </summary>
		public List<long> Ids { get; set; }
		/// <summary>
		/// 排除编号
		/// </summary>
		public List<long> ExcludeIds { get; set; }
		
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 地址
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// 组件
		/// </summary>
		public string Component { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		public MenuStatus? Status { get; set; }

		/// <summary>
		/// 激活的菜单项
		/// </summary>
		public long? ActiveMenu { get; set; }

		/// <summary>
		/// 导航跳转
		/// </summary>
		public string Redirect { get; set; }

		/// <summary>
		/// 是否显示根解点
		/// </summary>
		public bool? AlwaysShow { get; set; }

		/// <summary>
		/// 是否固定不允许删除
		/// </summary>
		public bool? Affix { get; set; }

		/// <summary>
		/// 小红框提示内容
		/// </summary>
		public string Badge { get; set; }

		/// <summary>
		/// 是否在面包屑中显示
		/// </summary>
		public bool? BreadCrumb { get; set; }

		/// <summary>
		/// 路由图标
		/// </summary>
		public string Icon { get; set; }

		/// <summary>
		/// 路由小图标
		/// </summary>
		public string RemixIcon { get; set; }

		/// <summary>
		/// 是否不缓存路由
		/// </summary>
		public bool? NoKeepAlive { get; set; }

		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTimeOffset? CreationTime { get; set; }

		/// <summary>
		/// UpdateTime
		/// </summary>
		public DateTimeOffset? UpdateTime { get; set; }


        #endregion

        #region 根据筛选条件创建查询对象

        /// <summary>
        /// 根据筛选条件创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            var query = base.CreateQuery() ?? QueryManager.Create<Menu>(this);

            #region 数据筛选
				
            if(!Ids.IsNullOrEmpty())
            {
                query.In<Menu>(c => c.Id, Ids);
            }
            if (!ExcludeIds.IsNullOrEmpty())
            {
                query.NotIn<Menu>(c => c.Id, ExcludeIds);
            }
							
            if(!string.IsNullOrWhiteSpace(Name))
            {
                query.And<Menu>(c => c.Name == Name);
            }
							
            if(!string.IsNullOrWhiteSpace(Title))
            {
                query.And<Menu>(c => c.Title == Title);
            }
							
            if(!string.IsNullOrWhiteSpace(Path))
            {
                query.And<Menu>(c => c.Path == Path);
            }
							
            if(!string.IsNullOrWhiteSpace(Component))
            {
                query.And<Menu>(c => c.Component == Component);
            }
							
            if(Status.HasValue)
            {
                query.And<Menu>(c => c.Status == Status.Value);
            }
							
            if(ActiveMenu.HasValue)
            {
                query.And<Menu>(c => c.ActiveMenu == ActiveMenu.Value);
            }
							
            if(!string.IsNullOrWhiteSpace(Redirect))
            {
                query.And<Menu>(c => c.Redirect == Redirect);
            }
							
            if(AlwaysShow.HasValue)
            {
                query.And<Menu>(c => c.AlwaysShow == AlwaysShow.Value);
            }
							
            if(Affix.HasValue)
            {
                query.And<Menu>(c => c.Affix == Affix.Value);
            }
							
            if(!string.IsNullOrWhiteSpace(Badge))
            {
                query.And<Menu>(c => c.Badge == Badge);
            }
							
            if(BreadCrumb.HasValue)
            {
                query.And<Menu>(c => c.BreadCrumb == BreadCrumb.Value);
            }
							
            if(!string.IsNullOrWhiteSpace(Icon))
            {
                query.And<Menu>(c => c.Icon == Icon);
            }
							
            if(!string.IsNullOrWhiteSpace(RemixIcon))
            {
                query.And<Menu>(c => c.RemixIcon == RemixIcon);
            }
							
            if(NoKeepAlive.HasValue)
            {
                query.And<Menu>(c => c.NoKeepAlive == NoKeepAlive.Value);
            }
							
            if(CreationTime.HasValue)
            {
                query.And<Menu>(c => c.CreationTime == CreationTime.Value);
            }
							
            if(UpdateTime.HasValue)
            {
                query.And<Menu>(c => c.UpdateTime == UpdateTime.Value);
            }
					
            #endregion

            return query;
        } 

        #endregion
    }
}