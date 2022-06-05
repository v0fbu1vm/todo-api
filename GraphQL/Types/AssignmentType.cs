using Todo.Api.Data.Data;
using Todo.Api.Data.Data.Entities;
using Todo.Api.GraphQL.Resolvers;

namespace Todo.Api.GraphQL.Types
{
    /// <summary>
    /// Defining types for <see cref="Assignment"/>. This helps define the schema.
    /// </summary>
    public class AssignmentType : ObjectType<Assignment>
    {
        protected override void Configure(IObjectTypeDescriptor<Assignment> descriptor)
        {
            descriptor.Description("Represents the title of the assignment.");

            descriptor.Field(options => options.Id)
                .Description("Represents a unique identifier.");

            descriptor.Field(options => options.DateCreated)
                .Description("Represents when a record was added to the system.");

            descriptor.Field(options => options.DateModified)
                .Description("Represents when a record was modified.");

            descriptor.Field(options => options.Title)
                .Description("Represents the title of the assignment.");

            descriptor.Field(options => options.Description)
                .Description("Represents the description of the assignment.");

            descriptor.Field(options => options.DateScheduled)
                .Description("Represents when the assignment is scheduled to occur.");

            descriptor.Field(options => options.IsCompleted)
                .Description("Represents whether an assignment is complete or not.");

            descriptor.Field(options => options.IsImportant)
                .Description("Represents whether an assignment is important or not.");

            descriptor.Field(options => options.CollectionId)
                .Description("Represents the id of the collection that holds this assignment.");

            descriptor.Field(options => options.Collection)
                .Description("Represents the collection that holds this assignment.")
                .ResolveWith<AssignmentResolvers>(options => options.GetCollection(default!, default!))
                .UseDbContext<DatabaseContext>()
                .UseProjection(); ;

            descriptor.Field(options => options.UserId)
                .Description("Represents the id of the user that is assigned this assignment.");

            descriptor.Field(options => options.User)
                .Description("Represents the user that is assigned this assignment.")
                .ResolveWith<AssignmentResolvers>(options => options.GetUser(default!, default!))
                .UseDbContext<DatabaseContext>()
                .UseProjection();

            base.Configure(descriptor);
        }
    }
}
