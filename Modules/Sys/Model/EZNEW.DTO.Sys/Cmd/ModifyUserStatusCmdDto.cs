using EZNEW.Application.Identity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改用户状态
    /// </summary>
    public class ModifyUserStatusCmdDto
    {
        #region 属性

        /// <summary>
        /// 用户状态
        /// </summary>
        public UserStatus Status
        {
            get;set;
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserId
        {
            get;set;
        }

        #endregion
    }
}
