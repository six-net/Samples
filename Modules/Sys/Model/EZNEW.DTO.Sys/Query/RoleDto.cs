using EZNEW.Module.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Query
{
    /// <summary>
    /// 角色对象
    /// </summary>
    public class RoleDto
    {
        #region	属性

        /// <summary>
        /// 角色编号
        /// </summary>
        public long SysNo
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
        public int Level
        {
            get;
            set;
        }

        /// <summary>
        /// 上级
        /// </summary>
        public RoleDto Parent
        {
            get;
            set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public RoleStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateDate
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
