using EZNEW.Application.Identity.Auth;
using System;
using Microsoft.AspNetCore.Mvc;

namespace EZNEW.ViewModel.Sys.Response
{
    /// <summary>
    /// 权限分组
    /// </summary>
    public class AuthorityGroupViewModel
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
        [Remote("CheckAuthorityGroupName", "Sys", AdditionalFields = "SysNo", ErrorMessage = "分组名已存在",HttpMethod ="Post")]
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
        /// 状态
        /// </summary>
        public AuthorityGroupStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusString
        {
            get
            {
                return Status.ToString();
            }
        }

        /// <summary>
        /// 上级分组
        /// </summary>
        public AuthorityGroupViewModel Parent
        {
            get;
            set;
        }

        /// <summary>
        /// 分组等级
        /// </summary>
        public int Level
        {
            get;
            set;
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