using System;
using EZNEW.Develop.Domain.Aggregation;
using EZNEW.Domain.Sys.Repository;
using EZNEW.ValueType;
using EZNEW.Develop.CQuery;
using EZNEW.Entity.Sys;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 用户授权
    /// </summary>
    public class UserPermission : AggregationRoot<UserPermission>
    {
        #region 构造方法

        /// <summary>
        /// 实例化用户授权对象
        /// </summary>
        internal UserPermission()
        {
            repository = this.Instance<IUserPermissionRepository>();
        }

        #endregion

        #region	属性

        /// <summary>
        /// 用户
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public long PermissionId { get; set; }

        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disable { get; set; }

        #endregion

        #region 内部方法

        #region 标识值是否为空

        /// <summary>
        /// 主标识值是否为空
        /// </summary>
        /// <returns></returns>
        public override bool IdentityValueIsNone()
        {
            return UserId < 1 || PermissionId < 1;
        }

        #endregion

        #region 获取标识值

        /// <summary>
        /// 获取标识值
        /// </summary>
        /// <returns>返回标识值</returns>
        protected override string GetIdentityValue()
        {
            return $"{UserId}_{PermissionId}";
        }

        #endregion

        #endregion

        #region 静态方法

        #region 创建用户授权

        /// <summary>
        /// 创建一个用户授权对象
        /// </summary>
        /// <param name="userId">用户</param>
        /// <param name="permissionId">权限</param>
        /// <param name="disable">禁用权限</param>
        /// <returns></returns>
        public static UserPermission Create(long userId, long permissionId, bool disable = false)
        {
            return new UserPermission()
            {
                UserId = userId,
                PermissionId = permissionId,
                Disable = disable
            };
        }

        #endregion

        #endregion

        #region 功能方法

        #endregion
    }
}