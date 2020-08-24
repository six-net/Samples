using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 删除权限分组信息
    /// </summary>
    public class RemovePermissionGroupDto
    {
        /// <summary>
        /// 要删除的权限分组编号
        /// </summary>
        public IEnumerable<long> Ids { get; set; }
    }
}
