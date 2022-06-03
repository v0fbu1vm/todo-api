using Microsoft.Extensions.DependencyInjection;

namespace Todo.Api.Data.Configurations.Dependencies
{
    public interface IServiceRegistrant
    {
        public void Register(IServiceCollection services);
    }
}
