using System.Collections.Generic;

namespace Kevins.Examples.Common.Utils.Strings
{
    public interface IStringLoader
    {
        Dictionary<string, string> Load();
    }
}
