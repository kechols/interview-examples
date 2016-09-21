using System;
using System.IO;
using System.Net;
using System.Text;
using Kevins.Examples.Common.Utils.Patterns;
using log4net;

namespace Kevins.Examples.Common.Io.Web
{
    public class WebUrlStreamReader : AbstractRetryable
    {

        private static readonly WebUrlStreamReaderResources srisWebUrlStreamReaderResources = new WebUrlStreamReaderResources();

        private string url;

        public string Error { get; private set; }

        public bool HasError => !string.IsNullOrWhiteSpace(Error);

        public string HtmlContent { get; private set; }

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string DoWork(string url)
        {
            this.url = url;
            HtmlContent = string.Empty;

            try
            {
                // Try generating the notification max attemts once every second because it takes time for the message to be generated
                var retryer = new Retryer(this);
                retryer.Perform(MaxAttempts, 1000);
                Exception realException = null;
            }
            catch (RetryFailedException ex)
            {
                var message = GetCauseOfFailure() != null
                    ? (GetCauseOfFailure().Message + " : " + ex.Message)
                    : ex.Message;
                var realException = GetCauseOfFailure() ?? ex;
                Log.Error(message, realException);
            }


            return HtmlContent;
        }


        protected override bool DoAttempt()
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            StreamReader readStream = null;

            try
            {
                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var receiveStream = response.GetResponseStream();

                    readStream = response.CharacterSet == null
                        ? new StreamReader(receiveStream)
                        : new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                    HtmlContent = readStream.ReadToEnd();

                    response.Close();
                    return true;
                }
                else
                {
                    throw new Exception(LogInvalidUrlError(url, response.StatusCode));
                }
            }
            catch (WebException webException)
            {
                throw new WebException(LogInvalidUrlError(url, webException.Message), webException);
            }
        }


        private string LogInvalidUrlError(string url, Object errorDetails)
        {
            Error = string.Format(srisWebUrlStreamReaderResources.GetValue("invalidUrl"), url, errorDetails);
            Log.Error(Error);
            return Error;
        }
    }
}
