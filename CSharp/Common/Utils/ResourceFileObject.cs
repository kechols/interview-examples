using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Resources;
using log4net;

namespace Sunrise.Radiology.Messenger.Common.Utils
{
    public class ResourceFileObject
    {
        public static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string Path { get; }

        public ConcurrentDictionary<string, string> Entries { get; }

        public ResourceFileObject(string path)
        {
            Path = path ?? string.Empty;
            Entries = new ConcurrentDictionary<string, string>();
        }


        public ResourceFileObject AddEntry(string key, string value)
        {
            if (!Entries.ContainsKey(key))
            {
                Entries[key] = value;
            }
            return this;
        }


        public ResourceFileObject AddNewEntries(Dictionary<string, string> data)
        {
            foreach (var key in data.Keys)
            {
                var value = data[key] ?? string.Empty;
                AddEntry(key, value);
            }

            return this;
        }


        public ResourceFileObject LoadExistingEntries()
        {
            var reader = new ResXResourceReader(Path) { UseResXDataNodes = true };
            try
            {
                foreach (DictionaryEntry entry in reader)
                {
                    var value = ((ResXDataNode) entry.Value)?.GetValue((ITypeResolutionService) null).ToString() ??
                                string.Empty;
                    AddEntry(entry.Key.ToString(), value);
                }
            }
            catch (Exception ex) when (ex is ArgumentException || ex is FileNotFoundException)
            {
                // Create empty File
                Log.Error(ex);
            }
            return this;
        }
    }
}