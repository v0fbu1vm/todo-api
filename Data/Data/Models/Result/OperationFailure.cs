namespace Todo.Api.Data.Data.Models.Result
{
    /// <summary>
    /// Represents a model which can be used to provide detailes about an operation,
    /// incase of failure.
    /// </summary>
    public class OperationFailure
    {
        /// <summary>
        /// Represents an error code.
        /// </summary>
        public short ErrorCode { get; set; } = default!;

        /// <summary>
        /// Represents an error message.
        /// </summary>
        public string ErrorMessage { get; set; } = default!;
    }
}