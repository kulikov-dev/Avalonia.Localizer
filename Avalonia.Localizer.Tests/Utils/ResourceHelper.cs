using System.Reflection;

namespace Avalonia.Localizer.Tests.Utils
{
    /// <summary>
    /// Helper class to read embedded resources in assembly.
    /// </summary>
    public static class ResourceHelper
    {
        /// <summary>
        /// Read embedded resource as Stream.
        /// </summary>
        /// <param name="assembly"> Assembly </param>
        /// <param name="folder"> Folder </param>
        /// <param name="fileName"> Resource name with extension </param>
        /// <returns> Stream for the resource </returns>
        public static Stream? ReadResource(Assembly assembly, string? folder, string fileName)
        {
            var assemblyName = assembly.GetName().Name;

            var resourcePath = folder != null ? $"{assemblyName}.{folder}.{fileName}" : $"{assemblyName}.{fileName}";

            return assembly.GetManifestResourceStream(resourcePath);
        }

        /// <summary>
        /// Read embedded resource as String.
        /// </summary>
        /// <param name="assembly"> Assembly </param>
        /// <param name="folder"> Folder </param>
        /// <param name="fileName"> Resource name with extension </param>
        /// <returns> Resource as string </returns>
        public static string? ReadResourceAsString(Assembly assembly, string? folder, string fileName)
        {
            using var stream = ReadResource(assembly, folder, fileName);

            if (stream == null)
            {
                return null;
            }

            using var streamReader = new StreamReader(stream);

            return streamReader.ReadToEnd();
        }
    }
}
