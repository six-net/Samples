using EZNEW.Domain.Sys.Model;
using EZNEW.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Framework.Extension;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Develop.Domain.Repository;

namespace EZNEW.Repository.Sys
{
    /// <summary>
    /// 用户存储操作
    /// </summary>
    public class UserRepository : DefaultAggregationRepository<User, UserEntity, IUserDataAccess>, IUserRepository
    {
    }
}
