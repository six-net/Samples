using EZNEW.Module.Sys;
using EZNEW.Paging;
using System.Collections.Generic;

namespace EZNEW.DTO.Sys.Query.Filter
{
    /// <summary>
    /// 授权操作分组筛选信息
    /// </summary>
    public class AuthorityOperationGroupFilterDto: PagingFilter
    {
        #region	属性

        /// <summary>
        /// 编号
        /// </summary>
        public List<long> SysNos
        {
            get;
            set;
        }

        /// <summary>
        /// 排除数据
        /// </summary>
        public List<long> ExcludeSysNos
        {
            get;set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 上级
        /// </summary>
        public long? Parent
        {
            get;
            set;
        }

        /// <summary>
        /// 等级
        /// </summary>
        public int? Level
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public AuthorityOperationGroupStatus? Status
        {
            get;
            set;
        }

        /// <summary>
        /// 所属应用
        /// </summary>
        public string Application
        {
            get;
            set;
        }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        #endregion
    }
}
