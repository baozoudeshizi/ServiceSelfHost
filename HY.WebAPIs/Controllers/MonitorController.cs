using HY.Common;
using HY.Common.Helpers;
using HY.Models.Model;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Web.Http;
using WebAPIs;
using static HY.Models.Model.GlobalConfig;

namespace HY.WebAPIs.Controllers
{
    /// <summary>
    /// 监视器
    /// </summary>
    public class MonitorController : ApiController
    {
        private readonly string configPath = Path.GetFullPath("..\\DebugDir\\config.json");

        /// <summary>
        /// 监视器
        /// </summary>
        public MonitorController()
        {

        }

        /// <summary>
        /// 获取监视的配置文件
        /// </summary>
        /// <returns></returns>
        public MonitorConfig GetMonitorConfig()
        {
            //string configPath = Path.GetFullPath("..\\..\\..\\DebugDir\\config.json");
            MonitorConfig monitor = JsonConvert.DeserializeObject<MonitorConfig>(ConfigHelper.GetConfig("monitor",configPath));
            return monitor;
        }


        /// <summary>
        /// 设置监视的配置文件
        /// </summary>
        /// <returns></returns>
        public TData<bool> SetMonitorConfig(MonitorConfig monitor)
        {
            //string configPath = Path.GetFullPath("..\\DebugDir\\config.json");
            bool bIsSuccess = ConfigHelper.SetConfig("monitor", monitor, configPath, out string errMsg);
            if (bIsSuccess)
            {
                return TDataRet<bool>.Success(bIsSuccess,"设置成功");
            }
            else
            {
                return TDataRet<bool>.Failure(bIsSuccess,errMsg);
            }
        }


        /// <summary>
        /// 重新手动重启监视任务
        /// </summary>
        /// <returns></returns>
        public TData<string> ReStartMonitorTask()
        {
            try
            {
                MonitorTask.GetMonitorTask().Start();
                return TDataRet<string>.Success("启动成功！");
            }
            catch(Exception e)
            {
                return TDataRet<string>.Failure(e.Message);
            }
        }


        /// <summary>
        /// 向所有人广播消息
        /// </summary>
        /// <param name="data">发送的信息</param>
        public void SendData(object data)
        {
            //PersistentConnection模式 
            var perConnection = GlobalHost.ConnectionManager.GetConnectionContext<SmartPersistentConnection>();
            perConnection.Connection.Broadcast(data);
        }

    }



}
