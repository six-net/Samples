using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改用户绑定角色信息
    /// </summary>
    public class ModifyUserBindRoleCmdDto
    {
        /// <summary>
        /// 绑定信息
        /// </summary>
        public IEnumerable<Tuple<UserCmdDto, RoleCmdDto>> Binds
        {
            get;set;
        }

        /// <summary>
        /// 解绑信息
        /// </summary>
        public IEnumerable<Tuple<UserCmdDto, RoleCmdDto>> UnBinds
        {
            get;set;
        }
    }
}
