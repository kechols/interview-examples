using Kevins.Examples.Common.Utils;

namespace Kevins.Examples.Common.Io.Pdf
{
    public class PdfResources : AbstractResourceFileHandler
    {
        public override string ResourceFilePath => ProjectHelper.AssemblyDirectory +
                                                   @"\Io\Pdf\PdfResources.resx";
    }
}
