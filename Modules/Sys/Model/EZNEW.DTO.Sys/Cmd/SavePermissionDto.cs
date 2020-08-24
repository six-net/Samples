using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 保存权限信息
    /// </summary>
    public class SavePermissionDto
    {
        /// <summary>
        /// 权限信息
        /// </summary>
        public PermissionDto Permission { get; set; }
    }
}
