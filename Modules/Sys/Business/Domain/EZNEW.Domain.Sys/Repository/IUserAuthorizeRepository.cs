using EZNEW.Develop.Domain.Repository;
using EZNEW.Domain.Sys.Model;

namespace EZNEW.Domain.Sys.Repository
{
    /// <summary>
    /// 用户授权存储
    /// </summary>
    public interface IUserAuthorizeRepository : IAggregationRelationRepository<UserAuthorize, User, Authority>
    {
    }
}
