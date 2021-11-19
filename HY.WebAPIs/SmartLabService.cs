using Microsoft.Owin.Hosting;
using MobileServer.Common;
using SmartLabServiceServer;
using WebAPIs.App_Start;
using System;
using System.ServiceProcess;
using Wayee.CWS.AcquisitionServiceClient;
using Wayee.CWS.ReportServiceClientNew;
using Wayee.Services.Analysis;
using Wayee.Services.DataCenterServiceClient;

namespace WebAPIs
{
    public partial class SmartLabService : ServiceBase
    {
        /// <summary>
        /// 采集服务客户端
        /// </summary>
        //public static AcquisitionServiceClient AcClient;

        /// <summary>
        /// 报告客户端
        /// </summary>
        public static ReportServiceClient ReportClient = new ReportServiceClient();

        /// <summary>
        /// 分析服务客户端
        /// </summary>
        public static AnalysisServiceClient AnClient;

        private IDisposable hostObject;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmartLabService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            //这个地方不设置，发布的软件，DataCenterServiceClient为null
            DataCenterServiceClient.DataCenterAddress = $"net.tcp://127.0.0.1:9800/DataCenterService";
            //ReportServiceClient reportService1 = new ReportServiceClient();
            //AcClient = new AcquisitionServiceClient(new ChromSysEventCallBack(), "net.tcp://127.0.0.1:9200/AcquisitionService");
            var baseAddress = string.Format("http://*:9000/");
            //启动WebApi
            hostObject = WebApp.Start<Startup>(baseAddress);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        protected override void OnStop()
        {
            //注意释放
            hostObject.Dispose();
        }

        
    }
}
