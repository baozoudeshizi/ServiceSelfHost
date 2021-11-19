using Microsoft.Owin.Hosting;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using WebAPIs;

namespace WebApiHostService
{
    public partial class WebApiHostServiceService : ServiceBase
    {
        #region 构造函数
        public WebApiHostServiceService()
        {
            InitializeComponent();
        }
        #endregion

        #region OnStart 启动服务
        protected override void OnStart(string[] args)
        {
            int port = int.Parse(ConfigurationManager.AppSettings["WebApiServicePort"]);
            StartOptions options = new StartOptions();
            options.Urls.Add("http://127.0.0.1:" + port);
            options.Urls.Add("http://localhost:" + port);
            options.Urls.Add("http://+:" + port);
            WebApp.Start<Startup>(options);
            //LogUtil.Log("Web API 服务 启动成功");
        }
        #endregion

        #region OnStop 停止服务
        protected override void OnStop()
        {
            //LogUtil.Log("Web API 服务 停止成功");
            Thread.Sleep(100); //等待一会，待日志写入文件
        }
        #endregion

        #region Start 启动服务
        public void Start()
        {
            OnStart(null);
        }
        #endregion

    }
}
