using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebAPIs.App_Start
{
    /// <summary>
    /// Swagger中加入token
    /// </summary>
    public class HttpAuthHeaderFilter : IOperationFilter
    {
        /// <summary>
        /// 是否包含头部
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)

        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();
            var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline(); //判断是否添加权限过滤器
                                                                                      // var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Instance).Any(filter => filter is IAuthorizationFilter); //判断是否允许匿名方法 
            var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
            var actionFilter = apiDescription.ActionDescriptor.GetCustomAttributes<ApiAuthorizeAttribute>().Any();
            var controllerFilter = apiDescription.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<ApiAuthorizeAttribute>(true).Any();
            //如果包含名字叫TokenAuthorizeAttribute过滤器则给该方法的swagger调用出添加两个头部信息输入框(看个人需要)
            if (actionFilter || controllerFilter)
            {
                operation.parameters.Add(new Parameter { name = "Authorization", @in = "header", description = "请填入Token", required = true, type = "string" });
                //operation.parameters.Add(new Parameter { name = "userid", @in = "header", description = "安全", required = true, type = "string" });
            }

        }
    }
}
