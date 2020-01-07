using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Redis
{
    /// <summary>
    /// Redis相关接口
    /// </summary>
    public interface IRedisHelper
    {
        Task<bool> AddString(string Key, string Value);
        Task<string> GetString(string Key);
    }
}
