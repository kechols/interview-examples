using Kevins.Examples.Common.Utils;
using Sunrise.Radiology.Messenger.Common.Utils;

namespace Kevins.Examples.Common.Io.Pdf
{
    public class PdfResources : AbstractResourceFileHandler
    {
        public override string ResourceFilePath => ProjectHelper.AssemblyDirectory +
                                                   @"\Io\Pdf\PdfResources.resx";
    }
}
