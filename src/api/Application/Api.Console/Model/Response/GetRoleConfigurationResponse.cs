using EZNEW.Model;

namespace Api.Console.Model.Response
{
    /// <summary>
    /// 角色配置
    /// </summary>
    public class GetRoleConfigurationResponse
    {
        /// <summary>
        /// 角色状态
        /// </summary>
        public KeyValueCollection<int, string> StatusCollection { get; set; }
    }
}
