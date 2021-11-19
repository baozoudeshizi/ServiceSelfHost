using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPIs
{
    /// <summary>
    /// 权限过滤器
    /// </summary>
    public class ApiAuthorizeAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var attr = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
            bool isAnonymous = attr.Any(a => a is AllowAnonymousAttribute);
            if (!isAnonymous)
            {
                var rq = actionContext.Request.Properties;
                var authorization = actionContext.Request.Headers.Authorization;
                if (authorization == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
                else
                {
                    //暂不作处理
                    //string ResultMessage;//需要解析的消息
                    //string Payload;//获取负载
                                   //var result = JwtToken.ValidateJWT(authorization.Scheme, out Payload, out ResultMessage); //TokenManager.ValidateToken(authorization.Scheme);
                                   //if (!result)
                                   //{
                                   //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                                   //}
                                   //var result = JWTTokenHelper.DecodeJTWToken("Bearer "+authorization.Scheme);
                    //var result = JWTTokenHelper.DecodeJTWToken(authorization.Scheme);
                }

            }
        }
    }
}
