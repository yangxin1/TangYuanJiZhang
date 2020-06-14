using DTO.Model.Acount;
using IDAL.IAcount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sparkle_Framework2019.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sparkle_Framework2019.Controllers.Acount
{
    /// <summary>
    /// 账户接口
    /// </summary>
    [Authorize]
    public class AcountController : BaseAPIController
    {
        private readonly IAcountDAL service;
        public AcountController(IAcountDAL Service)
        {
            service = Service;
        }

        /// <summary>
        /// 获取账户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/account/acountlist/{index}/{limit}")]
        public async Task<IActionResult> GetAcountList(int index = 1, int limit = 10)
        {
            //List<AccountDTO> acountlist = await service.GetListAsync(CurrentUserId,index, limit, "");
            List<AccountDTO> acountlist = await service.GetListAsync(index, limit);
            return Success(acountlist);
        }
        /// <summary>
        /// 根据ID获取账户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/api/account/account/{id}")]
        public IActionResult GetAccountByID(int id)
        {
            AccountDTO account = service.GetDTOById(id);
            if (account == null) return Fail("未获取到数据");
            return Success(account);
        }
        /// <summary>
        /// 添加账户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost("/api/account/account")]
        public IActionResult AddAccount(AccountDTO account)
        {
            if (service.Insert(account, out string msg))
            {
                return Success(msg);
            }
            else
            {
                return Fail(msg);
            }
        }
        /// <summary>
        /// 修改账户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPut("api/account/account")]
        public IActionResult UpdateAccount(AccountDTO account)
        {
            if (service.Update(account))
            {
                return Success();
            }
            else
            {
                return Fail("修改失败");
            }
        }
    }
}
