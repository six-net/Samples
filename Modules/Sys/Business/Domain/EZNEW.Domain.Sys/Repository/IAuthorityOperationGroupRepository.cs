using EZNEW.Develop.CQuery;
using EZNEW.Develop.Domain.Repository;
using EZNEW.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Repository
{
    /// <summary>
    /// 授权操作组存储
    /// </summary>
    public interface IAuthorityOperationGroupRepository : IAggregationRepository<AuthorityOperationGroup>
    {
    }
}
