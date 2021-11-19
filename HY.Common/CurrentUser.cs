using HY.Models.Model;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HY.Common
{
    public class CurrentUser
    {
        public static Dictionary<string, UserInfoDTO>  userCache = new Dictionary<string, UserInfoDTO>();
        public static CurrentUser Instance
        {
            get { return new CurrentUser(); }
        }

        public void AddCurrent(string token, UserInfoDTO userInfo)
        {

            if (userInfo != null)
            {
                userCache.Add(token, userInfo);
            }

        }


        /// <summary>
        /// Api接口需要传入apiToken
        /// </summary>
        /// <param name="apiToken"></param>
        //public void RemoveCurrent(string apiToken = "")
        //        {
        //                    CacheFactory.Cache.RemoveCache(apiToken);
        //        }

        /// <summary>
        /// Api接口需要传入apiToken
        /// </summary>
        /// <param name="apiToken"></param>
        /// <returns></returns>
        //public async Task<OperatorInfo> Current(string apiToken = "")
        //{
        //    var Authorization = Request.Headers.Authorization;
        //}
    }
}
