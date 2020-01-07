using DTO.Model.DTOCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Model.Student
{
    /// <summary>
    /// 测试类
    /// </summary>
    [Table("test_student")]
    public class TestStudentDTO : BaseEntity
    {
        [DTOColumn(Name = "stu_name")]
        public string StudentName { get; set; }

        [DTOColumn(Name = "class_id")]
        public int ClassId { get; set; }

        [DTOColumn(Name = "score")]
        public int Score { get; set; }
    }
}
