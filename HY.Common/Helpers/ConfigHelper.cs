using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HY.Common.Helpers
{
    public static class ConfigHelper
    {
        public static string GetAllConfig(string configPath)
        {
            try
            {
                StreamReader sr = File.OpenText(configPath);
                string config = sr.ReadToEnd();
                sr.Close();
                JObject jConfig = JObject.Parse(config);
                return jConfig.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //获取配置文件中的值
        public static string GetConfig(string key,string configPath)
        {
            try
            {
                //string configPath = Path.GetFullPath("..\\..\\..\\DebugDir\\config.json");
                StreamReader sr = File.OpenText(configPath);
                string config = sr.ReadToEnd();
                sr.Close();
                JObject jConfig = JObject.Parse(config);
                return jConfig[key].ToString();
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        //修改配置文件的内容
        public static bool SetConfig(string currentObj,object configObj,string configPath,out string errMsg)
        {
            try
            {
               string aa =  Directory.GetCurrentDirectory();
                //dynamic newJsonObj = JsonConvert.DeserializeObject(sNewObj);
                string sAllConfig = GetAllConfig(configPath);//获取整个配置
                JObject configJObject = JObject.Parse(sAllConfig);
                configJObject[currentObj] = JObject.Parse(JsonConvert.SerializeObject(configObj));
                string config = JsonConvert.SerializeObject(configJObject, Formatting.Indented);
                File.WriteAllText(configPath, config);
                errMsg = "";
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("设置错误：" + e.Message);
                errMsg = e.Message;
                return false;
            }
        }
    }
}
