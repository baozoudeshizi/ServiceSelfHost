using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApis
{
    public class SwaggerOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null) operation.parameters = new List<Parameter>();

            if (apiDescription.RelativePath.Contains("/UploadFile"))
            {
                operation.parameters.RemoveAt(0);

                operation.parameters.Add(new Parameter
                {
                    name = "folder",
                    @in = "formData",
                    description = "文件夹",
                    required = false,
                    type = "string"
                });

                operation.parameters.Add(new Parameter
                {
                    name = "file",
                    @in = "formData",
                    description = "文件",
                    required = true,
                    type = "file"
                });

                operation.consumes.Add("multipart/form-data");
            }

            Collection<AllowAnonymousAttribute> attributes = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>();
            if (attributes.Count == 0)
            {
                //operation.parameters.Insert(0, new Parameter { name = "token", @in = "header", description = "Token", required = true, type = "string" });
            }
        }
    }
}