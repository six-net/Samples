using System;
using EZNEW.Module.Sys;

namespace EZNEW.ViewModel.Sys
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleViewModel
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public RoleStatus Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName => Status.GetEnumDisplayName();

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
    }
}