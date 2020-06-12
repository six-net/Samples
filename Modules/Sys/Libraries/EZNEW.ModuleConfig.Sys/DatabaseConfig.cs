using EZNEW.Data;
using EZNEW.Develop.Entity;
using EZNEW.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Module.Sys
{
    /// <summary>
    /// Database config
    /// </summary>
    public static class DatabaseConfig
    {
        public static void Init()
        {
            //针对Oracle数据库配置
            ConfigureOracle();
        }

        #region 针对Oracle数据库配置

        /// <summary>
        /// 针对Oracle数据库访问的配置
        /// </summary>
        static void ConfigureOracle()
        {
            DataManager.ConfigureEntityFields(DatabaseServerType.Oracle, typeof(RoleEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Level_",
                    PropertyName="Level"
                }
            });
            DataManager.ConfigureEntityFields(DatabaseServerType.Oracle, typeof(AuthorityGroupEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Level_",
                    PropertyName="Level"
                }
            });
            DataManager.ConfigureEntityFields(DatabaseServerType.Oracle, typeof(AuthorityOperationGroupEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Level_",
                    PropertyName="Level"
                }
            });
            DataManager.ConfigureEntityFields(DatabaseServerType.Oracle, typeof(AuthorityOperationEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Group_",
                    PropertyName="Group"
                }
            });
            DataManager.ConfigureEntityFields(DatabaseServerType.Oracle, typeof(AuthorityEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Group_",
                    PropertyName="Group"
                }
            });
        }

        #endregion
    }
}
