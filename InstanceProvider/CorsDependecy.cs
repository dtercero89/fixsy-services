namespace FixsyWebApi.InstanceProvider
{
    public static class CorsSettings{

        public static void AddCorsSettings(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "ClientPermission", builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .WithOrigins("http://localhost:3100", "https://fixsy.vercel.app")
                           .AllowCredentials();
                });
            });
        }
    }
}