namespace Todo.Api.GraphQL.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class TempQueries
    {
        public string HelloWorld() => "Hello, World!";
    }
}
