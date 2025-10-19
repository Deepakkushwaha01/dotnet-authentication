namespace Authentication.API.Settings
{
    public class ConnectionStringSettings(IConfiguration configuration)
    {
        public string AuthenticationIdentityDbConnectionString { get; } = configuration.GetValue<string>("ConnectionStrings:AuthenticationIdentityDbConnectionString")!;
    }
}