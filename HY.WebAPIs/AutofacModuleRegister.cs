using Autofac;
using System;
using System.IO;
using System.Reflection;

namespace WebAPIs
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public class AutofacModuleRegister:Autofac.Module
    {
        /// <summary>
        /// 重写接在部分
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
           var basePath = AppDomain.CurrentDomain.BaseDirectory;
           //var basePath = AppContext.BaseDirectory;
            //builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();


            #region 带有接口层的服务注入

            var servicesDllFile = Path.Combine(basePath, "HY.Services.dll");

            if (!(File.Exists(servicesDllFile) ))
            {
                var msg = "service.dll 丢失，因为项目解耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。";
                throw new Exception(msg);
            }



            // AOP 开关，如果想要打开指定的功能，只需要在 appsettigns.json 对应对应 true 就行。
            //var cacheType = new List<Type>();
            //if (Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "Enabled" }).ObjToBool())
            //{
            //    builder.RegisterType<SmartOfficeRedisCacheAOP>();
            //    cacheType.Add(typeof(SmartOfficeRedisCacheAOP));
            //}
            //if (Appsettings.app(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ObjToBool())
            //{
            //    builder.RegisterType<SmartOfficeCacheAOP>();
            //    cacheType.Add(typeof(SmartOfficeCacheAOP));
            //}
            //if (Appsettings.app(new string[] { "AppSettings", "TranAOP", "Enabled" }).ObjToBool())
            //{
            //    builder.RegisterType<SmartOfficeTranAOP>();
            //    cacheType.Add(typeof(SmartOfficeTranAOP));
            //}
            //if (Appsettings.app(new string[] { "AppSettings", "LogAOP", "Enabled" }).ObjToBool())
            //{
            //    builder.RegisterType<SmartOfficeLogAOP>();
            //    cacheType.Add(typeof(SmartOfficeLogAOP));
            //}

            //builder.RegisterGeneric(typeof(MongoBaseRepository<>)).As(typeof(IMongoBaseRepository<>)).InstancePerDependency();//注册仓储
            //builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();//注册仓储


            // 获取 Service.dll 程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()
                      .InstancePerDependency();

                      //.EnableInterfaceInterceptors();//引用Autofac.Extras.DynamicProxy;
                      //.InterceptedBy(cacheType.ToArray());//允许将拦截器服务的列表分配给注册。

            // 获取 Repository.dll 程序集服务，并注册
            //var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            //builder.RegisterAssemblyTypes(assemblysRepository)
            //       .AsImplementedInterfaces()
            //       .InstancePerDependency();

            #endregion

            #region 没有接口层的服务层注入

            //因为没有接口层，所以不能实现解耦，只能用 Load 方法。
            //注意如果使用没有接口的服务，并想对其使用 AOP 拦截，就必须设置为虚方法
            //var assemblysServicesNoInterfaces = Assembly.Load("SmartOffice.Services");
            //builder.RegisterAssemblyTypes(assemblysServicesNoInterfaces);

            #endregion


        }
    }
}
