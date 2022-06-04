namespace Todo.Api.Data.Data.Models.Collection
{
    /// <summary>
    /// A model for creating a new <see cref="Todo.Api.Data.Data.Entities.Collection"/>
    /// </summary>
    public class CreateCollectionRequest
    {
        /// <summary>
        /// Represents the name of the collection.
        /// </summary>
        public string Name { get; set; } = default!;
    }
}
