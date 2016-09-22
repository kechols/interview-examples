using Kevins.Examples.Common.Utils;

namespace Kevins.Examples.Common.Io.Web
{
    public class WebUrlStreamReaderResources : AbstractResourceFileHandler
    {
        public override string ResourceFilePath => ProjectHelper.AssemblyDirectory +
                                                   @"\Io\Web\WebUrlStreamReaderResources.resx";
    }
}
