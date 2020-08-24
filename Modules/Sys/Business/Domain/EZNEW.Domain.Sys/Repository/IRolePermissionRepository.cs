using EZNEW.Develop.Domain.Repository;
using EZNEW.Domain.Sys.Model;

namespace EZNEW.Domain.Sys.Repository
{
    /// <summary>
    /// 角色权限存储
    /// </summary>
    public interface IRolePermissionRepository:IRelationRepository<Role,Permission>
    {
    }
}
