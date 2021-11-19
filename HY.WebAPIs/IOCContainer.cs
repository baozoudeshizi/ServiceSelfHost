using Autofac;
using Autofac.Integration.WebApi;
using HY.Common;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace WebAPIs
{
    /// <summary>
    ///  .NET Framework WebApi容器
    /// </summary>
    public class IOCContainer
    {
        /// <summary>
        /// IoC容器
        /// </summary>
        public static IContainer Instance;

        /// <summary>
        /// 初始化Api容器
        /// </summary>
        /// <param name="func"></param>
        public static System.Web.Http.Dependencies.IDependencyResolver Init(Func<ContainerBuilder, ContainerBuilder> func = null)
        {
            //新建容器构建器，用于注册组件和服务
            var builder = new ContainerBuilder();
            //注册组件
            MyBuild(builder);
            func?.Invoke(builder);
            //利用构建器创建容器
            Instance = builder.Build();

            //返回针对WebApi的AutoFac解析器
            return new AutofacWebApiDependencyResolver(Instance);
        }

        /// <summary>
        /// 注册组件
        /// </summary>
        /// <param name="builder"></param>
        public static void MyBuild(ContainerBuilder builder)
        {

            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var servicesDllFile = Path.Combine(basePath, "HY.Services.dll");
            var commonDllFile = Path.Combine(basePath, "HY.Common.dll");

            if (!(File.Exists(servicesDllFile)) || !(File.Exists(commonDllFile)))
            {
                var msg = "路径为："+ servicesDllFile+ " ,但未在该路径下找到该Dll";
                throw new Exception(msg);
            }
            //注册ApiController
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // 获取 Service.dll 程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerRequest();

            //注册泛型仓储
            //builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<HttpRequestMessage>();
            builder.RegisterType<AspNetUser>().As<IUser>()
             .AsImplementedInterfaces()
             .OwnedByLifetimeScope();


            ////注册仓储 && Service
            //builder.RegisterAssemblyTypes(assemblies)//程序集内所有具象类（concrete classes）
            //    .Where(cc => cc.Name.EndsWith("Repository") |//筛选
            //                 cc.Name.EndsWith("Service"))
            //    .PublicOnly()//只要public访问权限的
            //    .Where(cc => cc.IsClass)//只要class型（主要为了排除值和interface类型）
            //    .AsImplementedInterfaces();//自动以其实现的所有接口类型暴露（包括IDisposable接口）

            //注册泛型仓储
            //builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>));

            //注册ApiController
            //方法1：自己根据反射注册
            //Assembly[] controllerAssemblies = assemblies.Where(x => x.FullName.Contains(".NetFrameworkApi")).ToArray();
            //builder.RegisterAssemblyTypes(assemblysServices)
            //    .Where(cc => cc.Name.EndsWith("Controller"))
            //    .AsSelf();
            //方法2：用AutoFac提供的专门用于注册ApiController的扩展方法
            //Assembly apiAssembly = assemblysServices.FirstOrDefault(x => x.FullName.Contains(".NetFrameworkApi"));


        }

    }
}
