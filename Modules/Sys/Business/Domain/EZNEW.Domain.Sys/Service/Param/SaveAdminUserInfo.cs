using EZNEW.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Service.Param
{
    /// <summary>
    /// 保存管理用户信息
    /// </summary>
    public class SaveAdminUserInfo
    {
        #region 属性

        /// <summary>
        /// 用户基础信息
        /// </summary>
        public User User
        {
            get; set;
        }

        /// <summary>
        /// 要移除的账户角色
        /// </summary>
        public List<Role> RemoveRoleList
        {
            get; set;
        }

        /// <summary>
        /// 新添加的账户角色
        /// </summary>
        public List<Role> NewRoleList
        {
            get; set;
        }

        #endregion
    }
}
