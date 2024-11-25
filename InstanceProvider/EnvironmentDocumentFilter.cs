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
            // Agregar una extensión que muestre el entorno actual
            swaggerDoc.Info.Extensions.Add("x-environment", new OpenApiString(_environment));
        }
    }
}
