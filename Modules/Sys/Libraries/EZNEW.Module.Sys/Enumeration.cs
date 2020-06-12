using System.ComponentModel.DataAnnotations;

namespace EZNEW.Module.Sys
{
    /// <summary>
    /// 模块对象类型
    /// </summary>
    public enum SysModuleObject
    {
        Role = 110,
        User = 111,
        AuthorityGroup = 112,
        Authority = 113,
        AuthorityOperationGroup = 114,
        AuthorityOperation = 115
    }

    #region 权限组

    /// <summary>
    /// 权限分组状态
    /// </summary>
    public enum AuthorityGroupStatus
    {
        启用 = 310,
        关闭 = 320
    }

    #endregion

    #region 权限

    /// <summary>
    /// 权限状态
    /// </summary>
    public enum AuthorityStatus
    {
        启用 = 310,
        关闭 = 320
    }

    /// <summary>
    /// 权限类型
    /// </summary>
    public enum AuthorityType
    {
        管理 = 410
    }

    #endregion

    #region 授权操作分组

    /// <summary>
    /// 授权操作分组状态
    /// </summary>
    public enum AuthorityOperationGroupStatus
    {
        启用 = 310,
        关闭 = 320
    }

    #endregion

    #region 授权操作

    /// <summary>
    /// 授权操作状态
    /// </summary>
    public enum AuthorityOperationStatus
    {
        启用 = 310,
        关闭 = 320
    }

    /// <summary>
    /// 授权操作请求方式
    /// </summary>
    public enum AuthorityOperationMethod
    {
        全部 = 410,
        GET = 420,
        POST = 430
    }

    /// <summary>
    /// 授权操作类型
    /// </summary>
    public enum AuthorityOperationAuthorizeType
    {
        无限制 = 510,
        权限授权 = 520
    }

    #endregion

    #region 授权

    /// <summary>
    /// 授权对象类型
    /// </summary>
    public enum AuthorizeType
    {
        权限组 = 410,
        权限 = 420
    }

    #endregion

    #region 账户

    /// <summary>
    /// 账户类型
    /// </summary>
    public enum UserType
    {
        管理账户 = 210
    }

    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        正常 = 310,
        锁定 = 320
    }

    #endregion

    #region 角色

    /// <summary>
    /// 角色状态
    /// </summary>
    public enum RoleStatus
    {
        正常 = 310,
        禁用 = 320
    }

    #endregion
}
