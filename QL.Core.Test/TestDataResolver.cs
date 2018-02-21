using System.IO;
using System.Linq;
using System.Reflection;

namespace QL.Core.Test
{
    internal static class TestDataResolver
    {
        public static string LoadTestFile(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceId = assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(resourceName));
            if (string.IsNullOrEmpty(resourceId))
            {
                return string.Empty;
            }

            using (Stream stream = assembly.GetManifestResourceStream(resourceId))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
