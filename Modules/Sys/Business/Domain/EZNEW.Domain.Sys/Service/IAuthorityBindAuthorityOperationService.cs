using EZNEW.Domain.Sys.Service.Param;
using EZNEW.Framework.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Domain.Sys.Service
{
    /// <summary>
    /// 权限&授权操作绑定操作服务
    /// </summary>
    public interface IAuthorityBindAuthorityOperationService
    {
        #region 修改权限&操作绑定

        /// <summary>
        /// 修改权限&操作绑定
        /// </summary>
        /// <param name="bindInfo">绑定信息</param>
        /// <returns></returns>
        Result ModifyAuthorityAndAuthorityOperationBind(ModifyAuthorityAndAuthorityOperationBind bindInfo);

        #endregion
    }
}
