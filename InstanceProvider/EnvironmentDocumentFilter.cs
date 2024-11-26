using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FixsyWebApi.InstanceProvider
{
    public class EnvironmentDocumentFilter : IDocumentFilter
    {
        private readonly string _environment;

        public EnvironmentDocumentFilter(string environment)
        {
            _environment = environment;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (!swaggerDoc.Info.Extensions.ContainsKey("x-environment"))
            {
                swaggerDoc.Info.Extensions.Add("x-environment", new OpenApiString(_environment));
            }
        }
    }
}
