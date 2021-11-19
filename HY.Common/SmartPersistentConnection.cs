using Microsoft.AspNet.SignalR;
using HY.Common.Helpers;
using HY.Models.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HY.Common
{
    /// <summary>
    /// SignalR的模型之一，持久长连接，与WebSockt类似，比Hub方式要更底层一点
    /// 1. 长连接，给用户谱图数据
    /// 2. 维护一个连接的用户列表，因为一个用户可能从多个端进入
    /// </summary>
    public class SmartPersistentConnection : PersistentConnection
    {
        //TData d = new TData();
        /// <summary>
        /// 当前连接数
        /// </summary>
        private static int _connections = 0;

        private static ConcurrentDictionary<string, string> Clients = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 通过用户ID，获取所有的长连接ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<string> getClients(string userID)
        {
            //dic.Where(q => q.Value == "2").Select(q => q.Key);
            List<string> connectionIds = Clients.Where(q => q.Value == userID).Select(q => q.Key).ToList();
            return connectionIds;
        }

        //protected override bool AuthorizeRequest(IRequest request)
        //{
        //    return request.User.Identity.IsAuthenticated;
        //}

        /// <summary>
        /// 客户端建立连接时会自动触发该函数  
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        protected override async Task OnConnected(IRequest request, string connectionId)
        {
            string userID = JWTTokenHelper.DecodeJTWToken(request.Headers["myToken"]).ID;
            if (!Clients.ContainsKey(userID))
            {
                Console.WriteLine($"++ {userID} logged in");
                Clients.TryAdd(userID, connectionId);
            }
            //else
            //{
            //    Clients.TryRemove(userID, out connectionId); //将前一个连接
            //}
            //原子操作,防止多条现成同时+1而只做一次变化
            Interlocked.Increment(ref _connections);
            //await Connection.Send(connectionId, "Hi, " + connectionId + "!");
            TData<float> d = new TData<float>();
            d.Code = 201;
            d.Msg = "Connected Success";
            d.Data = 10;
            await Connection.Broadcast(d);
            //Thread thread = new Thread(new ThreadStart(Method1));
            //thread.Start();

        }

        void Method1()
        {
            while (true)
            {
                Console.WriteLine(DateTime.Now.ToString() + "_" + Thread.CurrentThread.ManagedThreadId.ToString());
                Thread.CurrentThread.Join(3000);//阻止设定时间
                Thread1();
            }
        }

        void Thread1()
        {
            TData<float> d = new TData<float>();
            Console.WriteLine($"++ {DateTime.Now.ToString()} logged in");
            d.Code = 400;
            d.Msg = "报错测试";
            Connection.Broadcast(d);
        }

        /// <summary>
        /// 服务端接收到消息时，会触发该函数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connectionId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            //d.Code = 1;
            //一对一发送消息
            //var model = JsonConvert.DeserializeObject<TempData>(data);
            //return Connection.Send(model.receiveId, model.msg);
            //return Connection.Broadcast(d);
            var message = connectionId + ">> " + data;
            return Connection.Broadcast(message);
        }


        /// <summary>
        /// 断连时会自动触发该函数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connectionId"></param>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            //告诉所有人该用户退出了(包括自己，也可以配置排除一些用户)
            //d.Code = 201;
            //d.Msg = $"用户{connectionId}已经退出";
            //Connection.Broadcast(d);
            //return base.OnDisconnected(request, connectionId, stopCalled);

            //原子操作,防止多条现成同时-1而只做一次变化
            string userId = string.Empty;
            Clients.TryRemove(connectionId, out userId);
            Interlocked.Decrement(ref _connections);
            //Connection.Broadcast(userId + " 连接关闭. 当前连接数: " + _connections);
            return base.OnDisconnected(request, connectionId, stopCalled);
        }

        /// <summary>
        /// 重连时会自动触发该函数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        protected override Task OnReconnected(IRequest request, string connectionId)
        {
            return base.OnReconnected(request, connectionId);
        }

    }

    public class ErrData
    {
        public int Type { get; set; }
        public string Msg { get; set; }
    }
}
