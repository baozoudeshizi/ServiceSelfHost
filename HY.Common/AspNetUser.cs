using HY.Common.Helpers;
using System.Net.Http;

namespace HY.Common
{
    /// <summary>
    /// 弃用
    /// </summary>
    public class AspNetUser:IUser
    {
        private readonly HttpRequestMessage _request;
        //private readonly ILogger<AspNetUser> _logger;

        public AspNetUser(HttpRequestMessage request/*, ILogger<AspNetUser> logger*/)
        {
            _request = request;
            //_logger = logger;
        }

        public string ID => JWTTokenHelper.DecodeJTWToken(_request.Headers.Authorization.ToString()).ID;
        public string Name => GetName();

        private string GetName()
        {
            return "";
        }


    }
}
