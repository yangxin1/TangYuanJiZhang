using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sparkle_Framework2019.Controllers.Base;

namespace Sparkle_Framework2019.Controllers.APILab
{
    /// <summary>
    /// 测试接口
    /// </summary>
    [Authorize]
    public class HeaderLABController : BaseAPIController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HeaderLABController(IHttpContextAccessor http)
        {
            _httpContextAccessor = http;
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/lab/header")]
        public IActionResult GetHeader()
        {
            var header = _httpContextAccessor.HttpContext.Request.Headers;
            string token = header["Authorization"].ToString().Trim();
            token = token.Substring(7);
            return Success(token);
        }
    }
}