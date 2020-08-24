using EZNEW.Develop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改用户密码信息
    /// </summary>
    public class ModifyUserPassword : IDomainParameter
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 当前密码
        /// </summary>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// 是否验证现有密码
        /// </summary>
        public bool CheckCurrentPassword { get; set; }
    }
}
