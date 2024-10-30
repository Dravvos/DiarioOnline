namespace DiarioOnline.Web.Services
{
    public static class Services
    {
        private static string DiarioOnline_API = "DiarioOnline.API";

        public static string Api(IConfiguration config, string service)
        {
            return config[DiarioOnline_API] + service;
        }

    }
}
