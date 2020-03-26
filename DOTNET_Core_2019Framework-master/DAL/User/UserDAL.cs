using Dapper;
using DTO.Model.User;
using IDAL.IUSer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DAL.User
{
    /// <summary>
    /// 用户访问类
    /// </summary>
    public class UserDAL:BaseDAL<UserDTO>,IUserDAL
    {
        /// <summary>
        /// 检测登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CheckLogin(string name, string password, out UserDTO user,out string msg)
        {
            bool result = false;
            user = null;
            msg = "";
            

            StringBuilder sqlsb = new StringBuilder();
            sqlsb.Append("select " + Tablefields + " from " + Tablename);
            #region 用户匹配
            //全数字：电话
            if (Regex.IsMatch(name, @"^[0-9]*$"))
            {
                if(!CheckUserName("phone",name,out string checkmsg))
                {
                    msg = "电话号码错误";
                    return false;
                }
                sqlsb.Append(" where phone='" + name + "'");
            }
            //包含@：邮箱
            else if (Regex.IsMatch(name, ".*[@.]+.*"))
            {
                if (!CheckUserName("email", name, out string checkmsg))
                {
                    msg = "邮箱地址错误";
                    return false;
                }
                sqlsb.Append(" where email='" + name + "'");
            }
            //其他：账号
            else
            {
                if (!CheckUserName("user_name", name, out string checkmsg))
                {
                    msg = "用户名错误";
                    return false;
                }
                sqlsb.Append(" where user_name = '" + name + "'");
            }
            #endregion
            sqlsb.Append(" and password='"+password+"'");
            user = _conn.Query<UserDTO>(sqlsb.ToString()).FirstOrDefault();
            if (user == null)
            {
                msg = "密码不正确";
                return false;
            }
            result = true;
            msg = "校验成功";
            return result;
        }
        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="fieldname"></param>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        private bool CheckUserName(string fieldname, string value, out string msg)
        {
            msg = "";
            string checkname = _conn.Query<string>("select "+fieldname+" from sys_user where "+fieldname+"='" + value + "'").FirstOrDefault();
            if (string.IsNullOrWhiteSpace(checkname))
            {
                msg = "未找到用户名";
                return false;
            }
            return true;
        }
    }

}
