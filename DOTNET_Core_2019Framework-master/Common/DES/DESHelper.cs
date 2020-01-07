using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    /// <summary>
    /// DES 加密解密类
    /// </summary>
    public class DESHelper:IDESHelper
    {
        /// <summary>
        /// 密钥
        /// </summary>
        private string DES_Key = "sparkle831143";

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="str">需要加密的</param>
        /// <returns></returns>
        public string Encrypt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "加密处理失败，加密字符串为空";
            }
            //加密秘钥补位处理
            string encryptKeyall = Convert.ToString(DES_Key);    //定义密钥  
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

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(str);
            des.Key = Encoding.UTF8.GetBytes(DES_Key); // 密匙
            des.IV = Encoding.UTF8.GetBytes(DES_Key);  // 向量
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            var result = Convert.ToBase64String(ms.ToArray());
            return result;
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="str">需要解密的</param>
        /// <returns></returns>
        public string Decrypt(string str)
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
    }
}
