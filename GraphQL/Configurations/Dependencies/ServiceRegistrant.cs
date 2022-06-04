using Todo.Api.Data.Configurations.Dependencies;
using Todo.Api.GraphQL.Mutations;
using Todo.Api.GraphQL.Queries;

namespace Todo.Api.GraphQL.Configurations.Dependencies
{
    /// <summary>
    /// Used for registring the remaining minor services.
    /// </summary>
    public class ServiceRegistrant : IServiceRegistrant
    {
        public void Register(IServiceCollection services)
        {
            services.AddGraphQLServer()
               .AddAuthorization()
               .AddDefaultTransactionScopeHandler()
               .AddQueryType()
               .AddMutationType()
               .AddTypeExtension<TempQueries>()
               .AddTypeExtension<AuthMutations>()
               .AddProjections();
        }
    }
}
