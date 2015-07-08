﻿using System;
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


        IList<string> allRoundIdList = new List<string>();
        private DBHelper()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(appPath, DBName);
            if (File.Exists(dbPath))
            {
                string connectionString = string.Format("data source={0}", dbPath);
                sqlCon = new SQLiteConnection(connectionString);
                sqlCon.Open();
                if (IsSqlConOpened)
                {
                    allRoundIdList = GetAllRoundId();
                }
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
                    if (allRoundIdList.Contains(item.RoundId))
                    {
                        continue;
                    }
                    AddRoundIdToCache(item.RoundId);
                    string sql = string.Format(" insert into History (RoundId,Result,Stake,Amount,date,totalamount,winnernum)" +
                                               " values (\'{0}\',{1},{2},{3},\'{4}\',{5},{6}) ",
                                              item.RoundId, item.Result, item.Stake, item.Amount,
                                              item.Date, item.TotalAmount, item.WinnerNum);
                    sqlList.Add(sql);
                }
                RunSql(sqlList.ToArray());

                Debug.WriteLine(string.Format("/--- All history num:{0}, actual insert num:{1} ---/", list.Count, sqlList.Count));
            }
            return false;
        }

        public bool InsertHistory(HistoryInfo item)
        {
            int i = 0;
            if (IsSqlConOpened && item != null && !allRoundIdList.Contains(item.RoundId))
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
                if (allRoundIdList.Count > 100)
                {
                    allRoundIdList.RemoveAt(0);
                }
            }
        }

        public IList<HistoryInfo> GetAll()
        {
            IList<HistoryInfo> result = new List<HistoryInfo>();

            using (SQLiteCommand cmd = new SQLiteCommand("select * from history order by RoundId desc", sqlCon))
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

        public IList<string> GetAllRoundId()
        {
            IList<string> result = new List<string>();

            using (SQLiteCommand cmd = new SQLiteCommand("select RoundId from history order by RoundId desc limit 50", sqlCon))
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
