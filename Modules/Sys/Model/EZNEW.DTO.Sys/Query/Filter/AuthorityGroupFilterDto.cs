using EZNEW.Module.Sys;
using EZNEW.Paging;
using System.Collections.Generic;

namespace EZNEW.DTO.Sys.Query.Filter
{
    /// <summary>
    /// 权限分组筛选信息
    /// </summary>
    public class AuthorityGroupFilterDto: PagingFilter
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
        /// 排除的数据
        /// </summary>
        public List<long> ExcludeIds
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
        /// 状态
        /// </summary>
        public AuthorityGroupStatus? Status
        {
            get;
            set;
        }

        /// <summary>
        /// 上级分组
        /// </summary>
        public long? Parent
        {
            get;
            set;
        }

        /// <summary>
        /// 分组等级
        /// </summary>
        public int? Level
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
