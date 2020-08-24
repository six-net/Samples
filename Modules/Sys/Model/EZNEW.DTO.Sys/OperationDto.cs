using EZNEW.Module.Sys;

namespace EZNEW.DTO.Sys
{
    /// <summary>
    /// 授权操作信息对象
    /// </summary>
    public class OperationDto
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string ControllerCode { get; set; }

        /// <summary>
        /// 操作方法
        /// </summary>
        public string ActionCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public OperationStatus Status { get; set; }

        /// <summary>
        /// 访问限制
        /// </summary>
        public OperationAccessLevel AccessLevel { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 操作分组
        /// </summary>
        public OperationGroupDto Group { get; set; }

        /// <summary>
        /// 方法描述
        /// </summary>
        public string Remark { get; set; }
    }
}
