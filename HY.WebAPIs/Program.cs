using Microsoft.Owin.Hosting;
using System;

namespace WebAPIs
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new SmartLabService()
            //};
            //ServiceBase.Run(ServicesToRun);
            var baseAddress = string.Format("http://*:1234/");
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Server started");
                Console.ReadLine();
            }
        }
    }
}
