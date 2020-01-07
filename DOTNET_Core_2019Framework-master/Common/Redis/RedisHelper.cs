using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Redis
{
    /// <summary>
    /// Redis访问类
    /// </summary>
    public class RedisHelper:IRedisHelper
    {
        public static readonly IConfigurationRoot builder = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
        private readonly ConnectionMultiplexer Redis;
        private readonly IDatabase Db;
        private readonly IDESHelper deshelper = new DESHelper();

        public RedisHelper()
        {
            string conn = deshelper.Decrypt(builder["Redis"]);
            Redis = ConnectionMultiplexer.Connect(conn);
            Db = Redis.GetDatabase();
        }

        /// <summary>
        /// 添加测试
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public async Task<bool> AddString(string Key, string Value)
        {
            var result = Task.Run(() => Db.StringSet(Key, Value));
            return await result;
        }
        /// <summary>
        /// 通过key获取(使用异步)
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public async Task<string> GetString(string Key)
        {
            return await Task.Run(() => Db.StringGet(Key));
            //return Db.StringGet(Key);
        }
    }
}
