using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 删除权限信息
    /// </summary>
    public class RemovePermissionDto
    {
        /// <summary>
        /// 要删除的权限编号
        /// </summary>
        public IEnumerable<long> Ids { get; set; }
    }
}
