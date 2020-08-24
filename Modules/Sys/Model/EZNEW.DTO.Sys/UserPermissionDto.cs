namespace EZNEW.DTO.Sys
{
    /// <summary>
    /// 用户授权信息
    /// </summary>
    public class UserPermissionDto
    {
        /// <summary>
        /// 用户
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public long PermissionId { get; set; }

        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disable { get; set; }
    }
}
