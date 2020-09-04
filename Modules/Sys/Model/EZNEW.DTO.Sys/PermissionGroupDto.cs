namespace EZNEW.DTO.Sys
{
    /// <summary>
    /// 权限分组
    /// </summary>
    public class PermissionGroupDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 上级分组
        /// </summary>
        public PermissionGroupDto Parent { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
    }
}
