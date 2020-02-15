using EZNEW.Domain.Sys.Repository;
using EZNEW.Domain.Sys.Service.Param;
using EZNEW.Framework;
using EZNEW.Framework.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Framework.Extension;
using EZNEW.Framework.Response;
using EZNEW.Develop.UnitOfWork;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 权限&授权操作绑定操作
    /// </summary>
    public class AuthorityBindAuthorityOperationService : IAuthorityBindAuthorityOperationService
    {
        static readonly IAuthorityBindOperationRepository bindRepository = ContainerManager.Container.Resolve<IAuthorityBindOperationRepository>();

        #region 修改权限&操作绑定

        /// <summary>
        /// 修改权限&操作绑定
        /// </summary>
        /// <param name="bindInfo">绑定信息</param>
        /// <returns></returns>
        public Result ModifyAuthorityAndAuthorityOperationBind(ModifyAuthorityAndAuthorityOperationBind bindInfo)
        {
            if (bindInfo == null || (bindInfo.Binds.IsNullOrEmpty() && bindInfo.UnBinds.IsNullOrEmpty()))
            {
                return Result.FailedResult("没有指定任何要修改的信息");
            }
            //解绑
            if (!bindInfo.UnBinds.IsNullOrEmpty())
            {
                bindRepository.Remove(bindInfo.UnBinds);
            }
            //绑定
            if (!bindInfo.Binds.IsNullOrEmpty())
            {
                bindRepository.Remove(bindInfo.Binds, new ActivationOption()
                {
                    ForceExecute = true
                });
                bindRepository.Save(bindInfo.Binds);
            }
            return Result.SuccessResult("修改成功");
        }

        #endregion
    }
}
