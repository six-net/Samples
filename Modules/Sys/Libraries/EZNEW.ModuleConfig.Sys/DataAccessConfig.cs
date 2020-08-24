using EZNEW.Data;
using EZNEW.Develop.DataAccess.Event;
using EZNEW.Develop.Entity;
using EZNEW.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.ModuleConfig.Sys
{
    /// <summary>
    /// 数据访问配置
    /// </summary>
    internal static class DataAccessConfig
    {
        internal static void Configure()
        {
            //配置Oracle数据库访问
            ConfigureOracle();
            //配置数据访问事件
            //ConfigureDataAccessEvent();
        }

        /// <summary>
        /// Oracle数据访问
        /// </summary>
        static void ConfigureOracle()
        {
            #region 数据库特殊字段映射(当使用Oracle数据时使用)

            DataManager.ConfigureEntityFields(DatabaseServerType.Oracle, typeof(RoleEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Level_",
                    PropertyName="Level"
                }
            });
            DataManager.ConfigureEntityFields(DatabaseServerType.Oracle, typeof(PermissionGroupEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Level_",
                    PropertyName="Level"
                }
            });
            DataManager.ConfigureEntityFields(DatabaseServerType.Oracle, typeof(OperationGroupEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Level_",
                    PropertyName="Level"
                }
            });
            DataManager.ConfigureEntityFields(DatabaseServerType.MySQL, typeof(OperationGroupEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Level123",
                    PropertyName="Level"
                }
            });
            DataManager.ConfigureEntityFields(DatabaseServerType.SQLServer, typeof(OperationGroupEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Level_sqlserver",
                    PropertyName="Level"
                }
            });
            DataManager.ConfigureEntityFields(DatabaseServerType.Oracle, typeof(OperationEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Group_",
                    PropertyName="Group"
                }
            });
            DataManager.ConfigureEntityFields(DatabaseServerType.Oracle, typeof(PermissionEntity), new List<EntityField>()
            {
                new EntityField()
                {
                    FieldName="Group_",
                    PropertyName="Group"
                }
            });

            #endregion
        }

        /// <summary>
        /// 配置数据访问事件
        /// </summary>
        static void ConfigureDataAccessEvent()
        {
            //all event
            DataAccessEventBus.SubscribeAll(e =>
            {
                Console.WriteLine($"执行了数据操作,事件编号:{e.Id},时间:{e.TriggerDate.ToString()},操作对象:{e.EntityType.FullName},事件类型:{e.GetType().FullName}");
                return DataAccessEventExecuteResult.EmptyResult();
            });

            //add event
            DataAccessEventBus.SubscribeAdd(addEvent =>
            {
                Console.WriteLine($"添加数据:{addEvent.EntityType.FullName}");
                return DataAccessEventExecuteResult.EmptyResult();
            }, true, typeof(UserEntity));

            //update event
            DataAccessEventBus.SubscribeUpdate(updateEvent =>
            {
                Console.WriteLine($"修改数据:{updateEvent.EntityType.FullName},修改值:{string.Join(",", updateEvent.NewValues.Keys)}");
                return DataAccessEventExecuteResult.EmptyResult();
            });

            //modify expression
            DataAccessEventBus.SubscribeModifyExpression(e =>
            {
                Console.WriteLine($"修改表达式事件:{e.EntityType.FullName},修改值:{string.Join(",", e.ModifyValues.Keys)}");
                return DataAccessEventExecuteResult.EmptyResult();
            });

            //delete event
            DataAccessEventBus.SubscribeDelete(e =>
            {
                Console.WriteLine($"删除数据:{e.EntityType.FullName},对象标识值:{string.Join(",", e.KeyValues.Keys)}");
                return DataAccessEventExecuteResult.EmptyResult();
            });

            //delete by condition
            DataAccessEventBus.SubscribeDeleteByCondition(e =>
            {
                Console.WriteLine($"根据条件删除数据:{e.EntityType.FullName},条件:{string.Join(",", e.Query.AllConditionFieldNames)}");
                return DataAccessEventExecuteResult.EmptyResult();
            });

            //query event
            DataAccessEventBus.SubscribeQuery(e =>
            {
                Console.WriteLine($"查询数据:{e.EntityType.FullName},条件:{string.Join(",", e.Query.AllConditionFieldNames)}");
                return DataAccessEventExecuteResult.EmptyResult();
            });

            //check value
            DataAccessEventBus.SubscribeCheckData(e =>
            {
                Console.WriteLine($"查询数据是否存在:{e.EntityType.FullName},是否拥有值:{e.HasValue}");
                return DataAccessEventExecuteResult.EmptyResult();
            });

            //agg func
            DataAccessEventBus.SubscribeAggregateFunction(e =>
            {
                Console.WriteLine($"执行聚合函数:{e.EntityType.FullName},函数:{e.OperateType},返回值:{e.Value}");
                return DataAccessEventExecuteResult.EmptyResult();
            });

            DataAccessEventBus.Subscribe<QueryDataEvent>(new CustomQueryHandler());
        }
    }

    public class CustomQueryHandler : IDataAccessEventHandler
    {
        public DataAccessEventExecuteResult Execute(IDataAccessEvent dataAccessEvent)
        {
            Console.WriteLine($"自定义查询事件处理程序");
            return DataAccessEventExecuteResult.EmptyResult();
        }
    }
}
