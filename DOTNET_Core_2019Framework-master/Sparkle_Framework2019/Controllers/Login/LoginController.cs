using System;
using System.Collections.Generic;
using Common;
using DTO.Model.User;
using DTO.ViewModel.login;
using IDAL.IUSer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sparkle_Framework2019.Controllers.Base;

namespace Sparkle_Framework2019.Controllers.Login
{
    /// <summary>
    /// 登陆控制器
    /// </summary>
    public class LoginController : BaseAPIController
    {
        #region 构造函数
        /// <summary>
        /// IP地址服务
        /// </summary>
        private readonly IHttpContextAccessor ipservice;

        /// <summary>
        /// 加密解密
        /// </summary>
        private readonly IDESHelper deshelper;

        /// <summary>
        /// 数据访问
        /// </summary>
        private readonly IUserDAL service;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginController(IHttpContextAccessor httpContextAccessor,IDESHelper DEShelper,IUserDAL Service)
        {
            ipservice = httpContextAccessor;
            deshelper = DEShelper;
            service = Service;
        }
        #endregion

        #region 控制器
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("/api/login/login")]
        public IActionResult Login([FromBody]LoginUser loginuser)
        {
            string name = loginuser.name;
            string password = loginuser.password;
            //空判断
            if (name == null || password == null) return Fail("用户名和密码为空");
            //身份校验
            string despassword = deshelper.Encrypt(password);
            bool result = service.CheckLogin(name, despassword, out UserDTO user,out string msg);
            if (result)
            {
                //通过用户获取角色
                string roles = "admin";
                //获取登录IP
                string ipaddr = ipservice.HttpContext.Connection.RemoteIpAddress.ToString();
                Dictionary<string, object> para = new Dictionary<string, object>
                {
                    { "name", name },
                    //{ "password", despassword },
                    { "logintime", DateTime.Now },
                    { "roles",roles },
                    {"ip",ipaddr },
                    {"userid", user.Id}
                };
                string token = Token.CreateTokenByHandler(para); // 加密
                return Success(token);
            }
            else
            {
                return Fail("登陆失败："+msg);
            }
        }
        /// <summary>
        /// 检测登录(linshi)
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        [HttpPost("/api/login/checklogin")]
        public IActionResult CheckLogin(string jwt)
        {
            if(Token.Validate(jwt, out string message))
            {
                return Success(ipservice.HttpContext.Connection.RemoteIpAddress.ToString());
            }
            else
            {
                return Fail("验证失败：" + message);
            }
        }
        #endregion
    }
}