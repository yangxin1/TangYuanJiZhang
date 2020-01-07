using IDAL.ITestStudent;
using System;
using System.Collections.Generic;
using System.Text;
using DTO.Model.Student;

namespace DAL.TestStudent
{
    /// <summary>
    /// 测试访问类
    /// </summary>
    public class TestStudentDAL:BaseDAL<TestStudentDTO>,ITestStudent
    {
    }
}
