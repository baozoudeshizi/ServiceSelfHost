using HY.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HY.Common.Helpers
{
    public static class CurrentOperatorHelper
    {
        /// <summary>
        /// 根据当前请求的信息，获得操作用户
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static CurrentOperator GetCurrentOperatorHelper(HttpRequestMessage Request)
        {
            if(Request!=null && !string.IsNullOrEmpty(Request.Headers.Authorization.ToString()))
            {
                string token = Request.Headers.Authorization.ToString();
                CurrentOperator curOperator = JWTTokenHelper.DecodeJTWToken(token);
                return curOperator;
            }
            else
            {
                return null;
            }

        }
    }
}
