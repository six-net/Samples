using EZNEW.Model;

namespace Api.Console.Model.Response
{
    /// <summary>
    /// 功能操作配置信息
    /// </summary>
    public class GetOperationConfigurationResponse
    {
        /// <summary>
        /// 功能操作状态
        /// </summary>
        public KeyValueCollection<int, string> StatusCollection { get; set; }

        /// <summary>
        /// 功能操作访问级别
        /// </summary>
        public KeyValueCollection<int, string> AccessLevelCollection { get; set; }
    }
}
