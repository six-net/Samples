using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 验证权限名称
    /// </summary>
    public class ExistAuthorityNameCmdDto
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 系统编号
        /// </summary>
        public long SysNo
        {
            get; set;
        }
    }
}
