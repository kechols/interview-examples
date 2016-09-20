using System.Collections.Generic;

namespace Sunrise.Radiology.Messenger.Common.Utils.Strings
{
    public interface IStringLoader
    {
        Dictionary<string, string> Load();
    }
}
