using System;
using System.Collections.Generic;
using EZNEW.Data.Modification;
using EZNEW.Development.Entity;
using EZNEW.Development.Query;

namespace EZNEWApp.Domain.Sys.Model
{
    /// <summary>
    /// 操作功能分组
    /// </summary>
    [Serializable]
    [Entity(ObjectName = "Sys_OperationGroup", Group = "Sys", Description = "操作功能分组")]
    public class OperationGroup : ModelRecordEntity<OperationGroup>
    {
        #region 属性

        /// <summary>
        /// 编号
        /// </summary>
        [EntityField(Description = "编号", Role = FieldRole.PrimaryKey)]
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [EntityField(Description = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [EntityField(Description = "排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        [EntityField(Description = "上级")]
        public long Parent { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [EntityField(Description = "说明")]
        public string Remark { get; set; }

        #endregion

        #region 方法

        #region 修改排序

        /// <summary>
        /// 修改排序
        /// </summary>
        /// <param name="newSort">新排序,排序编号不能小于0</param>
        public void ModifySort(int newSort)
        {
            if (newSort < 0)
            {
                throw new Exception("请填写正确的排序号");
            }
            Sort = newSort;
            //同级后面的数据排序顺延
            IQuery sortQuery = QueryManager.Create<OperationGroup>(r => r.Sort >= newSort && r.Parent==Parent);
            IModification modifyExpression = ModificationFactory.Create().Add<OperationGroup>(r => r.Sort, 1);
            Repository.Modify(modifyExpression, sortQuery);
        }

        #endregion

        #endregion
    }
}