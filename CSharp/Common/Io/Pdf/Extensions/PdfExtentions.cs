using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using Kevins.Examples.Common.Extensions;
using Kevins.Examples.Common.Io.Web;
using log4net;
using NReco.PdfGenerator;

namespace Kevins.Examples.Common.Io.Pdf.Extensions
{
    public static class PdfExtentions
    {
        private static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly PdfResources pdfResources = new PdfResources();


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static PdfFileObject HtmlContentToPdf(this string htmlContent, string fileNamePrefix, int id)
        {
            var pdfResult = new PdfFileObject();
            pdfResult.FilePath = GetCalculatedPdfFilename(fileNamePrefix, id);

            (new FileInfo(pdfResult.FilePath)).Directory.Create();
            // Creates output directory structure if it has not been.

            if (File.Exists(pdfResult.FilePath))
            {
                htmlContent = string.Format(pdfResources.GetValue("fileAlreadyExist"), id);
                Log.Error(htmlContent);
            }

            File.WriteAllBytes(pdfResult.FilePath, new HtmlToPdfConverter().GeneratePdf(htmlContent));

            return pdfResult;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static PdfFileObject UrlToPdf(this string url, string fileNamePrefix, int id,
            out bool removedFieldsToHideWhenPrintng)
        {
            removedFieldsToHideWhenPrintng = false;
            var pdfResult = new PdfFileObject();

            if (IsValidUrl(url))
            {
                var srisWebUrlStreamReader = new WebUrlStreamReader();
                srisWebUrlStreamReader.DoWork(url);
                if (srisWebUrlStreamReader.HasError)
                {
                    return pdfResult;
                }

                if (IsValidSrisUrl(url))
                {
                    var removedFieldsToHideWhenPrintngResult =
                        srisWebUrlStreamReader.HtmlContent.RemoveSrisFieldsToHideWhenPrinting(
                            removedFieldsToHideWhenPrintng);
                    removedFieldsToHideWhenPrintng = removedFieldsToHideWhenPrintngResult.RemovedFields;
                    pdfResult = removedFieldsToHideWhenPrintngResult.Html.HtmlContentToPdf(fileNamePrefix, id);
                }
                else
                {
                    pdfResult = srisWebUrlStreamReader.HtmlContent.HtmlContentToPdf(fileNamePrefix, id);
                }
            }
            else
            {
                var request = (FileWebRequest)WebRequest.Create(url);
                var response = (FileWebResponse)request.GetResponse();
                var readStream = new StreamReader(response.GetResponseStream());

                pdfResult = readStream.ReadToEnd().HtmlContentToPdf(fileNamePrefix, id);

                response.Close();
                readStream?.Close();
            }

            return pdfResult;
        }


        private static string GetCalculatedPdfFilename(string fileNamePrefix, int id)
        {
            var generatedTime = @"" + DateTime.Now.ToString("yyyy-MM-d@HH-mm-ss");
            var generatedPdfFilename = ("MessageOutputLocation".RequiredSetting() + @"/" + fileNamePrefix + "_" + id +
                                        "_" + generatedTime +
                                        ".pdf").
                ReplaceAll(@"/", Path.DirectorySeparatorChar.ToString());
            return generatedPdfFilename;
        }


        private static bool IsValidSrisUrl(string url)
        {
            return (url.IndexOf(".do", StringComparison.OrdinalIgnoreCase) >= 0);
        }


        private static bool IsValidUrl(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}