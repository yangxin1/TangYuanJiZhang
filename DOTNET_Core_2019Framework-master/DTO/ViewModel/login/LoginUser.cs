using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.ViewModel.login
{
    /// <summary>
    /// 用户登录vm
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
    }
}
