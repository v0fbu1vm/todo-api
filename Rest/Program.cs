using Todo.Api.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServicesFromAssemblies();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();