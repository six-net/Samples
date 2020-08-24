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
        public string Password { get; set; }

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

        #region 设置用户密码

        /// <summary>
        /// 设置用户密码
        /// </summary>
        /// <param name="newPwd">新的密码</param>
        void SetPassword(string newPwd)
        {
            Password = PasswordEncryption(newPwd);
        }

        #endregion

        #region 验证对象标识信息是否未设置

        /// <summary>
        /// 判断对象标识信息是否未设置
        /// </summary>
        /// <returns></returns>
        public override bool IdentityValueIsNone()
        {
            return Id < 1;
        }

        #endregion

        #region 从指定对象复制值

        /// <summary>
        /// 从指定对象复制值
        /// </summary>
        /// <typeparam name="TModel">数据类型</typeparam>
        /// <param name="similarObject">数据对象</param>
        /// <param name="excludePropertys">排除不复制的属性</param>
        protected override void CopyDataFromSimilarObject<TModel>(TModel similarObject, IEnumerable<string> excludePropertys = null)
        {
            base.CopyDataFromSimilarObject(similarObject, excludePropertys);
            if (similarObject == null)
            {
                return;
            }
            excludePropertys = excludePropertys ?? new List<string>(0);

            #region 复制值

            if (!excludePropertys.Contains("SysNo"))
            {
                Id = similarObject.Id;
            }
            if (!excludePropertys.Contains("UserName"))
            {
                UserName = similarObject.UserName;
            }
            if (!excludePropertys.Contains("RealName"))
            {
                RealName = similarObject.RealName;
            }
            if (!excludePropertys.Contains("Password"))
            {
                Password = similarObject.Password;
            }
            if (!excludePropertys.Contains("UserType"))
            {
                UserType = similarObject.UserType;
            }
            if (!excludePropertys.Contains("SuperUser"))
            {
                SuperUser = similarObject.SuperUser;
            }
            if (!excludePropertys.Contains("Status"))
            {
                Status = similarObject.Status;
            }
            if (!excludePropertys.Contains(nameof(Email)))
            {
                Email = similarObject.Email;
            }
            if (!excludePropertys.Contains(nameof(QQ)))
            {
                QQ = similarObject.QQ;
            }
            if (!excludePropertys.Contains(nameof(Mobile)))
            {
                Mobile = similarObject.Mobile;
            }
            if (!excludePropertys.Contains("CreateDate"))
            {
                CreateDate = similarObject.CreateDate;
            }

            #endregion
        }

        #endregion

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

        #region 保存验证

        /// <summary>
        /// 保存验证
        /// </summary>
        /// <returns>返回是否允许保存执行</returns>
        protected override bool SaveValidation()
        {
            var allowSave = base.SaveValidation();
            if (allowSave)
            {
                //添加数据
                if (IsNew)
                {
                    CreateDate = DateTime.Now;
                }
            }
            return allowSave;
        }

        #endregion

        #endregion

        #region 静态方法

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

        #region 修改密码

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPassword">信息的密码</param>
        public void ModifyPassword(string newPassword)
        {
            SetPassword(newPassword);
        }

        #endregion

        #region 执行用户密码加密

        /// <summary>
        /// 执行用户密码加密
        /// </summary>
        /// <param name="pwdValue">密码值</param>
        /// <returns></returns>
        public static string PasswordEncryption(string pwdValue)
        {
            return pwdValue.MD5();
        }

        #endregion

        #region 当前账号是否允许登陆

        /// <summary>
        /// 当前账号是否允许登陆
        /// 超级管理员始终返回true
        /// 若用户绑定的角色及所有上级角色都关闭，则不能登录
        /// </summary>
        /// <returns></returns>
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

        #region 根据给定的对象更新当前信息

        /// <summary>
        /// 根据给定的对象更新当前信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="excludePropertys">排除更新的属性</param>
        public virtual void ModifyFromOtherUser(User user, IEnumerable<string> excludePropertys = null)
        {
            if (user == null)
            {
                return;
            }
            CopyDataFromSimilarObject(user, excludePropertys);
        }

        #endregion

        #endregion
    }
}