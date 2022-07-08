using Todo.Api.Data.Data;
using Todo.Api.Data.Data.Entities;
using Todo.Api.GraphQL.Resolvers;

namespace Todo.Api.GraphQL.Types
{
    /// <summary>
    /// Defining types for <see cref="Collection"/>. This helps define the schema.
    /// </summary>
    public class CollectionType : ObjectType<Collection>
    {
        protected override void Configure(IObjectTypeDescriptor<Collection> descriptor)
        {
            descriptor.Description("Represents a collection. A placeholder for assignments/todos.");

            descriptor.Field(options => options.Id)
                .Description("Represents a unique identifier.");

            descriptor.Field(options => options.DateCreated)
                .Description("Represents when a record was added to the system.");

            descriptor.Field(options => options.DateModified)
                .Description("Represents when a record was modified.");

            descriptor.Field(options => options.Name)
                .Description("Represents the name of the collection.");

            descriptor.Field(options => options.UserId)
                .Description("Represents the id of the user that owns this collection.");

            descriptor.Field(options => options.User)
                .Description("Represents the user that owns this collection.")
                .ResolveWith<CollectionResolvers>(options => options.GetUser(default!, default!))
                .UseDbContext<DatabaseContext>()
                .UseProjection();

            descriptor.Field(options => options.Assignments)
                .Description("Represents the assignments belonging to this collection.")
                .ResolveWith<CollectionResolvers>(options => options.GetAssignments(default!, default!))
                .UseDbContext<DatabaseContext>()
                .UseProjection();

            base.Configure(descriptor);
        }
    }
}