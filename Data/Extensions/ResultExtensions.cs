using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Todo.Api.Data.Extensions
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Converts errors from <see cref="IdentityResult"/> to a <see langword="string"/>.
        /// </summary>
        /// <param name="result">Represents the result of an identity operation.</param>
        /// <returns>
        /// All errors as a <see langword="string"/>.
        /// </returns>
        public static string ErrorMessage(this IdentityResult result)
        {
            var errorMessage = new StringBuilder();
            foreach (var item in result.Errors)
            {
                errorMessage.AppendLine(item.Description);
            }

            return errorMessage.ToString();
        }

        /// <summary>
        /// Converts errors from <see cref="ValidationResult"/> to a <see langword="string"/>.
        /// </summary>
        /// <param name="result">Represents the result of a validation operation.</param>
        /// <returns>
        /// All errors as a <see langword="string"/>.
        /// </returns>
        public static string ErrorMessage(this ValidationResult result)
        {
            var errorMessage = new StringBuilder();
            foreach (var item in result.Errors)
            {
                errorMessage.AppendLine(item.ErrorMessage);
            }

            return errorMessage.ToString();
        }
    }
}