using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.ViewModel.Sys.Response
{
    /// <summary>
    /// 授权权限数据
    /// </summary>
    public class AuthorizationAuthorityViewModel : AuthorityViewModel
    {
        /// <summary>
        ///  允许使用
        /// </summary>
        public bool AllowUse
        {
            get; set;
        }
    }
}
