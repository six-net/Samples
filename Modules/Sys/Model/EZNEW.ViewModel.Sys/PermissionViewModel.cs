using System;
using EZNEW.Module.Sys;

namespace EZNEW.ViewModel.Sys
{
    /// <summary>
    /// 权限
    /// </summary>
    public class PermissionViewModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public PermissionType Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public PermissionStatus Status { get; set; }

        /// <summary>
        /// 状态字符串
        /// </summary>
        public string StatusString
        {
            get
            {
                return Status.ToString();
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 权限分组
        /// </summary>
        public PermissionGroupViewModel Group { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}