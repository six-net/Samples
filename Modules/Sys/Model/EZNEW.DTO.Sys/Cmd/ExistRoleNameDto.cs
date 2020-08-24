using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 验证角色名是否存在
    /// </summary>
    public class ExistRoleNameDto
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 验证角色名称时需要排除的角色编号
        /// </summary>
        public long ExcludeId { get; set; }
    }
}
