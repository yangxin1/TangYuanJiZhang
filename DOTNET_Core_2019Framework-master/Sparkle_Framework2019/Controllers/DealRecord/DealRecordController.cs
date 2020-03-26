using DTO.Model.DealAccount;
using IDAL.IDealRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sparkle_Framework2019.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparkle_Framework2019.Controllers.DealRecord
{
    /// <summary>
    /// 交易记录接口
    /// </summary>
    [Authorize]
    public class DealRecordController:BaseAPIController
    {
        /// <summary>
        /// 字段和构造函数
        /// </summary>
        private readonly IDealRecordDAL service;
        public DealRecordController(IDealRecordDAL Service)
        {
            service = Service;
        }
        /// <summary>
        /// 获取交易详情列表
        /// </summary>
        /// <param name="index"></param>
        /// <param name="limit"></param>
        /// <param name="accountid"></param>
        /// <returns></returns>
        [HttpGet("/api/deal/list/{accountid}/{index}/{limit}")]
        public async Task<IActionResult> GetDealList(int accountid,int index=1,int limit = 10)
        {
            Dictionary<string, string> where = new Dictionary<string, string>
            {
                {"user_id",CurrentUserId.ToString() },//用户id
                {"account_id",accountid.ToString() }// 账户id
            };
            string condition ="";
            string empty = " ";
            if (where.ContainsKey("user_id") && !string.IsNullOrEmpty(where["user_id"]))
            {
                condition += "and user_id=" + where["user_id"] + empty;
            }
            if (where.ContainsKey("account_id") && !string.IsNullOrEmpty(where["account_id"]))
            {
                condition += "and account_id=" + where["account_id"] + empty;
            }
            List<DealRecordDTO> list = await service.GetListAsync(CurrentUserId, index, limit,condition);
            return Success(list);
        }

        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("/api/deal/record")]
        public IActionResult InsertRecord([FromBody]DealRecordDTO data)
        {
            //在这里判断数据准确性
            //修改总金额
            service.Insert(data);
            return Success();
        }

        /// <summary>
        /// 获取一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/api/deal/record/{id}")]
        public IActionResult GetRecordById(int id)
        {
            DealRecordDTO dto = service.GetDTOById(id);
            return Success(dto);
        }

        /// <summary>
        /// 修改一条记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("/api/deal/record")]
        public IActionResult UpdateRecordById([FromBody]DealRecordDTO data)
        {
            service.Update(data);
            return Success();
        }
    }
}
