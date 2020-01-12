using DTO.Model.DTOCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Model.Acount
{
    /// <summary>
    /// 账户实体
    /// </summary>
    [Table("biz_account")]
    public class AccountDTO: BaseBizEntity
    {
        /// <summary>
        /// 账本名称
        /// </summary>
        [DTOColumn(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// 账户类型(1:储蓄卡账户,2:网络账户)
        /// </summary>
        [DTOColumn(Name="account_type")]
        public int AccountType { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        [DTOColumn(Name ="amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 是否计入总金额(0:不计入,1:计入)
        /// </summary>
        [DTOColumn(Name ="is_allaccount")]
        public int IsAllAmount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DTOColumn(Name="remark")]
        public string Remark { get; set; }
    }
}
