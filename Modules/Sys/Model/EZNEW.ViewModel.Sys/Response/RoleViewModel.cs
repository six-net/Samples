using EZNEW.Module.Sys;
using System;
using Microsoft.AspNetCore.Mvc;

namespace EZNEW.ViewModel.Sys.Response
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleViewModel
    {
        #region	属性

        /// <summary>
        /// 角色编号
        /// </summary>
        public long SysNo
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [Remote("CheckRoleName", "Sys", AdditionalFields = "SysNo", ErrorMessage = "角色名已存在",HttpMethod ="post")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level
        {
            get;
            set;
        }

        /// <summary>
        /// 上级
        /// </summary>
        public RoleViewModel Parent
        {
            get;
            set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public RoleStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// 状态值
        /// </summary>
        public string StatusText
        {
            get
            {
                return Status.ToString();
            }
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateDate
        {
            get;
            set;
        }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        #endregion
    }
}