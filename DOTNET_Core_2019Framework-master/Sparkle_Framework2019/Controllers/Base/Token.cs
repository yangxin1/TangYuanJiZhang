using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sparkle_Framework2019.Controllers.Base
{
    /// <summary>
    /// JWTToken加密类
    /// </summary>
    public class Token
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public static string SecurityKey { get { return Getsecuritykey("jwtSecret"); } }

        /// <summary>
        /// 过期时间
        /// </summary>
        public static int ExpiresTime { get { return Convert.ToInt32(Getsecuritykey("JWTExpries")); } }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="node">配置节点名称</param>
        /// <returns></returns>
        private static string Getsecuritykey(string node)
        {
            IConfigurationRoot builder = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();
            return builder[node];
        }
        #region 加密
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="payLoad">加密信息</param>
        /// <returns></returns>
        public static string CreateTokenByHandler(Dictionary<string, object> payLoad)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim>();
            foreach (var key in payLoad.Keys)
            {
                var tempClaim = new Claim(key, payLoad[key]?.ToString());
                claims.Add(tempClaim);
            }

            var jwt = new JwtSecurityToken(
                issuer: "Sparkle", // 签发者
                audience: "User", // 使用者
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(ExpiresTime)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey)), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
        #endregion

        #region 解密
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="encodeJwt"></param>
        /// <param name="msg"></param>
        /// <param name="validatePayLoad"></param>
        /// <returns></returns>
        public static bool Validate(string encodeJwt, out string msg, Func<Dictionary<string, object>, bool> validatePayLoad = null)
        {
            var success = true;
            Dictionary<string, object> header;
            Dictionary<string, object> payLoad;
            var jwtArr = encodeJwt.Split('.');
            try
            {
                header = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[0]));
                payLoad = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[1]));
            }
            catch (Exception error)
            {
                msg = error.Message;
                return false;
            }
            var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(SecurityKey));
            //首先验证签名是否正确(重要)
            success = success && string.Equals(jwtArr[2], Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(jwtArr[0], ".", jwtArr[1])))));
            if (!success)
            {
                msg = "签名不正确";
                return success;//签名不正确直接返回
            }
            //其次验证是否在有效期内
            var now = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000; // 获取时间戳
            success = success && (now >= long.Parse(payLoad["nbf"].ToString()) && now < long.Parse(payLoad["exp"].ToString()));

            //再其次 进行自定义的验证
            if (validatePayLoad == null && success)
            {
                msg = "验证通过";
                return success;
            }
            success = success && validatePayLoad(payLoad); // 执行自定义验证
            if (success) msg = "通过"; else msg = "不通过";
            return success;
        }
        #endregion 
    }
}
