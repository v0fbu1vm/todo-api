using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.Api.Data.Configurations.Settings;
using Todo.Api.Data.Data;
using Todo.Api.Data.Extensions;
using Todo.Api.Data.Helpers;
using Todo.Api.Data.Services;

namespace Todo.Api.Data.Configurations.Dependencies
{
    /// <summary>
    /// Used for registring the remaining minor services.
    /// </summary>
    public class ServiceRegistrant : IServiceRegistrant
    {
        public void Register(IServiceCollection services)
        {
            services.AddDbContextFactory<DatabaseContext>(options => options.UseNpgsql(AppSettings.ConnectionString));

            services.AddSingleton<TokenProvider>();

            services.AddTransient<AuthService>();
            services.AddTransient<CollectionService>();
            services.AddTransient<AssignmentService>();

            services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssembliesForProjects()));
        }
    }
}