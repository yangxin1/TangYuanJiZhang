using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using NLog;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DAL
{
    /// <summary>
    /// 数据库连接类
    /// </summary>
    public static class DBConfig
    {
        static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 密钥
        /// </summary>
        private static string DES_Key = "sparkle831143";


        /// <summary>
        /// 连接字符串
        /// </summary>
        public static readonly IConfigurationRoot builder = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetConn()
        {
            string sqlcon = Decrypt(builder["Connection"]);
            return Decrypt(builder["Connection"]);
        }

        #region 解密
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="str">需要解密的</param>
        /// <returns></returns>
        private static string Decrypt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "解密处理失败，解密字符串为空";
            }
            //解密秘钥补位处理
            string encryptKeyall = Convert.ToString(DES_Key);
            if (encryptKeyall.Length < 9)
            {
                for (; ; )
                {
                    if (encryptKeyall.Length < 9)
                        encryptKeyall += encryptKeyall;
                    else
                        break;
                }
            }
            string encryptKey = encryptKeyall.Substring(0, 8);
            DES_Key = encryptKey;
            //解密处理
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Convert.FromBase64String(str);
                des.Key = ASCIIEncoding.UTF8.GetBytes(DES_Key);  //秘钥---加密解密秘钥需要一致
                des.IV = ASCIIEncoding.UTF8.GetBytes(DES_Key);   //向量
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception error)
            {
                Logger.Error("解密连接字符串失败：" + error);
                return "";
            }
        }   
        #endregion
    }

}
