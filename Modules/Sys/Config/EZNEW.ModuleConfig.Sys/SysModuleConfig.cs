using AutoMapper;
using EZNEW.Data;
using EZNEW.Develop.Entity;
using EZNEW.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.ModuleConfig.Sys
{
    public static class SysModuleConfig
    {
        public static void Init(ref Action<IMapperConfigurationExpression> configuration)
        {
            //对象转换
            if (configuration == null)
            {
                configuration = new Action<IMapperConfigurationExpression>(SysModuleObjectMapper.Config);
            }
            else
            {
                configuration += SysModuleObjectMapper.Config;
            }
            //资源仓储配置
            SysModuleRepositoryConfig.Init();
            //对象标识符生成配置
            SysIdentityKeyConfig.Config();
            //领域事件配置
            DomainEventConfig.Init();
            //数据库配置
            DbConfig();
        }

        static void DbConfig()
        {
            #region 数据库特殊字段映射(当使用Oracle数据时使用)

            DataManager.ConfigEntityFieldsToServerType(ServerType.Oracle, typeof(RoleEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Level_",
                    PropertyName="Level"
                }
            });
            DataManager.ConfigEntityFieldsToServerType(ServerType.Oracle, typeof(AuthorityGroupEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Level_",
                    PropertyName="Level"
                }
            });
            DataManager.ConfigEntityFieldsToServerType(ServerType.Oracle, typeof(AuthorityOperationGroupEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Level_",
                    PropertyName="Level"
                }
            });
            DataManager.ConfigEntityFieldsToServerType(ServerType.Oracle, typeof(AuthorityOperationEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Group_",
                    PropertyName="Group"
                }
            });
            DataManager.ConfigEntityFieldsToServerType(ServerType.Oracle, typeof(AuthorityEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Group_",
                    PropertyName="Group"
                }
            });

            #endregion
        }
    }
}
