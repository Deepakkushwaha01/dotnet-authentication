using Authentication.API.Registers;


namespace Authentication.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                var app = BuildApplication(builder);
                RunApplication(app);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private static WebApplication BuildApplication(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services
            .RegisterSwagger(builder.Configuration)
            .RegisterMediatR()
            .RegisterVersioning()
            .RegisterDatabase(builder.Configuration);

            return builder.Build();
        }

        private static void RunApplication(WebApplication app)
        {
            app.UseSwaggerDocumentation(app.Configuration);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
