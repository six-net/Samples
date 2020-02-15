using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Entity.Sys;
using EZNEW.Framework.Extension;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Develop.Domain.Repository;

namespace EZNEW.Repository.Sys
{
    /// <summary>
    /// 授权操作组存储
    /// </summary>
    public class AuthorityOperationGroupRepository : DefaultAggregationRepository<AuthorityOperationGroup, AuthorityOperationGroupEntity, IAuthorityOperationGroupDataAccess>, IAuthorityOperationGroupRepository
    {
    }
}
