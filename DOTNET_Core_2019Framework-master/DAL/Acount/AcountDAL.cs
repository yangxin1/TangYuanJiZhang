using DTO.Model.Acount;
using IDAL.IAcount;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Acount
{
    /// <summary>
    /// 账户实现
    /// </summary>
    public class AcountDAL:BaseDAL<AccountDTO>,IAcountDAL
    {
    }
}
