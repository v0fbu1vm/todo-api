using Todo.Api.Data.Configurations.Dependencies;

namespace Todo.Api.Rest.Configurations.Dependencies
{
    /// <summary>
    /// Used for registring the remaining minor services.
    /// </summary>
    public class ServiceRegistrant : IServiceRegistrant
    {
        public void Register(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
        }
    }
}
