using Aliyun.OpenServices;

using Aliyun.OpenServices.OpenStorageService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
            var metadata = new ObjectMetadata();
            metadata.UserMetadata.Add("speed28rule", "test");
            metadata.CacheControl = "No-Cache";
            metadata.ContentLength = fs.Length;
            try
            {
                using (fs)
                {
                    var result = ossClient.PutObject(bucket, RuleFileHelper.Speed28RuleFile, fs, metadata);
                }
            }
            catch (Exception)
            {
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
