using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Text;
using Game28.Model;
using System.Linq;

namespace Game28.DB
{
    public class DBHelper : IDisposable
    {
        public const string DBName = @"DB\DB.db3";
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


        IList<string> allRoundIdList = new List<string>();
        const int CacheCapacity = 1000;

        private DBHelper()
        {
            string dbPath = GetDBPath(DBName);
            LoadDB(dbPath);
        }

        public DBHelper(string dbPath)
        {
            LoadDB(dbPath);
        }

        private void LoadDB(string dbPath)
        {
            if (File.Exists(dbPath))
            {
                string connectionString = string.Format("data source={0}", dbPath);
                sqlCon = new SQLiteConnection(connectionString);
                sqlCon.Open();
                if (IsSqlConOpened)
                {
                    allRoundIdList = GetAllRoundId(CacheCapacity);
                }
            }
        }

        public static string GetDBPath(string dbName)
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(appPath, dbName);
            return dbPath;
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
                StringBuilder sb = new StringBuilder();
                int count = 0;
                foreach (var item in list)
                {
                    if (IsContainRoundId(item.RoundId))
                    {
                        continue;
                    }
                    AddRoundIdToCache(item.RoundId);
                    string sql = string.Format(" insert into History (RoundId,Result,Stake,Amount,date,totalamount,winnernum)" +
                                               " values (\'{0}\',{1},{2},{3},\'{4}\',{5},{6}); ",
                                              item.RoundId, item.Result, item.Stake, item.Amount,
                                              item.Date, item.TotalAmount, item.WinnerNum);
                    sb.AppendLine(sql);
                    count++;
                }
                RunSql(sb.ToString());

                Debug.WriteLine(string.Format("/--- All history num:{0}, actual insert num:{1} ---/", list.Count, count));
            }
            return false;
        }

        public bool InsertHistory(HistoryInfo item)
        {
            int i = 0;
            if (IsSqlConOpened && item != null && !IsContainRoundId(item.RoundId))
            {
                string sql = string.Format(" insert into History (RoundId,Result,Stake,Amount,date,totalamount,winnernum)" +
                                           " values (\'{0}\',{1},{2},{3},\'{4}\',{5},{6}) ",
                                          item.RoundId, item.Result, item.Stake, item.Amount,
                                          item.Date, item.TotalAmount, item.WinnerNum);
                i++;
                AddRoundIdToCache(item.RoundId);
                if (!string.IsNullOrEmpty(sql))
                {
                    return RunSql(sql);
                }
            }

            Debug.WriteLine(string.Format("/--- All history num:{0}, actual insert num:{1} ---/", 1, i));
            return false;
        }

        private void AddRoundIdToCache(string roundId)
        {
            if (!string.IsNullOrEmpty(roundId))
            {
                allRoundIdList.Add(roundId);
                if (allRoundIdList.Count > CacheCapacity)
                {
                    string min = allRoundIdList.Min(item => item);
                    allRoundIdList.Remove(min);
                }
            }
        }

        public IList<HistoryInfo> GetByRows(int rows = 0)
        {
            IList<HistoryInfo> result = new List<HistoryInfo>();

            string sql = "select * from history order by RoundId desc";
            if (rows > 0)
            {
                sql = string.Format("{0} limit {1}", sql, rows);
            }

            using (SQLiteCommand cmd = new SQLiteCommand(sql, sqlCon))
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
                        item.Date = reader["Date"].ToString();
                        item.TotalAmount = long.Parse(reader["totalamount"].ToString());
                        item.WinnerNum = int.Parse(reader["winnernum"].ToString());
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        public IList<HistoryInfo> GetByRows(string where)
        {
            IList<HistoryInfo> result = new List<HistoryInfo>();

            string sql = string.Format("select * from history {0} order by RoundId desc", where);

            using (SQLiteCommand cmd = new SQLiteCommand(sql, sqlCon))
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
                        item.Date = reader["Date"].ToString();
                        item.TotalAmount = long.Parse(reader["totalamount"].ToString());
                        item.WinnerNum = int.Parse(reader["winnernum"].ToString());
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        public bool IsContainRoundId(string roundId)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(roundId))
            {
                result = allRoundIdList.Contains(roundId);
            }

            return result;
        }

        public string GetMaxRoundId()
        {
            string result = string.Empty;
            using (SQLiteCommand cmd = new SQLiteCommand("select max(roundid)as maxId from history", sqlCon))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader[0].ToString();
                    }
                }
            }
            return result;
        }

        public IList<string> GetAllRoundId(int count = 0)
        {
            IList<string> result = new List<string>();

            string sql = "select RoundId from history order by RoundId desc";
            if (count > 0)
            {
                sql = string.Format("{0} limit {1}", sql, count);
            }


            using (SQLiteCommand cmd = new SQLiteCommand(sql, sqlCon))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string item = reader["RoundId"].ToString();
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
