using HY.Common.Helpers;
using HY.Models.Model;
using Newtonsoft.Json;
using System;
using System.IO;

namespace WebAPIs
{
    /// <summary>
    ///  开启监控任务
    /// </summary>
    public class MonitorTask
    {
        private static readonly FileSystemWatcher fsWatcher = new FileSystemWatcher();
        private readonly string configPath = Path.GetFullPath("..\\DebugDir\\config.json");


        private static MonitorTask monitorTask = null;
        /// <summary>
        /// 单例模式实现MonitorTask，暂未考虑线程安全
        /// </summary>
        public static MonitorTask GetMonitorTask()
        {
            if(monitorTask == null)
            {
                monitorTask = new MonitorTask();
            }
            return monitorTask;
        }



        /// <summary>
        /// 开启监控
        /// </summary>
        public void Start()
        {
            //var aa = GlobalConfig.Monitor.Path;
            //var bb = GlobalConfig.Monitor.IsIncludeSub;
            MonitorConfig config =initConfig();
            //开始监视
            try
            {
                fsWatcher.Path = config.Path;  //设置监控路径
                fsWatcher.IncludeSubdirectories = config.IsIncludeSub;  //是否监视指定路径中的子目录。
                fsWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size | NotifyFilters.LastWrite; //监视的几个类型
                fsWatcher.Created += new FileSystemEventHandler(fileSystemWatcher_EventHandle);
                fsWatcher.Deleted += new FileSystemEventHandler(fileSystemWatcher_EventHandle);
                fsWatcher.Changed += new FileSystemEventHandler(fileSystemWatcher_EventHandle);
                fsWatcher.Renamed += new RenamedEventHandler(fileSystemWatcher_Renamed);
                fsWatcher.EnableRaisingEvents = true;
            }
            catch(Exception e)
            {
                throw new Exception("监视失败：" + e.Message);
            }

        }

        /// <summary>
        /// 初始化
        /// </summary>
        private MonitorConfig initConfig()
        {
            MonitorConfig config = JsonConvert.DeserializeObject<MonitorConfig>(ConfigHelper.GetConfig("monitor",configPath));
            Console.WriteLine("开始监控！ 路径为：" + config.Path);
            return config;
            //Start(config.Path,config.IsIncludeSub);
        }


        private void fileSystemWatcher_EventHandle(object sender, FileSystemEventArgs fsEvent)
        {
            if (fsEvent.ChangeType == WatcherChangeTypes.Created)
            {
                Console.WriteLine(fsEvent.Name + " 文件被建立了！");
            }
            else if (fsEvent.ChangeType == WatcherChangeTypes.Deleted)
            {
                Console.WriteLine(fsEvent.Name + " 文件被删除了！");
            }
            else if (fsEvent.ChangeType == WatcherChangeTypes.Changed)
            {
                Console.WriteLine(fsEvent.Name + " 文件被改变了！");
                read(fsEvent.FullPath);
            }
            else
            {
                Console.WriteLine("未知操作类型！");
            }
        }

        private void fileSystemWatcher_Renamed(object sender, RenamedEventArgs fsEvent)
        {
            Console.WriteLine("文件被重命名：" + "原名： " + fsEvent.OldName + "; 现在名字：" + fsEvent.Name);
            read(fsEvent.FullPath);
        }

        public void read(string path)
        {
            string[] line = File.ReadAllLines(path);
            int startLine = 0;
            //遍历所有行
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i].Trim().StartsWith("Target Compounds") && line[i].Trim().EndsWith("Qvalue"))
                {
                    startLine = i;
                    break;
                }

            }

            //打印所有行
            for (int i = startLine; i < line.Length - 1; i++)
            {
                string l = line[i];
                string[] aa = l.Split(new char[] { '\t' }, StringSplitOptions.None);
                Console.WriteLine(line[i]);
                Console.WriteLine(aa.Length);
            }

        }


    }
}
