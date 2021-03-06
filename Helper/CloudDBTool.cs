﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game28.DB;
using System.IO;
using System.Windows.Forms;

namespace Game28.Helper
{
    /// <summary>
    /// DB backup or restor to cloud
    /// </summary>
    class CloudDBTool
    {
        public void Bakcup(string cloudName)
        {
            string dbPath = DBHelper.GetDBPath(DBHelper.DBName);

            Action action = new Action(() =>
            {
                if (OSSHelper.UploadFile(dbPath, cloudName))
                {
                    MessageBox.Show("云备份成功");
                }
            });
            action.BeginInvoke((ar) => action.EndInvoke(ar), action);
        }

        public void Restore(string cloudName)
        {
            Action action = new Action(() =>
            {
                if (DownloadFromCloud(cloudName))
                {
                    MergeDB(cloudName);
                    MessageBox.Show("云还原成功");
                }
            });
            action.BeginInvoke((ar) => action.EndInvoke(ar), action);
        }

        private bool DownloadFromCloud(string cloudName)
        {
            bool result = false;
            Stream stream = OSSHelper.Download(cloudName);

            string localPath = DBHelper.GetDBPath(cloudName);
            if (File.Exists(localPath))
            {
                File.Delete(localPath);
            }

            byte[] buffer = new byte[1024];
            int len = 0;
            using (stream)
            {
                using (FileStream fs = new FileStream(localPath, FileMode.CreateNew))
                {
                    while ((len = stream.Read(buffer, 0, 1024)) > 0)
                    {
                        fs.Write(buffer, 0, len);

                        result = true;
                    }
                }
            }
            return result;
        }

        private bool MergeDB(string cloudName)
        {
            bool result = false;
            string localPath = DBHelper.GetDBPath(cloudName);
            DBHelper cloudDB = new DBHelper(cloudName);
            DBHelper localDb = DBHelper.Instance;
            var rows = cloudDB.GetByRows();
            using (cloudDB)
            {
                if (rows.Count > 0)
                {
                    result = localDb.Replace(rows);
                }
            }

            File.Delete(localPath);
            return result;
        }
    }
}
