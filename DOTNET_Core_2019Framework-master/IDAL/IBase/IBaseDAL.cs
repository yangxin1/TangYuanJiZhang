using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    /// <summary>
    /// 基础数据访问接口
    /// </summary>
    public interface IBaseDAL<T>
    {
        /// <summary>
        /// 通过ID获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetDTOById(int id);

        /// <summary>
        /// 通过ID获取数据 异步
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetDTOByIdAsync(int id);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="limit">每页显示数量</param>
        /// <returns></returns>
        List<T> GetList(int index = 1, int limit = 10);

        /// <summary>
        /// 获取分页数据 异步
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="limit">每页显示数量</param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(int index = 1, int limit = 10);

        /// <summary>
        /// 有条件的获取分页数据 异步
        /// </summary>
        /// <param name="index"></param>
        /// <param name="limit"></param>
        /// <param name="where">and 条件语句</param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(int index = 1, int limit = 10, string where = "");

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Insert(T model);

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool Insert(T model, out string msg);

        /// <summary>
        /// 根据ID修改实体
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        bool Update(T Model);

        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
    }
}
