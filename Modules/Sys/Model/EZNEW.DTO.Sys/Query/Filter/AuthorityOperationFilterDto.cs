using EZNEW.Application.Identity.Auth;
using EZNEW.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Query.Filter
{
    /// <summary>
    /// 授权操作筛选
    /// </summary>
    public class AuthorityOperationFilterDto : PagingFilter
    {
        #region	属性

        /// <summary>
        /// 主键编号
        /// </summary>
        public List<long> SysNos
        {
            get;
            set;
        }

        /// <summary>
        /// 控制器
        /// </summary>
        public string ControllerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 操作方法
        /// </summary>
        public string ActionCode
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public AuthorityOperationStatus? Status
        {
            get;
            set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 操作分组
        /// </summary>
        public long? Group
        {
            get;
            set;
        }

        /// <summary>
        /// 授权类型
        /// </summary>
        public AuthorityOperationAuthorizeType? AuthorizeType
        {
            get;
            set;
        }

        /// <summary>
        /// 所属应用
        /// </summary>
        public string Application
        {
            get;
            set;
        }

        /// <summary>
        /// 方法描述
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// ControllerCode/ActionCode/Name 匹配关键字
        /// </summary>
        public string OperationMateKey
        {
            get; set;
        }

        #endregion

        #region 数据加载

        /// <summary>
        /// 加载操作分组数据
        /// </summary>
        public bool LoadGroup
        {
            get; set;
        }

        #endregion
    }
}
