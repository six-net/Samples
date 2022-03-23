using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using IdentityModel;
using EZNEW.Model;
using EZNEW.Security.Cryptography;
using EZNEW.Serialization;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.Domain.Sys.Parameter;
using Api.Console.Util;
using Api.Console.Model.Response;
using Api.Console.Model.Request;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Console.Controllers
{
    /// <summary>
    /// 登录授权
    /// </summary>
    [AllowAnonymous]
    public class AccountController : ApiBaseController
    {
        IUserAppService _userAppService;
        public AccountController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        #region 登录

        [HttpPost]
        [Route("login")]
        public Result<LoginResponse> Login([FromBody] string values)
        {
            var jsonData = RSAHelper.Decrypt(Constants.RSA.PrivateKey, values);
            var loginData = JsonSerializer.Deserialize<LoginRequest>(jsonData);
            var loginResult = _userAppService.Login(new LoginParameter()
            {
                UserName = loginData?.UserName,
                Password = loginData?.Password
            });
            if (loginResult?.Success ?? false)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var authTime = DateTime.UtcNow;
                var expiresAt = authTime.AddDays(7);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtClaimTypes.Audience,"api"),
                        new Claim(JwtClaimTypes.Issuer,"http://localhost:5000"),
                        new Claim(JwtClaimTypes.Id, loginResult.Data.Id.ToString()),
                        new Claim(JwtClaimTypes.Name, loginResult.Data.UserName),
                        new Claim(JwtClaimTypes.Email, loginResult.Data.Email),
                        new Claim(JwtClaimTypes.PhoneNumber,loginResult.Data.Mobile)
                    }),
                    Expires = expiresAt,
                    SigningCredentials = new SigningCredentials(Constants.Token.SecurityKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return Result<LoginResponse>.SuccessResult(new LoginResponse()
                {
                    Token = tokenString
                });
            }
            else
            {
                return Result<LoginResponse>.FailedResult("登录失败");
            }

        }

        #endregion

        #region 退出登录

        [HttpGet]
        [Route("logout")]
        public Result Logout()
        {
            return Result.SuccessResult();
        }

        #endregion
    }
}
