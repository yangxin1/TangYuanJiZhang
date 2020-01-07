using DTO.Model.DTOCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Model.User
{
    /// <summary>
    /// 用户类
    /// </summary>
    [Table("sys_user")]
    public class UserDTO
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [DTOColumn(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 用户登录名
        /// </summary>
        [DTOColumn(Name ="user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [DTOColumn(Name ="phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DTOColumn(Name ="email")]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DTOColumn(Name ="password")]
        public string Password { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DTOColumn(Name ="status")]
        public int Status { get; set; }

        /// <summary>
        /// 照片地址
        /// </summary>
        [DTOColumn(Name ="photo")]
        public string Photo { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [DTOColumn(Name ="signature")]
        public string Signature { get; set; }
    }
}
