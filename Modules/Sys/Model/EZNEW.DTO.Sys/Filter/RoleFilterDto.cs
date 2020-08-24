using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Paging;

namespace EZNEW.DTO.Sys.Filter
{
    /// <summary>
    /// 角色信息筛选
    /// </summary>
    public class RoleFilterDto : PagingFilter
    {
        #region	数据筛选

        /// <summary>
        /// 角色编号
        /// </summary>
        public List<long> Ids { get; set; }

        /// <summary>
        /// 要排除的信息
        /// </summary>
        public List<long> ExcludeIds { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 名称匹配关键字
        /// </summary>
        public string NameMateKey { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int? Level { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public long? Parent { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 查询上级角色
        /// </summary>
        public bool QuerySuperiorRole { get; set; }

        #endregion

        #region 数据加载

        /// <summary>
        /// 加载上级
        /// </summary>
        public bool LoadParent
        {
            get; set;
        }

        #endregion

        #region 筛选条件转换

        /// <summary>
        /// 筛选条件转换
        /// </summary>
        /// <returns></returns>
        public virtual RoleFilter ConvertToFilter()
        {
            return this.MapTo<RoleFilter>();
        } 

        #endregion
    }
}
