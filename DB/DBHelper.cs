using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data;
using Game28.Model;
using System.Diagnostics;

namespace Game28.DB
{
    public class DBHelper : IDisposable
    {
        const string DBName = @"DB\DB.db3";
        SQLiteConnection sqlCon = null;

        private static DBHelper instance;
        public static DBHelper Instance
        {
            get
            {
                instance = instance ?? new DBHelper();
                return instance;
            }
        }

        private DBHelper()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(appPath, DBName);
            if (File.Exists(dbPath))
            {
                string connectionString = string.Format("data source={0}", dbPath);
                sqlCon = new SQLiteConnection(connectionString);
                sqlCon.Open();
            }
        }

        private bool IsSqlConOpened
        {
            get
            {
                return (null != sqlCon) && (sqlCon.State == ConnectionState.Open);
            }
        }

        private static IList<string> sqlHistoryList = new List<string>();

        public bool InsertHistory(HistoryInfo item)
        {
            if (IsSqlConOpened)
            {
                string sql = string.Format(" insert into History (RoundId,Result,Stake,Amount)" +
                                           " values (\'{0}\',{1},{2},{3}) ",
                                          item.RoundId, item.Result, item.Stake, item.Amount);

                if (!string.IsNullOrEmpty(sql))
                {
                    return RunSql(sql);
                }
            }

            return false;
        }
        
        private bool RunSql(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                try
                {
                    using (SQLiteCommand cmd = sqlCon.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(string.Format("执行Insert sql时出错：{0}", ex.Message));
                }
            }
            return false;
        }

        private bool InsertData(params string[] sqls)
        {
            if ((sqls == null) || (sqls.Length == 0))
            {
                return false;
            }

            try
            {
                using (SQLiteTransaction trans = sqlCon.BeginTransaction())
                {
                    using (SQLiteCommand cmd = sqlCon.CreateCommand())
                    {
                        foreach (string sql in sqls)
                        {
                            cmd.CommandText = sql;
                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                    trans.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("执行Insert sql时出错：{0}", ex.Message));
            }
            return false;
        }

        public void Dispose()
        {
            if (this.IsSqlConOpened)
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
        }
    }
}
