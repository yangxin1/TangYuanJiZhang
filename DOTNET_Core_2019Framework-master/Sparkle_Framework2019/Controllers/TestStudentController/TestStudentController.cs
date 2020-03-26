using DTO.Model.Student;
using IDAL.ITestStudent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sparkle_Framework2019.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sparkle_Framework2019.Controllers.TestStudentController
{
    /// <summary>
    /// 学生信息相关接口（测试）
    /// </summary>
    [Authorize]
    public class TestStudentController : BaseAPIController
    {
        #region 字段
        /// <summary>
        /// 服务
        /// </summary>
        private readonly ITestStudent service;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Service"></param>
        public TestStudentController(ITestStudent Service)
        {
            service = Service;
        }
        #endregion

        #region 接口
        /// <summary>
        /// 获取学生ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/api/test/student/{id}")]
        public IActionResult GetStudentById(int id)
        {
            TestStudentDTO student = service.GetDTOById(id);
            return Success(student);
        }

        /// <summary>
        /// 获取学生ID 异步（允许匿名访问）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("/api/test/getstudentasync/{id}")]
        public async Task<IActionResult> Getstuasync(int id)
        {
            TestStudentDTO stu = await service.GetDTOByIdAsync(id);
            return Success(stu);
        }
        /// <summary>
        /// 获取学生列表
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="limit">每页数量</param>
        /// <returns></returns>
        [HttpGet("/api/test/studentlist/{index}/{limit}")]
        public IActionResult GetStudentList(int index=1,int limit = 10)
        {
            List<TestStudentDTO> stulist = service.GetList(index, limit);
            return Success(stulist);
        }
        /// <summary>
        /// 获取学生列表异步
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="limit">每页数量</param>
        /// <returns></returns>
        [HttpGet("/api/test/studentlistasync/{index}/{limit}")]
        public async Task<IActionResult> GetStudentListAsync(int index = 1, int limit = 10)
        {
            List<TestStudentDTO> list = await service.GetListAsync(index, limit);
            return Success(list);
        }
        /// <summary>
        /// 根据条件获取学生列表
        /// </summary>
        /// <param name="index"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("/api/test/studentlistwhere/{index}/{limit}")]
        public async Task<IActionResult> GetStudentListAsyncWhere(int index=1,int limit = 10)
        {
            //从前端传入的筛选条件
            Dictionary<string, string> where = new Dictionary<string, string>
            {
                { "score1", "80" },
                { "status1", "1" }
            };
            string condition = "";
            string empty = " ";
            if (where.ContainsKey("score1")&&!string.IsNullOrEmpty(where["score1"]))
            {
                condition += "and score=" + where["score1"]+empty;
            }
            if (where.ContainsKey("status1") && !string.IsNullOrEmpty(where["status1"]))
            {
                condition += "and status=" + where["status1"] + empty;
            }
            List<TestStudentDTO> list = await service.GetListAsync(index, limit, condition);
            return Success(list);
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        [HttpPost("/api/test/student")]
        public IActionResult InsertStuddent(TestStudentDTO stu)
        {
            if (service.Insert(stu))
            {
                return Success();
            }
            else
            {
                return Fail("添加学生失败");
            }
        }
        /// <summary>
        /// 编辑学生信息
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        [HttpPut("/api/test/student")]
        public IActionResult UpdateStudent(TestStudentDTO stu)
        {
            if (service.Update(stu))
            {
                return Success();
            }
            else
            {
                return Fail("修改失败");
            }
        }
        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/api/test/student/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            if (service.Delete(id))
            {
                return Success();
            }
            else
            {
                return Fail("删除失败");
            }
        }
    }
    #endregion
}
