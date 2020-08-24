using EZNEW.Develop.Domain.Repository;
using EZNEW.Domain.Sys.Model;

namespace EZNEW.Domain.Sys.Repository
{
    /// <summary>
    /// 权限&操作绑定存储
    /// </summary>
    public interface IPermissionOperationRepository : IRelationRepository<Permission, Operation>
    {
    }
}
