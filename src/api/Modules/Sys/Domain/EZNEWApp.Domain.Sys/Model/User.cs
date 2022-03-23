using System;
using System.Collections.Generic;
using EZNEW.Development.Entity;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Module.Sys;

namespace EZNEWApp.Domain.Sys.Model
{
    /// <summary>
    /// 管理用户
    /// </summary>
    [Serializable]
    [Entity(ObjectName = "Sys_User", Group = "Sys", Description = "管理用户")]
    public class User : ModelPermanentRecordEntity<User>
    {
        #region 属性

        /// <summary>
        /// 用户编号
        /// </summary>
        [EntityField(Description = "用户编号", Role = FieldRole.PrimaryKey)]
        public long Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [EntityField(Description = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 真实名称
        /// </summary>
        [EntityField(Description = "真实名称")]
        public string RealName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [EntityField(Description = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [EntityField(Description = "类型")]
        public UserType UserType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [EntityField(Description = "状态")]
        public UserStatus Status { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [EntityField(Description = "手机")]
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [EntityField(Description = "邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        [EntityField(Description = "QQ")]
        public string QQ { get; set; }

        /// <summary>
        /// 超级管理员
        /// </summary>
        [EntityField(Description = "超级管理员")]
        public bool SuperUser { get; set; }

        #endregion

        #region 方法

        #region 更新对象时触发

        /// <summary>
        /// 更新对象时触发
        /// </summary>
        /// <param name="newData">新的对象值</param>
        /// <returns>返回更新后的对象</returns>
        protected override User OnUpdating(User newData)
        {
            if (newData != null)
            {
                RealName = newData.RealName;
                if (SuperUser)
                {
                    Status = UserStatus.Enable;
                }
                else
                {
                    Status = newData.Status;
                }
                Mobile = newData.Mobile;
                Email = newData.Email;
                QQ = newData.QQ;
            }
            return this;
        }

        #endregion

        #region 添加对象时触发

        /// <summary>
        /// 添加对象时触发
        /// </summary>
        /// <returns>返回要保存的对象值</returns>
        protected override User OnAdding()
        {
            Password = EncryptPassword(Password);
            return this;
        }

        #endregion

        #region 加密密码

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password">密码明文</param>
        /// <returns>返回密码密文</returns>
        public static string EncryptPassword(string password)
        {
            return password.MD5();
        }

        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="modifyUserPasswordParameter">密码修改参数</param>
        public Result ModifyPassword(ModifyUserPasswordParameter modifyUserPasswordParameter)
        {
            if (string.IsNullOrWhiteSpace(modifyUserPasswordParameter?.NewPassword))
            {
                return Result.FailedResult("新密码为空");
            }
            //验证当前密码
            if (modifyUserPasswordParameter.CheckCurrentPassword && Password != EncryptPassword(modifyUserPasswordParameter.CurrentPassword))
            {
                return Result.FailedResult("当前密码不正确");
            }
            //加密密码
            string newPassword = EncryptPassword(modifyUserPasswordParameter.NewPassword);
            Password = newPassword;
            return Result.SuccessResult("用户密码修改成功");
        }

        #endregion

        #endregion
    }
}