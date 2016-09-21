namespace Kevins.Examples.Common.Io.Pdf
{
    public class PdfFileObject
    {
        public string FilePath { get; set; }


        public bool GeneratedSuccessfully => !string.IsNullOrEmpty(FilePath);


        public bool PrintedSuccessfully { get; set; }


        public PdfFileObject()
        {
            FilePath = string.Empty;
        }


        public PdfFileObject(string path, bool generatedSuccessfully)
        {
            FilePath = path ?? string.Empty;
        }
    }
}
