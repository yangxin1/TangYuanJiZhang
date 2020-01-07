using DTO.Model.DTOCommon;
using System;

namespace DTO.Model
{
    /// <summary>
    /// 基础数据表公共访问数据类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [DTOColumn(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DTOColumn(Name = "Status")]
        public int Status { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DTOColumn(Name = "Create_By")]
        public string CreateBy { get { return ""; } set { } }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DTOColumn(Name = "Create_Time")]
        public DateTime CreateTime { get { return DateTime.Now; } set { } }

        /// <summary>
        /// 编辑人
        /// </summary>
        [DTOColumn(Name = "Update_By")]
        public string UpdateBy { get { return ""; } set { } }

        /// <summary>
        /// 编辑时间
        /// </summary>
        [DTOColumn(Name = "Update_Time")]
        public DateTime UpdateTime { get { return DateTime.Now; } set { } }
    }
}
