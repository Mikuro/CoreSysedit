using System;
using System.IO;
using System.Linq;

namespace tanks
{
    public static class TestData
    {
        public static UnmanagedMemoryStream GetResourceStream(string resName)
        {
            var assembly = typeof(TestData).Assembly;
            var stream = assembly.GetManifestResourceStream(resName);
/*
            var strResources = assembly.GetName().Name + ".g.resources";
            var rStream = assembly.GetManifestResourceStream(strResources);
            var resourceReader = new System.Resources.ResourceReader(rStream);
            var items = resourceReader.OfType<System.Collections.DictionaryEntry>();
            var stream = items.First(x => (x.Key as string) == resName.ToLower()).Value;
*/
            return (UnmanagedMemoryStream)stream;
        }
    }
}
