namespace Todo.Api.Data.Data.Models.Collection
{
    /// <summary>
    /// A model for updating a <see cref="Todo.Api.Data.Data.Entities.Collection"/>
    /// </summary>
    public class UpdateCollectionRequest
    {
        /// <summary>
        /// Represents the id of the collection.
        /// </summary>
        public string Id { get; set; } = default!;

        /// <summary>
        /// Represents the name of the collection.
        /// </summary>
        public string Name { get; set; } = default!;
    }
}