using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Sparkle_Framework2019.Controllers.Base
{
    public class ApiAuthorize : Attribute, IAuthorizationFilter
    {
        // 参考：https://www.cnblogs.com/morang/p/7606843.html
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var claim = filterContext.HttpContext.User.Claims;
            var userName = claim.FirstOrDefault(x => x.Type == "name")?.Value; // 在这里加入需要进行权限判断的逻辑
            if (userName == null)
            {
                filterContext.Result = new StatusCodeResult(403);
            }
        }
    }
}
