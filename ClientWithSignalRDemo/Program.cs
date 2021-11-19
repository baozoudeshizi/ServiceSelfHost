using Microsoft.AspNet.SignalR.Client;
using System;
using static System.Console;

namespace ClientWithSignalRDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region PersistentConection连接方式
            var connection = new Connection("http://127.0.0.1:9000/smartPreConnection");
            connection.Headers.Add("myToken", "eyJhbGciOiJub25lIiwidHlwIjoiSldUIn0.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJqdGkiOiIxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9leHBpcmF0aW9uIjoiMjAyMS83LzIzIDk6NDY6MzMiLCJuYmYiOjE2MjY5MTgzOTMsImV4cCI6MTYyNzAwNDc5MywiaXNzIjoiaHV5dW4iLCJhdWQiOiJ6eXAifQ.");
            connection.Received += WriteLine;
            connection.Start().Wait();
            string line;
            while ((line = ReadLine()) != null)
            {
                connection.Send(line).Wait();
            }
            #endregion 


            #region Hub请求方式
            //string url = "http://localhost:9000/SmartLabHub";
            //HubConnection connection = new HubConnection(url);
            //connection.Headers.Add("myToken", "1111");
            //IHubProxy hubProxy = connection.CreateHubProxy("SmartLabHub");
            //hubProxy.On<string,string>("ParticipantLogin", RecvMsg);
            //connection.Start().ContinueWith(task =>
            //{
            //    if (!task.IsFaulted)
            //    {
            //        WriteLine(string.Format("与Signal服务器连接成功,服务器地址：{0}\r\n", url));
            //    }
            //    else
            //    {
            //        WriteLine("与服务器连接失败，请确认服务器是否开启。\r\n");
            //    }
            //}).Wait();
            //Console.ReadLine();
            #endregion

        }


        private static void RecvMsg(string name, string message)
        {
            WriteLine(string.Format("接收时间：{0},发送人：{1},消息内容：{2}，\r\n", DateTime.Now, name, message));
        }
    }
}
