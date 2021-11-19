using HY.Common;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;

namespace HY.WebAPIs.Controllers
{
    /// <summary>
    /// 监视器
    /// </summary>
    public class MonitorController : ApiController
    {
        FileSystemWatcher fsWatcher = new FileSystemWatcher();
        public MonitorController()
        {

        }

        public void Start(string Path, bool IsIncludeSubdirectories = false)
        {
            //开始监视
            fsWatcher.Path = Path;  //设置监控路径
            fsWatcher.IncludeSubdirectories = IsIncludeSubdirectories;  //是否监视指定路径中的子目录。
            fsWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size|NotifyFilters.LastWrite; //监视的几个类型
            fsWatcher.Created += new FileSystemEventHandler(fileSystemWatcher_EventHandle);
            fsWatcher.Deleted += new FileSystemEventHandler(fileSystemWatcher_EventHandle);
            fsWatcher.Changed += new FileSystemEventHandler(fileSystemWatcher_EventHandle);
            fsWatcher.Renamed += new RenamedEventHandler(fileSystemWatcher_Renamed);
            fsWatcher.EnableRaisingEvents = true;

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

        public void read(string path)
        {
            string[] line = File.ReadAllLines(path);
            int startLine = 0;
            //遍历所有行
            for (int i = 0; i < line.Length; i++)
            {
                if(line[i].Trim().StartsWith("Target Compounds") && line[i].Trim().EndsWith("Qvalue"))
                {
                    startLine = i;
                    break;
                }

            }

            //打印所有行
            for (int i = startLine; i < line.Length-1; i++)
            {
                string l = line[i];
                string[] aa = l.Split(new char[] {'\t'},StringSplitOptions.None);
                Console.WriteLine(line[i]);
                Console.WriteLine(aa.Length);
            }

        }
    }
}
