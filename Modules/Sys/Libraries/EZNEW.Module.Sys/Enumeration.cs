using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EZNEW.Module.Sys
{
    #region 模块对象类型

    /// <summary>
    /// 模块对象类型
    /// </summary>
    public enum SysModuleObject
    {
        Role = 110,
        User = 111,
        PermissionGroup = 112,
        Permission = 113,
        OperationGroup = 114,
        Operation = 115
    }

    #endregion

    #region 权限

    /// <summary>
    /// 权限状态
    /// </summary>
    public enum PermissionStatus
    {
        [Display(Name = "启用")]
        Enable = 310,
        [Display(Name = "禁用")]
        Disabled = 320
    }

    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PermissionType
    {
        [Display(Name = "管理权限")]
        Management = 410
    }

    #endregion

    #region 操作功能

    /// <summary>
    /// 操作功能状态
    /// </summary>
    public enum OperationStatus
    {
        [Display(Name = "启用")]
        Enable = 310,
        [Display(Name = "关闭")]
        Closed = 320
    }

    /// <summary>
    /// 操作功能访问限制级别
    /// </summary>
    public enum OperationAccessLevel
    {
        [Display(Name = "公开")]
        Public = 510,
        [Display(Name = "授权")]
        Authorized = 520
    }

    #endregion

    #region 账户

    /// <summary>
    /// 账户类型
    /// </summary>
    public enum UserType
    {
        [Display(Name = "管理")]
        Management = 210
    }

    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        [Display(Name = "启用")]
        Enable = 310,
        [Display(Name = "锁定")]
        Locked = 320
    }

    #endregion

    #region 角色

    /// <summary>
    /// 角色状态
    /// </summary>
    public enum RoleStatus
    {
        [Display(Name = "启用")]
        Enable = 310,
        [Display(Name = "禁用")]
        Disabled = 320
    }

    #endregion
}
