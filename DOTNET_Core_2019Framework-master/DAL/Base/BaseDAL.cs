using Dapper;
using IDAL;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 基础访问类
    /// </summary>
    public class BaseDAL<T> : IBaseDAL<T> where T : class, new()
    {
        #region 属性和字段
        /// <summary>
        /// 日志
        /// </summary>
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected readonly MySqlConnection _conn = new MySqlConnection(DBConfig.GetConn());

        /// <summary>
        /// 表类型
        /// </summary>
        protected Type Modeltype { get { return typeof(T); } }

        /// <summary>
        /// 表名
        /// </summary>
        protected string Tablename { get { return GetTableName(); } }

        /// <summary>
        /// 字段
        /// </summary>
        protected string Tablefields { get { return Getfields(); } }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        protected string GetTableName()
        {
            string tbname = "";
            Type t = typeof(T);
            TableAttribute tbattr = (TableAttribute)t.GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault();
            tbname = tbattr.Name;
            return tbname;
        }
        /// <summary>
        /// 获取字段组合
        /// </summary>
        /// <returns></returns>
        protected string Getfields()
        {
            string fields = " ";
            Type t = typeof(T);
            //字段名
            StringBuilder sqlfield = new StringBuilder();
            foreach (var x in t.GetProperties())
            {
                var Nameargument = x.CustomAttributes.FirstOrDefault().NamedArguments[0];
                string fieldname = Nameargument.TypedValue.Value.ToString();
                sqlfield.Append(fieldname);
                sqlfield.Append(",");
            }
            fields = sqlfield.ToString();
            fields = fields.Substring(0, fields.Length - 1);
            return fields;
        }
        #endregion

        #region GET
        /// <summary>
        /// 通过ID获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetDTOById(int id)
        {
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.Append("select " + Tablefields + " from " + Tablename + " where id=" + id);
            try
            {
                T model = _conn.Query<T>(sqlsb.ToString()).FirstOrDefault();
                Logger.Info("通过ID获取数据 成功");
                return model;
            }
            catch (Exception error)
            {
                Logger.Error("通过ID获取数据 失败："+error.Message+"。SQL："+sqlsb.ToString());
                return null;
            }

        }

        /// <summary>
        /// 通过ID获取数据异步
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetDTOByIdAsync(int id)
        {
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.Append("select " + Tablefields + " from " + Tablename + " where id=" + id);
            try
            {
                var resultmodel =await Task.Run(() =>
                {
                    T model = _conn.Query<T>(sqlsb.ToString()).FirstOrDefault();
                    Logger.Info("通过ID获取数据异步 成功");
                    return model;
                });
                return resultmodel;
            }
            catch (Exception error)
            {
                Logger.Error("通过ID获取数据异步 失败：" + error.Message + "。SQL：" + sqlsb.ToString());
                return null;
            }

        }
        #endregion

        #region GetList
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="limit">每页数量</param>
        /// <returns></returns>
        public virtual List<T> GetList(int index=1,int limit=10)
        {
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.Append("select " + Tablefields + " from " + Tablename);  

            //计算页码
            if (index == 0)
            {
                sqlsb.Append(" limit 0,"+limit);
            }
            else
            {
                int startindex = (index - 1) * limit;
                sqlsb.Append(" limit "+startindex+","+limit);
            }
            try
            {
                List<T> modellist = _conn.Query<T>(sqlsb.ToString()).ToList();
                Logger.Info("获取分页列表 成功");
                return modellist;
            }
            catch (Exception error)
            {
                Logger.Error("获取分页列表 失败：" + error.Message + "。SQL：" + sqlsb.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取分页列表 异步
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="limit">每页数量</param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetListAsync(int index = 1, int limit = 10)
        {
            #region 拼装SQL/计算页码
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.Append("select " + Tablefields + " from " + Tablename);

            //计算页码
            if (index == 0)
            {
                sqlsb.Append(" limit 0," + limit);
            }
            else
            {
                int startindex = (index - 1) * limit;
                sqlsb.Append(" limit " + startindex + "," + limit);
            }
            #endregion
            try
            {
                var resultlist =await Task.Run(() =>
                {
                    List<T> modellist = _conn.Query<T>(sqlsb.ToString()).ToList();
                    Logger.Info("获取分页列表异步 成功");
                    return modellist;
                });
                return resultlist;

            }
            catch (Exception error)
            {
                Logger.Error("获取分页列表异步 失败：" + error.Message + "。SQL：" + sqlsb.ToString());
                return null;
            }
        }
        /// <summary>
        /// 通过条件获取分页，异步
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="limit">每页数量</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetListAsync(int index=1,int limit=10,string where="")
        {
            #region 拼装SQL/计算页码
            StringBuilder sqlsb = new StringBuilder();
            //删除判断条件前的空格
            //where = where.TrimStart();
            //删除第一个and
            //where = where.Substring(3);
            sqlsb.Append("select " + Tablefields + " from " + Tablename +" where 1=1 ");

            if (!string.IsNullOrWhiteSpace(where))
            {
                //sqlsb.Append(" where "+where);
                sqlsb.Append(where);
            }
            //计算页码
            if (index == 0)
            {
                sqlsb.Append(" limit 0," + limit);
            }
            else
            {
                int startindex = (index - 1) * limit;
                sqlsb.Append(" limit " + startindex + "," + limit);
            }
            #endregion
            try
            {
                var resultlist = await Task.Run(() =>
                {
                    List<T> modellist = _conn.Query<T>(sqlsb.ToString()).ToList();
                    Logger.Info("获取分页列表异步 成功");
                    return modellist;
                });
                return resultlist;

            }
            catch (Exception error)
            {
                Logger.Error("获取分页列表异步 失败：" + error.Message + "。SQL：" + sqlsb.ToString());
                return null;
            }
        }
        /// <summary>
        /// 通过用户ID条件获取分页，异步
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="index">当前页码</param>
        /// <param name="limit">每页数量</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetListAsync(int userid,int index = 1, int limit = 10, string where = "")
        {
            #region 拼装SQL/计算页码
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.Append("select " + Tablefields + " from " + Tablename);
            sqlsb.Append(" where 1=1 " + where);

            if (!string.IsNullOrWhiteSpace(where))
            {
                ////删除判断条件前的空格
                //where = where.TrimStart();
                ////删除第一个and
                //where = where.Substring(3);
                //sqlsb.Append(" where 1=1 " + where);
                //sqlsb.Append(" where " + where);
                sqlsb.Append(" and user_id = " + userid); // 添加用户id
            }
            else
            {
                sqlsb.Append(" and user_id = " + userid); // 添加用户id
            }
            //计算页码
            if (index == 0)
            {
                sqlsb.Append(" limit 0," + limit);
            }
            else
            {
                int startindex = (index - 1) * limit;
                sqlsb.Append(" limit " + startindex + "," + limit);
            }
            #endregion
            try
            {
                var resultlist = await Task.Run(() =>
                {
                    List<T> modellist = _conn.Query<T>(sqlsb.ToString()).ToList();
                    Logger.Info("获取分页列表异步 成功");
                    return modellist;
                });
                return resultlist;

            }
            catch (Exception error)
            {
                Logger.Error("获取分页列表异步 失败：" + error.Message + "。SQL：" + sqlsb.ToString());
                return null;
            }
        }
        #endregion

        #region Insert
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool Insert(T model)
        {
            bool result = false;
            PropertyInfo[] propertys = Modeltype.GetProperties();//获取属性
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into " + Tablename + " (" + Tablefields + ") values (");

            string fields = "";
            for (int i = 0; i < propertys.Length; i++)
            {
                if (propertys[i].PropertyType.FullName == "System.String" || propertys[i].PropertyType.FullName == "System.DateTime")//判断string类型加入引号
                {
                    fields += "'" + propertys[i].GetValue(model) + "',";
                }
                else
                {
                    fields += propertys[i].GetValue(model) + ",";
                }
            }
            fields = fields.Substring(0, fields.Length - 1);
            sb.Append(fields + ")");
            try
            {
                result = _conn.Execute(sb.ToString()) > 0;
                Logger.Error("新增数据 成功");
                return result;
            }
            catch (Exception error)
            {
                Logger.Error("新增数据 失败：" + error.Message + "。SQL：" + sb.ToString());
                return result;
            }
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool Insert(T model,out string msg)
        {
            bool result = false;
            PropertyInfo[] propertys = Modeltype.GetProperties();//获取属性
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into " + Tablename + " (" + Tablefields + ") values (");

            string fields = "";
            for (int i = 0; i < propertys.Length; i++)
            {
                if (propertys[i].PropertyType.FullName == "System.String" || propertys[i].PropertyType.FullName == "System.DateTime")//判断string类型加入引号
                {
                    fields += "'" + propertys[i].GetValue(model) + "',";
                }
                else
                {
                    fields += propertys[i].GetValue(model) + ",";
                }
            }
            fields = fields.Substring(0, fields.Length - 1);
            sb.Append(fields + ")");
            try
            {
                result = _conn.Execute(sb.ToString()) > 0;
                Logger.Error("新增数据 成功");
                msg = "添加成功";
                return result;
            }
            catch (Exception error)
            {
                Logger.Error("新增数据 失败：" + error.Message + "。SQL：" + sb.ToString());
                msg = "添加失败:"+error.Message;
                return result;
            }

        }
        #endregion

        #region Update
        /// <summary>
        /// 根据ID修改实体
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public virtual bool Update(T Model)
        {
            bool result = false;
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.Append("update " + Tablename+" set ");
            Type t = Model.GetType();
            PropertyInfo[] prop = t.GetProperties();
            for(int i = 0; i < prop.Length; i++)
            { 
                //获取字段名
                var argument = prop[i].CustomAttributes.FirstOrDefault().NamedArguments[0];
                string filed = argument.TypedValue.Value.ToString();

                //排除ID字段
                if (prop[i].Name != "Id")//排除ID字段
                {
                    //判断是否是字符串或者是时间加引号
                    if (prop[i].PropertyType.FullName == "System.String" || prop[i].PropertyType.FullName == "System.DateTime")
                    {
                        sqlsb.Append(filed + " = '" + prop[i].GetValue(Model) + "',");// 拼接SQL
                    }
                    else
                    {
                        sqlsb.Append(filed + " = " + prop[i].GetValue(Model) + ",");// 拼接SQL
                    }
                }
            }
            string sql = sqlsb.ToString();
            sql = sql.Substring(0, sql.Length - 1);//去掉最后一个逗号
            StringBuilder sb = new StringBuilder();
            sb.Append(sql);
            sb.Append(" where id=" + Model.GetType().GetProperty("Id").GetValue(Model, null));
            try
            {
                result = _conn.Execute(sb.ToString()) > 0;
                return result;
            }
            catch (Exception error)
            {
                Logger.Error("根据ID修改实体 失败：" + error.Message + "。SQL：" + sb.ToString());
                return result;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(int id)
        {
            bool result = false;
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.Append("delete from " + Tablename + " where id=" + id);
            try
            {
                result = _conn.Execute(sqlsb.ToString()) > 0;
                Logger.Info("根据ID删除实体 成功。SQL："+sqlsb.ToString());
                return result;
            }
            catch(Exception error)
            {
                Logger.Error("根据ID删除实体 失败：" + error.Message + "。SQL：" + sqlsb);
                return result;
            }
            
        }
        #endregion
    }
}
