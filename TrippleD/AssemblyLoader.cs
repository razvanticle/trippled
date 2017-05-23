using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace TrippleD
{
    internal static class AssemblyLoader
    {
        public static IEnumerable<Assembly> GetReferencingAssemblies(string assemblyName)
        {
            return DependencyContext.Default.CompileLibraries.Select(x => x.Name)
                .Where(x => x.Contains(assemblyName.ToLower()))
                .Select(x => Assembly.Load(new AssemblyName(x)));
        }
    }
}