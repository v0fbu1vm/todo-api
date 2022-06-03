using Microsoft.Extensions.DependencyInjection;
using Todo.Api.Data.Configurations.Dependencies;

namespace Todo.Api.Data.Extensions
{
    public static class ServiceRegistrantExtensions
    {
        /// <summary>
        /// An extension for registring services in assembly.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServicesFromAssemblies(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssembliesForProjects();

            foreach (var assembly in assemblies)
            {
                var registrants = assembly.ExportedTypes.Where(x => typeof(IServiceRegistrant).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IServiceRegistrant>().ToList();

                registrants.ForEach(x => x.Register(services));
            }
        }
    }
}
