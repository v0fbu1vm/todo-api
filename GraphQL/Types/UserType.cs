using Todo.Api.Data.Data;
using Todo.Api.Data.Data.Entities;
using Todo.Api.GraphQL.Resolvers;

namespace Todo.Api.GraphQL.Types
{
    /// <summary>
    /// Defining types for <see cref="User"/>. This helps define the schema.
    /// </summary>
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Description("Represents a User.");

            descriptor.Field(options => options.Id)
                .Description("Represents a unique identifier.");

            descriptor.Field(options => options.DateCreated)
                .Description("Represents when a record was added to the system.");

            descriptor.Field(options => options.DateModified)
                .Description("Represents when a record was modified.");

            descriptor.Field(options => options.Email)
                .Description("Represents the email-address of a user.");

            descriptor.Field(options => options.EmailConfirmed)
                .Description("Represents whether or not the email-address is confirmed.");

            descriptor.Field(options => options.PhoneNumber)
                .Description("Represents the phone-number of a user.");

            descriptor.Field(options => options.Name)
                .Description("Represents the name of a user.");

            descriptor.Field(options => options.Assignments)
                .Description("Represents a list of assignments, assigned to this user.")
                .ResolveWith<UserResolvers>(options => options.GetAssignments(default!, default!))
                .UseDbContext<DatabaseContext>()
                .UseProjection();

            descriptor.Field(options => options.Collections)
                .Description("Represents a list of collections belonging to this user.")
                .ResolveWith<UserResolvers>(options => options.GetCollections(default!, default!))
                .UseDbContext<DatabaseContext>()
                .UseProjection();

            // Properties that aren't needed in the type.
            descriptor.Ignore(options => options.LockoutEnd);
            descriptor.Ignore(options => options.TwoFactorEnabled);
            descriptor.Ignore(options => options.ConcurrencyStamp);
            descriptor.Ignore(options => options.SecurityStamp);
            descriptor.Ignore(options => options.PhoneNumberConfirmed);
            descriptor.Ignore(options => options.PasswordHash);
            descriptor.Ignore(options => options.LockoutEnd);
            descriptor.Ignore(options => options.NormalizedEmail);
            descriptor.Ignore(options => options.NormalizedUserName);
            descriptor.Ignore(options => options.LockoutEnabled);
            descriptor.Ignore(options => options.AccessFailedCount);

            base.Configure(descriptor);
        }
    }
}
