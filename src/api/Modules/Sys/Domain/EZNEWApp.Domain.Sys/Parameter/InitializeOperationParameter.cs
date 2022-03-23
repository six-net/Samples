using EZNEW.Development.Domain;
using EZNEWApp.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 初始化
    /// </summary>
    public class InitializeOperationParameter : IDomainParameter
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
