namespace Authentication.API.Registers
{
    public static partial class Register
    {
        private const string CorsPolicyName = "AllowAll";

        public static IServiceCollection RegisterCors(this IServiceCollection services)
        {
            services.AddCors(f => f.AddPolicy(CorsPolicyName, builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .SetIsOriginAllowed(_ => true);
            }));

            return services;
        }

        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder app)
        {
            app.UseCors(CorsPolicyName);

            return app;
        }
    }
}
