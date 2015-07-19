using Aliyun.OpenServices;

using Aliyun.OpenServices.OpenStorageService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Game28.DB;

namespace Game28
{
    class OSSHelper
    {
        static string bucket = "stefantest";
        static OssClient ossClient;

        static OSSHelper()
        {
            if (ossClient == null)
            {
                string accessKeyId = "ASIceatqLayAkgJp";
                string accessKeySecret = "E9wjh0SSLyG14oYBvo1LJT7f8kO5nl";
                ossClient = new OssClient(accessKeyId, accessKeySecret);
            }
        }

        /// <summary>
        /// upload the txt rule config to oss
        /// </summary>
        public static void UploadRuleToOSS()
        {
            Stream fs = RuleFileHelper.GetSpeed28RuleFileStream();
            Upload(fs, "speed28rule");
        }

        public static bool Upload(Stream stream, string name)
        {
            var metadata = new ObjectMetadata();
            metadata.UserMetadata.Add("name", name);
            metadata.CacheControl = "No-Cache";
            metadata.ContentLength = stream.Length;
            bool result = false;
            try
            {
                using (stream)
                {
                    var obj = ossClient.PutObject(bucket, name, stream, metadata);
                    result = obj != null;
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public static Stream Download(string cloudName)
        {
            GetObjectRequest req = new GetObjectRequest(bucket, cloudName);
            OssObject obj = ossClient.GetObject(req);

            Stream result = null;
            if (obj != null)
            {
                result = obj.Content;
            }
            return result;
        }


        public static void UploadFile(string path, string newName)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path) || string.IsNullOrEmpty(newName))
            {
                throw new ArgumentNullException();
            }

            string newPath = DBHelper.GetDBPath(newName);
            if (File.Exists(newPath))
            {
                File.Delete(newPath);
            }
            File.Copy(path, newPath);

            if (File.Exists(newPath))
            {
                FileStream stream = new FileStream(newPath, FileMode.Open, FileAccess.Read);
                Upload(stream, newName);
            }

        }

        public static Rule GetRuleFromOss()
        {
            OssObject obj = null;
            try
            {
                obj = ossClient.GetObject(bucket, RuleFileHelper.Speed28RuleFile);
            }
            catch (Exception ex)
            {
            }
            string ruleStr = string.Empty;
            if (obj != null)
            {
                using (StreamReader sr = new StreamReader(obj.Content))
                {
                    ruleStr = sr.ReadToEnd();
                }
            }

            Rule rule = null;
            if (!string.IsNullOrEmpty(ruleStr))
            {
                string[] items = ruleStr.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (items != null && items.Length == 2)
                {
                    string[] valuesStr = items[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (valuesStr.Length == Speed28.MaxLen)
                    {
                        int[] values = new int[Speed28.MaxLen];
                        for (int i = 0; i < Speed28.MaxLen; i++)
                        {
                            values[i] = int.Parse(valuesStr[i]);
                        }

                        rule = new Rule(items[0], values);
                    }
                }
            }

            return rule;
        }
    }
}
