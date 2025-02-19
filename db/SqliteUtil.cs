using Microsoft.Data.Sqlite;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
{
    public class SqliteUtil
    {

 
        public static SqlSugarClient CreateClient() {
            var path = Path.Combine(Environment.CurrentDirectory, @"db\CW.db");
    
            var connectionString = new SqliteConnectionStringBuilder()
            {
                Mode = SqliteOpenMode.ReadWrite,
                DataSource = path
            }.ToString();
   
            return new SqlSugarClient(new ConnectionConfig()
            {
                DbType = SqlSugar.DbType.Sqlite,
                ConnectionString = connectionString,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true
            }, db => {
                db.Aop.OnLogExecuting = (sql, pars) =>
                {

                    //获取原生SQL推荐 5.1.4.63  性能OK
                    //Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));
                    Debug.WriteLine(UtilMethods.GetNativeSql(sql, pars));
                    //获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                    //Console.WriteLine(UtilMethods.GetSqlString(DbType.SqlServer,sql,pars))
                };
            });

        }
    }
}
