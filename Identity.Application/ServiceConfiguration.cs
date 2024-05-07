namespace Identity.Application
{
    public static class ServiceConfiguration
    {
        public static void Register(IServiceCollection Service)
        {
            Service.AddScoped<IAuthService, AuthService>();
        }
    }
}
