using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WebApiHostService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main(string[] args)
        {
            RunDebug();
            StartService();
        }

        private static void StartService()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WebApiHostServiceService()
            };
            ServiceBase.Run(ServicesToRun);
        }

        [Conditional("DEBUG")]
        private static void RunDebug()
        {
            new WebApiHostServiceService().Start();
            Console.WriteLine("启动成功");
            Console.ReadLine();
        }
    }
}
