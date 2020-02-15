using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 保存角色信息
    /// </summary>
    public class SaveRoleCmdDto
    {
        /// <summary>
        /// 角色信息
        /// </summary>
        public RoleCmdDto Role
        {
            get;set;
        }
    }
}
