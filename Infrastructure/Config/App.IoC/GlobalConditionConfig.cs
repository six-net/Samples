using EZNEW.Application.Identity.User;
using EZNEW.Develop.CQuery;
using EZNEW.Entity.Sys;
using EZNEW.Query.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.IoC
{
    public class GlobalConditionConfig
    {
        public static void Init()
        {
            QueryManager.GetGlobalCondition = filter =>
            {
                GlobalConditionFilterResult result = new GlobalConditionFilterResult();
                IQuery query = QueryFactory.Create();
                if (filter.EntityType == typeof(UserEntity))
                {
                    switch (filter.UsageScene)
                    {
                        case QueryUsageScene.Query:
                            query.And<RoleQuery>(r => r.Status == RoleStatus.正常);
                            break;
                        case QueryUsageScene.Count:
                            query.And<RoleQuery>(r => r.Name != "Count");
                            break;
                        case QueryUsageScene.Remove:
                            query.And<RoleQuery>(r => r.Name != "Remove");
                            break;
                    }
                }
                result.Condition = query;
                return result;
            };
        }
    }
}
