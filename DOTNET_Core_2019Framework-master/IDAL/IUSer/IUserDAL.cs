using DTO.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL.IUSer
{
    /// <summary>
    /// 用户访问类
    /// </summary>
    public interface IUserDAL:IBaseDAL<UserDTO>
    {
        /// <summary>
        /// 检测登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CheckLogin(string name, string password, out UserDTO user,out string msg);
    }
}
