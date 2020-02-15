using EZNEW.Application.Identity.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.ViewModel.Sys.Filter
{
    /// <summary>
    /// 角色筛选数据
    /// </summary>
    public class RoleFilterViewModel
    {
        #region	属性

        /// <summary>
        /// 角色编号
        /// </summary>
        public List<long> SysNos
        {
            get;
            set;
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
        /// 等级
        /// </summary>
        public int? Level
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
        public RoleStatus? Status
        {
            get;
            set;
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? CreateDate
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
        /// 备注信息
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        #endregion

    }
}
