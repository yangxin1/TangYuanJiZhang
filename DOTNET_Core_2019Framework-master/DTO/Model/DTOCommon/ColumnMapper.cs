using Dapper;
using DTO.Model.Acount;
using DTO.Model.DealAccount;
using DTO.Model.Student;
using DTO.Model.User;

namespace DTO.Model.DTOCommon
{
    public class ColumnMapper
    {
        /// <summary>
        /// 实体映射类
        /// </summary>
        public static void SetMapper()
        {
            //数据库字段名和c#属性名不一致，手动添加映射关系
            //每个实体类都需要添加映射关系
            SqlMapper.SetTypeMap(typeof(TestStudentDTO), new DTOColumnAttributeTypeMapper<TestStudentDTO>());
            SqlMapper.SetTypeMap(typeof(UserDTO), new DTOColumnAttributeTypeMapper<UserDTO>());
            SqlMapper.SetTypeMap(typeof(AccountDTO), new DTOColumnAttributeTypeMapper<AccountDTO>());
            SqlMapper.SetTypeMap(typeof(DealRecordDTO), new DTOColumnAttributeTypeMapper<DealRecordDTO>());
        }
    }
}
