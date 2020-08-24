using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 验证权限分组名称是否存在
    /// </summary>
    public class ExistPermissionGroupNameDto
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 验证分组名称时需要排除的分组编号
        /// </summary>
        public long ExcludeId { get; set; }
    }
}
