using Microsoft.AspNetCore.Http;

namespace Todo.Api.Data.Data.Models.Response
{
    /// <summary>
    /// A response model used for providing a detailed response.
    /// </summary>
    /// <typeparam name="T">Represents the data returned from the operation.</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Whether or not an operation was successful.
        /// </summary>
        public bool Succeeded { get; private set; }
        /// <summary>
        /// Represents the data returned from the operation
        /// </summary>
        public T? Data { get; private set; }
        /// <summary>
        /// Represents a message. To explain how the operation went.
        /// </summary>
        public string Message { get; private set; } = default!;
        /// <summary>
        /// Represents the code for the error.
        /// </summary>
        public short StatusCode { get; private set; }

        public static Response<T> Created(T result, string message = "Record successfully created.")
        {
            return new Response<T>()
            {
                Succeeded = true,
                Data = result,
                Message = message,
                StatusCode = StatusCodes.Status201Created
            };
        }

        public static Response<T> Ok(T result, string message = "The operation went okay.")
        {
            return new Response<T>()
            {
                Succeeded = true,
                Data = result,
                Message = message,
                StatusCode = StatusCodes.Status200OK
            };
        }

        public static Response<T> NoContent(T result, string message = "The record successfully deleted.")
        {
            return new Response<T>()
            {
                Succeeded = true,
                Data = result,
                Message = message,
                StatusCode = StatusCodes.Status204NoContent
            };
        }

        public static Response<T> NotFound(string message = "The record could not be found.")
        {
            return new Response<T>()
            {
                Succeeded = false,
                Message = message,
                StatusCode = StatusCodes.Status404NotFound
            };
        }

        public static Response<T> BadRequest(string message)
        {
            return new Response<T>()
            {
                Succeeded = false,
                Message = message,
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        public static Response<T> Unauthorized(string message = "Unauthorized.")
        {
            return new Response<T>()
            {
                Succeeded = false,
                Message = message,
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }
    }
}
