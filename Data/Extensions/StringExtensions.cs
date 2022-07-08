namespace Todo.Api.Data.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Removes special-characters from any <see langword="string"/>.
        /// <example>
        /// Input: "test@test.com"
        /// Output: "testtestcom"
        /// </example>
        /// </summary>
        /// <param name="value">Represents a string, which needs it's special-characters removed.</param>
        /// <returns>
        /// Returns a new <see langword="string"/> without special-characters.
        /// </returns>
        public static string RemoveSpecialCharacters(this string value)
        {
            return new string((from c in value
                               where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                               select c
                   ).ToArray());
        }
    }
}