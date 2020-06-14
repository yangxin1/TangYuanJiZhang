using Common.Redis;
using Microsoft.AspNetCore.Mvc;
using Sparkle_Framework2019.Controllers.Base;
using System.Threading.Tasks;

namespace Sparkle_Framework2019.Controllers.RedisController
{
    /// <summary>
    /// Redis测试控制器
    /// </summary>
    public class RedisTestController : BaseAPIController
    {
        private readonly IRedisHelper service;
        public RedisTestController(IRedisHelper Service)
        {
            service = Service;
        }
        /// <summary>
        /// 添加string
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("/api/redis/string/{key}/{value}")]
        public async Task<IActionResult> InsertRedis(string key, string value)
        {
            bool result = await service.AddString(key, value);
            if (result)
            {
                return Success();
            }
            else
            {
                return Fail("保存失败");
            }
        }
        /// <summary>
        /// 添加string
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("/api/redis/string/{key}")]
        public async Task<IActionResult> GetRedis(string key)
        {
            string result = await service.GetString(key);
            return Success(result);
        }
    }
}
