using Autofac;
using Autofac.Integration.WebApi;
using HY.Common;
using HY.WebAPIs.Controllers;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace WebAPIs
{
    /// <summary>
    ///  开启监控任务
    /// </summary>
    public class MonitorTask
    {

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            MonitorController monitor = new MonitorController();
            string path = ConfigurationManager.AppSettings["Path"];
            Console.WriteLine("开始监控！ 路径为："+path);
            monitor.Start(path);
        }


        //public static string GetConnectionString(string value)
        //{
        //    string config = "config.json";
        //    StreamReader sr = File.OpenText(config);
        //    string jsonWordTemplate = sr.ReadToEnd();
        //    sr.Close();
        //    JObject jConfig = JObject.Parse(jsonWordTemplate);
        //    return jConfig[value].ToString();
        //}


    }
}
