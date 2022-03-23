using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EZNEW.Web.Security;
using EZNEW.Model;
using EZNEWApp.AppServiceContract.Sys;
using EZNEW.Security.Cryptography;
using EZNEW.Serialization;
using EZNEWApp.Domain.Sys.Parameter;
using Api.Console.Model.Request;
using Api.Console.Model.Response;
using Api.Console.Util;

namespace Api.Console.Controllers
{
    /// <summary>
    /// 安全管理
    /// </summary>
    [AllowAnonymous]
    public class SecurityController : ApiBaseController
    {
        IUserAppService _userAppService;
        public SecurityController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        #region 获取公钥

        /// <summary>
        /// 获取公钥
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("publickey")]
        public Result<GetPublicKeyResponse> GetPublicKey()
        {
            return Result<GetPublicKeyResponse>.SuccessResult(new GetPublicKeyResponse()
            {
                PublicKey = Constants.RSA.PublicKey
            });
        }

        #endregion
    }
}
