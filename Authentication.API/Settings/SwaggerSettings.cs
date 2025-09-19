namespace Authentication.API.Registers.Settings
{
    public class SwaggerSettings(IConfiguration _configuration)
    {
        public string RoutePrefix => _configuration.GetValue<string>("swagger:routePrefix")!;
        public string Version1_0_JsonEndpointUrl => _configuration.GetValue<string>("swagger:version1_0:jsonEndpointUrl")!;
        public string Version1_0_Name => _configuration.GetValue<string>("swagger:version1_0:name")!;
        public string ApplicationName => _configuration.GetValue<string>("swagger:applicationName")!;
        public string Description => _configuration.GetValue<string>("swagger:description")!;
        public string Version => _configuration.GetValue<string>("swagger:version")!;
        public string RouteTemplate => _configuration.GetValue<string>("swagger:RouteTemplate")!;
    }
}
