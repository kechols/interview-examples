using System.IO;
using System.Reflection;

namespace Kevins.Examples.Common.Tests.Unit.Helpers
{
    public static class ResourceFileHelper
    {
        public static string GetEmbeddeResoueLocation(this string filename, string theNameSpace)
        {
            var asm = Assembly.GetExecutingAssembly();
            var seeWhatThisIs = asm.GetManifestResourceNames();
            var resource = string.Format(theNameSpace + ".{0}", filename);
            using (var stream = asm.GetManifestResourceStream(resource))
            {
                if (stream != null)
                {
                    var reader = new StreamReader(stream);
                    return reader.ReadToEnd();
                }
            }
            return string.Empty;
        }
    }
}
