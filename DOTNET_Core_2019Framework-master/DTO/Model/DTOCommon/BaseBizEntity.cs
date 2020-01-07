using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Model.DTOCommon
{
    /// <summary>
    /// DTO基础基类
    /// </summary>
    public class BaseBizEntity
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [DTOColumn(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [DTOColumn(Name ="user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DTOColumn(Name ="create_time")]
        public DateTime CreateTime { get; set; }
    }
}
