using System;
using System.IO;
using Kevins.Examples.Common.Extensions;

namespace Kevins.Examples.Common.Logs
{
    public class KevinsRollingFileAppender : log4net.Appender.RollingFileAppender
    {
        protected override void AdjustFileBeforeAppend()
        {
            // delete old files
            var logDirectory = Path.GetDirectoryName(File);
            foreach (var file in Directory.GetFiles(logDirectory, "*.log"))
            {
                if (DateTime.Now.Subtract(System.IO.File.GetLastWriteTime(file)).Days >= "NumberDaysToKeepLogs".Setting().AsInt())
                {
                    DeleteFile(file);
                }
            }

            base.AdjustFileBeforeAppend();
        }
    }
}
