using EZNEW.Application.Identity.Auth;
using System;
using Microsoft.AspNetCore.Mvc;

namespace EZNEW.ViewModel.Sys.Response
{
    /// <summary>
    /// 授权操作组
    /// </summary>
    public class AuthorityOperationGroupViewModel
    {
        #region	属性

        /// <summary>
        /// 编号
        /// </summary>
        public long SysNo
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [Remote("CheckAuthorityOperationGroupName", "Sys", AdditionalFields = "SysNo", ErrorMessage = "操作分组名已存在", HttpMethod = "Post")]
        public string Name
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
        /// 上级
        /// </summary>
        public AuthorityOperationGroupViewModel Parent
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
        /// 状态
        /// </summary>
        public AuthorityOperationGroupStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// 状态值
        /// </summary>
        public string StatusString
        {
            get
            {
                return Status.ToString();
            }
        }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        #endregion
    }
}