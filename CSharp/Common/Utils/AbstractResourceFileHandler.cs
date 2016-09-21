using Kevins.Examples.Common.Interfaces;

namespace Kevins.Examples.Common.Utils
{
    public abstract class AbstractResourceFileHandler : IResourceFile
    {        
        private ResourceFileObject resourceFile;

        public static readonly string Error = "There is no resource specified for '{0}' in '{1}";

        public abstract string ResourceFilePath { get; }

        public ResourceFileObject ResourceFile
        {
            get
            {
                resourceFile = resourceFile ?? new ResourceFileObject(ResourceFilePath).LoadExistingEntries();
                return resourceFile;
            }
        }


        public string GetValue(string key)
        {
            return ResourceFile.Entries.ContainsKey(key) ? 
                ResourceFile.Entries[key] : 
                string.Format(Error, key, ResourceFilePath);
        }
    }
}
