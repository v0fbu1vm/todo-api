using System.Reflection;

namespace Todo.Api.Data.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets the assemblies for projects found in the solution.
        /// </summary>
        /// <param name="appDomain"></param>
        /// <returns>
        /// A list of <see cref="Assembly"/>'s.
        /// </returns>
        public static IEnumerable<Assembly> GetAssembliesForProjects(this AppDomain appDomain)
        {
            return appDomain.GetAssemblies()
                .Where(options => options.FullName != null && options.FullName.Contains("Todo.Api"))
                .ToList();
        }
    }
}
