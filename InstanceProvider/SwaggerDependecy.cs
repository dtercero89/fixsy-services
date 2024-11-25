using Microsoft.OpenApi.Models;

namespace FixsyWebApi.InstanceProvider
{
    public static class SwaggerDependecy
    {
        public static void AddSwaggerConfig(this IServiceCollection services, string environment)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Fixsy API",
                    Description = $"Welcome to Fixsy Api's - Environment: {environment}",
                });

                options.DocumentFilter<EnvironmentDocumentFilter>(environment);
            });
        }

        public static void UseMySwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fixsy");
                //c.InjectStylesheet("/swagger/custom.css");
                c.RoutePrefix = String.Empty;
            });
        }
    }
}