using EZNEW.Model;

namespace Api.Console.Model.Response
{
    /// <summary>
    /// 权限配置信息
    /// </summary>
    public class GetPermissionConfigurationResponse
    {
        /// <summary>
        /// 权限状态信息
        /// </summary>
        public KeyValueCollection<int, string> StatusCollection { get; set; }
    }
}
