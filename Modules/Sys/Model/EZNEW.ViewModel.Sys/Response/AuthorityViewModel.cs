using EZNEW.Module.Sys;
using System;
using Microsoft.AspNetCore.Mvc;

namespace EZNEW.ViewModel.Sys.Response
{
    /// <summary>
    /// 权限
    /// </summary>
    public class AuthorityViewModel
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
        /// 权限编码
        /// </summary>
        [Remote("CheckAuthorityCode", "Sys", ErrorMessage = "权限码已存在",HttpMethod ="Post")]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [Remote("CheckAuthorityName","Sys",AdditionalFields ="Code",ErrorMessage ="权限名已存在",HttpMethod ="Post")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 权限类型
        /// </summary>
        public AuthorityType AuthorityType
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public AuthorityStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// 状态字符串
        /// </summary>
        public string StatusString
        {
            get
            {
                return Status.ToString();
            }
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
        /// 权限分组
        /// </summary>
        public AuthorityGroupViewModel Group
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

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateDate
        {
            get;
            set;
        }

        #endregion
    }
}