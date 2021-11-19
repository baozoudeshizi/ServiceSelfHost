using HY.Common;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Converters;
using Owin;
using System;
using System.Web.Http;
using WebAPIs.App_Start;

namespace WebAPIs
{
    /// <summary>
    /// 开始类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {

            //HttpConfiguration config = GlobalConfiguration.Configuration;
            HttpConfiguration config = new HttpConfiguration();

            //用永久连接时，无法获得用户的信息，尝试用Hub
            app.MapSignalR<SmartPersistentConnection>("/smartPreConnection");
            //app.MapSignalR("/smartLabHub", new HubConfiguration());

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "RPCApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //解决返回时间带T的问题
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            });

            app.UseWebApi(config);
            SwaggerConfig.Register(config);
            //初始化配置AutoMapper
            AutoMapper.Mapper.Initialize((cfg) =>
            {
                cfg.AddProfile<AutoMapProfile>();
            });
            //appBuilder.MapHubs();


            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(30);
            //config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            //跨域
            app.UseCors(CorsOptions.AllowAll);


            //初始化容器，并返回适用于WebApi的AutoFac解析器
            System.Web.Http.Dependencies.IDependencyResolver autoFacResolver = IOCContainer.Init();
            //将AutoFac解析器设置为系统DI解析器
            config.DependencyResolver = autoFacResolver;


            MonitorTask.Init(); //开始监控
            //AddHttpContextSetup();
        }


    }
}
