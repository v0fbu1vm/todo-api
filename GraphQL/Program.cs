using Todo.Api.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServicesFromAssemblies();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapGraphQL();

app.MapGraphQLVoyager("graphql-voyager");

app.Run();