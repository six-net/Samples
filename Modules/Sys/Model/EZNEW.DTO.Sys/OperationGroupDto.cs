namespace EZNEW.DTO.Sys
{
    /// <summary>
    /// 操作分组信息
    /// </summary>
    public class OperationGroupDto
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
        /// 上级
        /// </summary>
        public OperationGroupDto Parent { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
    }
}
