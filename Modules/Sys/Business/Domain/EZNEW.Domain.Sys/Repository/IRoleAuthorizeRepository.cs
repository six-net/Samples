using EZNEW.Develop.CQuery;
using EZNEW.Framework.Paging;
using EZNEW.Develop.Domain.Repository;
using EZNEW.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Develop.UnitOfWork;

namespace EZNEW.Domain.Sys.Repository
{
    /// <summary>
    /// 角色权限存储
    /// </summary>
    public interface IRoleAuthorizeRepository:IRelationRepository<Role,Authority>
    {
    }
}
