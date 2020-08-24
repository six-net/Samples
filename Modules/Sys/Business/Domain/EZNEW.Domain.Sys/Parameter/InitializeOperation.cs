using EZNEW.Develop.Domain;
using EZNEW.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Domain.Sys.Parameter
{
    /// <summary>
    /// 初始化
    /// </summary>
    public class InitializeOperation : IDomainParameter
    {
        /// <summary>
        /// 操作分组
        /// </summary>
        public List<OperationGroup> OperationGroups { get; set; }

        /// <summary>
        /// 操作功能
        /// </summary>
        public List<Operation> Operations { get; set; }
    }
}
