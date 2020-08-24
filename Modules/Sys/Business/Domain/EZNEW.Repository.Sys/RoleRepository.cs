using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Entity.Sys;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Develop.Domain.Repository;
using EZNEW.Develop.CQuery;

namespace EZNEW.Repository.Sys
{
    /// <summary>
    /// 角色资源存储
    /// </summary>
    public class RoleRepository : DefaultAggregationRepository<Role, RoleEntity, IRoleDataAccess>, IRoleRepository
    {
    }
}
