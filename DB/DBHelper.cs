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
        string createTableSql = @"CREATE TABLE IF NOT EXISTS  HISTORY (id integer primary key AUTOINCREMENT,
                                                        roundid varchar(20),
                                                        result integer,
                                                        stake long,
                                                        amount long,
                                                        date varchar(20),
                                                        totalamount long,
                                                        winnernum int
                                                        );
                                  CREATE UNIQUE INDEX IF NOT EXISTS idx_history on history(roundid );";

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


        public bool InsertHistory(IList<HistoryInfo> list)
        {
            if (list == null || list.Count == 0)
            {
                return false;
            }

            if (IsSqlConOpened)
            {
                IList<string> sqlList = new List<string>();
                foreach (var item in list)
                {
                    string sql = string.Format(" insert into History (RoundId,Result,Stake,Amount,date,totalamount,winnernum)" +
                                               " values (\'{0}\',{1},{2},{3},\'{4}\',{5},{6}) ",
                                              item.RoundId, item.Result, item.Stake, item.Amount,
                                              item.Date, item.TotalAmount, item.WinnerNum);
                    sqlList.Add(sql);
                }
                RunSql(sqlList.ToArray());
            }

            return false;
        }

        public bool InsertHistory(HistoryInfo item)
        {
            if (IsSqlConOpened)
            {
                string sql = string.Format(" insert into History (RoundId,Result,Stake,Amount,date,totalamount,winnernum)" +
                                           " values (\'{0}\',{1},{2},{3},\'{4}\',{5},{6}) ",
                                          item.RoundId, item.Result, item.Stake, item.Amount,
                                          item.Date, item.TotalAmount, item.WinnerNum);

                if (!string.IsNullOrEmpty(sql))
                {
                    return RunSql(sql);
                }
            }

            return false;
        }

        public IList<HistoryInfo> GetAll()
        {
            IList<HistoryInfo> result = new List<HistoryInfo>();

            using (SQLiteCommand cmd = new SQLiteCommand("select * from history", sqlCon))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HistoryInfo item = new HistoryInfo();
                        item.RoundId = reader["RoundId"].ToString();
                        item.Result = int.Parse(reader["Result"].ToString());
                        item.Stake = long.Parse(reader["Stake"].ToString());
                        item.Amount = long.Parse(reader["Amount"].ToString());
                        item.Date = reader["result"].ToString();
                        item.TotalAmount = long.Parse(reader["totalamount"].ToString());
                        item.WinnerNum = int.Parse(reader["winnernum"].ToString());
                        result.Add(item);
                    }
                }
            }

            return result;
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

        private bool RunSql(params string[] sqls)
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
