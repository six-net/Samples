using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 初始化操作功能
    /// </summary>
    public class InitializeOperationDto
    {
        /// <summary>
        /// 操作分组
        /// </summary>
        public List<OperationGroupDto> OperationGroups { get; set; }

        /// <summary>
        /// 操作功能
        /// </summary>
        public List<OperationDto> Operations { get; set; }
    }
}
