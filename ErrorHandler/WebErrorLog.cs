using System;
using System.IO;
using System.Threading;

namespace ErrorHandler
{
    public class WebErrorLog
    {
        public WebErrorLog()
        {

        }
        public void StartErrorLog(string message, bool issucces)
        {
            var frm = new frmNotification(message, issucces);
            frm.Show();
        }

        public void StartErrorLog(Exception ex, string _description = "",
            [System.Runtime.CompilerServices.CallerMemberName]
            string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath]
            string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber]
            int sourceLineNumber = 0)
        {
            var fileInfo = new FileInfo(sourceFilePath);
            var className = fileInfo.Name;
            var funcName = memberName;
            var time = DateTime.Now.ToLongTimeString();
            var exceptionType = ex.GetType().ToString();
            var exceptionMessage = ex.Message;
            var stackTrace = ex.StackTrace;
            var description = _description;


            var msg = $"Version:{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}" + "\r\n" +
                      $"ClassName:{className}" + "\r\n" +
                      $"FunctionName:{funcName}" + "\r\n" +
                      $"Time:{time}" + "\r\n" +
                      $"Type:{exceptionType}" + "\r\n" +
                      $"Message:{exceptionMessage}" + "\r\n" +
                      $"StackTrace:{stackTrace}" + "\r\n" +
                      $"Description:{description}";


            var th = new Thread(() => SendErrorToTelegram.Send.StartSending(msg));
            th.Start();
            var frm = new frmNotification(ex);
            frm.Show();
        }
        private class NestedCallInfo
        {
            internal static WebErrorLog _instence = new WebErrorLog();

            public NestedCallInfo()
            {
            }
        }

        public static WebErrorLog ErrorInstence => NestedCallInfo._instence;
    }
}
