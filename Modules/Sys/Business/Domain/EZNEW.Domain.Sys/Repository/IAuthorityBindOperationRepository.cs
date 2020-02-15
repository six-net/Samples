using EZNEW.Develop.Domain.Repository;
using EZNEW.Develop.UnitOfWork;
using EZNEW.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Repository
{
    /// <summary>
    /// 权限&操作绑定存储
    /// </summary>
    public interface IAuthorityBindOperationRepository: IRelationRepository<Authority,AuthorityOperation>
    {
        //#region 保存授权操作时修改绑定的权限

        //void SaveAuthorityOperation(IEnumerable<AuthorityOperation> options);

        //#endregion

        //#region 删除授权操作时删除绑定的权限

        ///// <summary>
        ///// 删除授权操作时删除绑定的权限
        ///// </summary>
        ///// <param name="operations">授权操作</param>
        //void DeleteBindAuthorityByOperation(IEnumerable<AuthorityOperation> operations);

        //#endregion

        //#region 删除权限数据时删除绑定的权限

        ///// <summary>
        ///// 删除权限数据时删除绑定的权限
        ///// </summary>
        ///// <param name="authoritys">权限操作</param>
        //void DeleteBindOperationByAuthority(IEnumerable<Authority> authoritys);

        //#endregion

        //#region 权限绑定操作

        ///// <summary>
        ///// 权限绑定操作
        ///// </summary>
        ///// <param name="binds">绑定信息</param>
        //void Bind(IEnumerable<Tuple<Authority, AuthorityOperation>> binds);

        //#endregion

        //#region 解绑权限绑定的操作

        ///// <summary>
        ///// 解绑权限绑定的操作
        ///// </summary>
        ///// <param name="binds">绑定信息</param>
        //void UnBind(IEnumerable<Tuple<Authority, AuthorityOperation>> binds);

        //#endregion
    }
}
