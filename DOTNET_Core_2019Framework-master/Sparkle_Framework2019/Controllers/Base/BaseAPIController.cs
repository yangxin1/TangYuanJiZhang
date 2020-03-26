using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Sparkle_Framework2019.Controllers.Base
{
    /// <summary>
    /// 基础APIController
    /// </summary>
    [ApiController]
    [EnableCors("Cors")]
    public class BaseAPIController : ControllerBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        protected string CurrentUserName
        {
            get
            {
                Claim claim = User.Claims.FirstOrDefault(x => x.Type == "name"); // 保存在Claim中的用户名
                if (claim == null) return "";
                return claim.Value;
            }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        protected int CurrentUserId
        {
            get
            {
                Claim claim = User.Claims.FirstOrDefault(x => x.Type == "userid");
                if (claim == null) return 0;
                return Convert.ToInt32(claim.Value);
            }
        }

        /// <summary>
        /// 日志
        /// </summary>
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region 通用方法
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        protected virtual IActionResult Success(dynamic Data = null, string Msg = "操作成功")
        {
            return new JsonResult(new { code = StateCode.Ok, msg = Msg, data = Data });
        }

        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="Msg"></param>
        /// <returns></returns>
        protected virtual IActionResult Fail(string Msg)
        {
            return new JsonResult(new { code = StateCode.Fail, msg = Msg });
        }
        //获取当前用户名方法
        #endregion
    }
    /// <summary>
    /// 返回消息枚举
    /// </summary>
    public enum StateCode
    {
        Ok = 200,
        Fail = 403
    }
}