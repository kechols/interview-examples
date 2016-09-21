using Kevins.Examples.Common.Utils;
using Sunrise.Radiology.Messenger.Common.Utils;

namespace Kevins.Examples.Common.Io.Web
{
    public class WebUrlStreamReaderResources : AbstractResourceFileHandler
    {
        public override string ResourceFilePath => ProjectHelper.AssemblyDirectory +
                                                   @"\Io\Web\WebUrlStreamReaderResources.resx";
    }
}
