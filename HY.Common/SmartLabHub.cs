using Microsoft.AspNet.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIs
{
    /// <summary>
    /// 集线器类，记得写public，不然在获取时会报错……尴尬……
    /// </summary>
    /// 
   // [Authorize]
    public class SmartLabHub : Hub
    {
        private static ConcurrentDictionary<string, User> HubClients = new ConcurrentDictionary<string, User>();


        //public override Task OnConnected()
        //{
        //    //var userId = this.Context.User.Claims.FirstOrDefault(s => s.Type == "UserId")?.Value;//从缓存中获取用户UserId
        //    //Get the username
        //   // string name = Context.User.Identity.Name;

        //    //Get the UserId
        //    var claimsIdentity = Context.User.Identity as ClaimsIdentity;
        //    if (claimsIdentity != null)
        //    {
        //        // the principal identity is a claims identity.
        //        // now we need to find the NameIdentifier claim
        //        var userIdClaim = claimsIdentity.Claims
        //            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        //        if (userIdClaim != null)
        //        {
        //            var userIdValue = userIdClaim.Value;
        //        }
        //    }
        //    return base.OnConnected();
        //}

        /// <summary>
        /// 连接成功的时候调用
        /// </summary>
        /// <returns></returns>

        public override Task OnConnected()
        {    
            //string name = Context.User.Identity.Name;
            //调用客户端的方法通知所有人包括自己
            Clients.All.LoginSuccessNotice($"用户【{this.Context.ConnectionId}】登录成功", this.Context.ConnectionId);
            //回传给客户端自己的CId
            Clients.Client(this.Context.ConnectionId).ReceiveOwnCid(this.Context.ConnectionId);
            return base.OnConnected();
        }


        /// <summary>
        /// 连接断开的时候调用
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            //除去自己以外的消息
            Clients.AllExcept(this.Context.ConnectionId).receiveMsg($"用户【{this.Context.ConnectionId}】已经离开");
            return base.OnDisconnected(stopCalled);
        }


        /// <summary>
        /// 重新连接的时候调用
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }


        //下面是自定义的服务器端方法
        /// <summary>
        /// 用户登录，自动建立一个长连接
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public List<User> Login(string name, string password)
        {
            if (!HubClients.ContainsKey(name))
            {
                Console.WriteLine($"++ {name} logged in");
                List<User> users = new List<User>(HubClients.Values);
                User newUser = new User { Name = name, Password = password, ConnectionId = Context.ConnectionId};
                var added = HubClients.TryAdd(name, newUser);
                if (!added) return null;
                Clients.CallerState.UserName = name;
                Clients.Others.ParticipantLogin(newUser);
                return users;
            }
            return null;
        }

        /// <summary>
        /// 登出
        /// </summary>
        public void Logout()
        {
            var name = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(name))
            {
                User client = new User();
                HubClients.TryRemove(name, out client);
                Clients.Others.ParticipantLogout(name);
                Console.WriteLine($"-- {name} logged out");
            }
        }

        /// <summary>
        /// 点对点发送消息
        /// </summary>
        /// <param name="receiveId"></param>
        /// <param name="msg"></param>

        public void SendSingleMsg(string receiveId, string msg)
        {
            Clients.Client(receiveId).receiveMsg($"用户【{this.Context.ConnectionId}】发来消息：{msg}");
        }


        /// <summary>
        /// 群发消息
        /// </summary>
        /// <param name="msg"></param>

        [HubMethodName(nameof(SendAllMsg))]
        public void SendAllMsg(string msg)
        {
            //除去自己以外的消息（不需要自己存储ConnectionId）
            Clients.AllExcept(this.Context.ConnectionId).receiveMsg($"用户【{this.Context.ConnectionId}】发来消息：{msg}");
        }


        /// <summary>
        /// 进入指定组
        /// </summary>
        /// <param name="roomName">组的名称</param>
        [HubMethodName(nameof(EnterRoom))]
        public void EnterRoom(string roomName)
        {
            //进入组
            Groups.Add(this.Context.ConnectionId, roomName);
           //告诉自己进入成功
            Clients.Client(this.Context.ConnectionId).receiveMsg($"用户【{this.Context.ConnectionId}】成功进入组：【{roomName}】");
        }


        /// <summary>
        /// 向指定组发送消息
        /// </summary>
        /// <param name="roomName">组名</param>
        /// <param name="msg">内容</param>
        [HubMethodName(nameof(SendRoomNameMsg))]
        public void SendRoomNameMsg(string roomName, string msg)
        {
           //向指定组发送消息，如果这个组包含自己，将自己除外
           Clients.Group(roomName, this.Context.ConnectionId).receiveMsg($"用户【{this.Context.ConnectionId}】发来消息：{msg}");
        }
    }
}
