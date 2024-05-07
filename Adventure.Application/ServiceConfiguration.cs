namespace Adventure.Application
{
    public static class ServiceConfiguration
    {
        public static void Register(IServiceCollection Service)
        {
            Service.AddScoped<IPersonService, PersonService>();
        }
    }
}
