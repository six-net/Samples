using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改用户授权信息
    /// </summary>
    public class ModifyUserAuthorizeCmdDto
    {
        /// <summary>
        /// 用户授权信息
        /// </summary>
        public IEnumerable<UserAuthorizeCmdDto> UserAuthorizes
        {
            get;set;
        }
    }
}
