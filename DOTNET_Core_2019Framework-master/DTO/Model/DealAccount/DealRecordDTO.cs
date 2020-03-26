using DTO.Model.DTOCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Model.DealAccount
{
    /// <summary>
    /// 交易实体
    /// </summary>
    [Table("biz_dealrecord")]
    public class DealRecordDTO:BaseBizEntity
    {
        /// <summary>
        /// 账户ID
        /// </summary>
        [DTOColumn(Name ="account_id")]
        public int AccountId { get; set; }

        /// <summary>
        /// 交易类型（1：支出，2：收入）
        /// </summary>
        [DTOColumn(Name ="type")]
        public int Type { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        [DTOColumn(Name = "deal_money")]
        public decimal DealMoney { get; set; }

        /// <summary>
        /// 交易详细类型（1：生活费，2：其他 等）
        /// </summary>
        [DTOColumn(Name = "deal_type")]
        public int DealType { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        [DTOColumn(Name = "Deal_time")]
        public DateTime DealTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DTOColumn(Name = "remark")]
        public string Remark { get; set; }
    }
}
