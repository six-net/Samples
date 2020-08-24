using System;
using System.Collections.Generic;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Module.Sys;
using EZNEW.Paging;

namespace EZNEW.DTO.Sys.Filter
{
    /// <summary>
    /// 授权操作筛选
    /// </summary>
    public class OperationFilterDto : PagingFilter
    {
        #region	数据筛选

        /// <summary>
        /// 主键编号
        /// </summary>
        public List<long> Ids { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string ControllerCode { get; set; }

        /// <summary>
        /// 操作方法
        /// </summary>
        public string ActionCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public OperationStatus? Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 操作分组
        /// </summary>
        public long? Group { get; set; }

        /// <summary>
        /// 授权类型
        /// </summary>
        public OperationAccessLevel? AccessLevel { get; set; }

        /// <summary>
        /// 方法描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// ControllerCode/ActionCode/Name 匹配关键字
        /// </summary>
        public string OperationMateKey { get; set; }

        #endregion

        #region 数据加载

        /// <summary>
        /// 加载操作分组数据
        /// </summary>
        public bool LoadGroup
        {
            get; set;
        }

        #endregion

        #region 筛选条件转换

        /// <summary>
        /// 筛选条件转换
        /// </summary>
        /// <returns></returns>
        public virtual OperationFilter ConvertToFilter()
        {
            return this.MapTo<OperationFilter>();
        }

        #endregion
    }
}
