using System;
using System.Collections.Generic;

namespace Sunrise.Radiology.Messenger.Common.Utils.Strings
{
    public abstract class AbstractStringsProvider<T>
    where T : AbstractStringsProvider<T>, new()
    {
        private Dictionary<string, string> strings;
        private static T instance;

        /// <exception cref="ArgumentException">The key must be not null and not empty</exception>
        /// <exception cref="InvalidOperationException">Messages not loaded.</exception>
        public string this[string key]
        {
            get
            {
                if (strings == null)
                {
                    throw new InvalidOperationException(GetIdentifier() + "s not loaded");
                }

                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentException("The key must be not null and not empty");
                }

                if (strings.ContainsKey(key))
                {
                    return strings[key];
                }

                return String.Empty;
            }
        }

        public IEnumerable<String> Keys
        {
            get
            {
                return strings.Keys;
            }
        }

        private void Load(IStringLoader loader)
        {
            strings = loader.Load();
        }

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                    instance.Load(instance.GetStringLoader());
                }
                return instance;
            }
        }


        // only for unit testing porpouses
        // breaks singleton pattern
        public static T GetInstance(IStringLoader stringLoader)
        {
            var nonSingletonProvider = new T();
            nonSingletonProvider.Load(stringLoader);

            return nonSingletonProvider;
        }


        public abstract IStringLoader GetStringLoader();

        public abstract string GetIdentifier();

    }
}
