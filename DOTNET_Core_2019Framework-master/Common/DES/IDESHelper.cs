using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    /// <summary>
    /// 加密解密接口
    /// </summary>
    public interface IDESHelper
    {
        /// <summary>
        /// 加密接口
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string Encrypt(string str);
        /// <summary>
        /// 解密接口
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string Decrypt(string str);
    }
}
