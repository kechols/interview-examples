using System;
using System.IO;
using System.Reflection;

namespace Kevins.Examples.Common.Utils
{
    public static class ProjectHelper
    {
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }


        public static string ProjectDirectory
        {
            get
            {
                var assemblyDirectory = AssemblyDirectory;
                var lastWordToInclude = @"Messenger";
                var trimIndex = assemblyDirectory.IndexOf(lastWordToInclude, StringComparison.Ordinal);
                trimIndex = trimIndex + lastWordToInclude.Length;
                return AssemblyDirectory.Substring(0, trimIndex);
            }
        }
    }
}
