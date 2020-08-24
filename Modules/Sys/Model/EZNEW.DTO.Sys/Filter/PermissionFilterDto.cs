using System;
using System.Collections.Generic;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Module.Sys;
using EZNEW.Paging;

namespace EZNEW.DTO.Sys.Filter
{
    /// <summary>
    /// 权限筛选信息
    /// </summary>
    public class PermissionFilterDto : PagingFilter
    {
        #region	数据筛选

        /// <summary>
        /// 编号
        /// </summary>
        public List<long> Ids { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public List<string> Codes { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public PermissionType? Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public PermissionStatus? Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 权限分组
        /// </summary>
        public long? Group { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 名称/编码关键字
        /// </summary>
        public string NameCodeMateKey { get; set; }

        #endregion

        #region 数据加载

        /// <summary>
        /// 加载分组
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
        public virtual PermissionFilter ConvertToFilter()
        {
            return this.MapTo<PermissionFilter>();
        }

        #endregion
    }
}
