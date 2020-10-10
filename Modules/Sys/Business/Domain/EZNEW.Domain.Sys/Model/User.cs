using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Develop.Domain.Aggregation;
using EZNEW.Domain.Sys.Repository;
using EZNEW.ValueType;
using EZNEW.Code;
using EZNEW.Domain.Sys.Service;
using EZNEW.DependencyInjection;
using EZNEW.Module.Sys;
using EZNEW.Domain.Sys.Parameter;
using EZNEW.Response;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 管理用户
    /// </summary>
    public class User : AggregationRoot<User>
    {
        #region 构造方法

        /// <summary>
        /// 实例化用户
        /// </summary>
        internal User()
        {
            repository = this.Instance<IUserRepository>();
        }

        #endregion

        #region	属性

        /// <summary>
        /// 用户编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真实名称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; protected set; }

        /// <summary>
        /// 类型
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// 超级用户
        /// </summary>
        public bool SuperUser { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        #endregion

        #region 内部方法

        #region 获取对象标识值

        /// <summary>
        /// 获取对象标识值
        /// </summary>
        /// <returns></returns>
        protected override string GetIdentityValue()
        {
            return Id.ToString();
        }

        #endregion

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
            var saveData = base.OnAdding();
            saveData.Password = EncryptPassword(Password);
            saveData.CreateDate = DateTime.Now;
            return saveData;
        }

        #endregion

        #endregion

        #region 静态方法

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

        #region 生成用户编号

        /// <summary>
        /// 生成用户编号
        /// </summary>
        /// <returns>返回用户编号</returns>
        public static long GenerateUserId()
        {
            return SysManager.GetId(SysModuleObject.User);
        }

        #endregion

        #region 创建用户对象

        /// <summary>
        /// 生成用户对象
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="userType">用户类型</param>
        /// <returns>返回一个信息用户对象</returns>
        public static User Create(long id, UserType userType = UserType.Management)
        {
            User user = new User()
            {
                Id = id,
                UserType = userType
            };
            return user;
        }

        #endregion

        #endregion

        #region 功能方法

        #region 验证对象标识信息是否为空

        /// <summary>
        /// 验证对象标识信息是否为空
        /// </summary>
        /// <returns>返回标识信息是否为空</returns>
        public override bool IdentityValueIsNone()
        {
            return Id < 1;
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

        #region 当前账号是否允许登陆

        /// <summary>
        /// 当前账号是否允许登陆
        /// 超级管理员始终允许登录
        /// </summary>
        /// <returns>返回当前账号是否允许登录</returns>
        public bool AllowLogin()
        {
            return SuperUser || Status == UserStatus.Enable;
        }

        #endregion

        #region 初始化标识信息

        /// <summary>
        /// 初始化标识信息
        /// </summary>
        public override void InitIdentityValue()
        {
            base.InitIdentityValue();
            Id = GenerateUserId();
        }

        #endregion

        #endregion
    }
}