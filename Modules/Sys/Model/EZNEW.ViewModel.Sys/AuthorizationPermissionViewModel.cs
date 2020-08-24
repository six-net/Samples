using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.ViewModel.Sys
{
    /// <summary>
    /// 授权权限数据
    /// </summary>
    public class AuthorizationPermissionViewModel : PermissionViewModel
    {
        /// <summary>
        ///  允许使用
        /// </summary>
        public bool AllowUse { get; set; }
    }
}
