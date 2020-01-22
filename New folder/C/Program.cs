using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdvertiseApp.Forms;
using ClientHub;
//using Modem;
using NLog;
using PacketParser.Entities;
using PerformanceEntities.Monitor;
using static AdvertiseApp.Classes.Utility;

namespace AdvertiseApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///
        public static Logger Logger1 { get; } = LogManager.GetCurrentClassLogger();

        [STAThread]
        private static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DebugLoger.DebugLogerInstance.init(ENSource.Advertise, false);
           // var frm11 = new FrmTest();
            //frm11.ShowDialog();


            PerformanceEntities.ErrorHandler.AddHandler(false, false, false, Assembly.GetExecutingAssembly().GetName().Version.ToString(), ENSource.Advertise, Application.StartupPath, 123456789, "accountingerrorlog", "accountingerrorlog", 10, 50);
            MonitorLog.Initialize(true, ENSource.Advertise, Assembly.GetExecutingAssembly().GetName().Version.ToString(), 10, 30, "accountingerrorlog", "accountingerrorlog", 1, 500);

            await AccSqlServerPersistence.cache.GetCache(ClsConnection.ConnectionString, Path.Combine(Application.StartupPath,"sqlite.db"), ImagePath(), new List<ClientConfig>() { new ClientConfig(){Source =ENSource.Advertise}}, new CancellationTokenSource(),
                    1,ENSource.Advertise,advertise:true);
           await Task.Run(()=> {ClsConnection.Backup(); });  

            var frm = new frmTestMain();
            try
            {
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
            }

            CloseAllChromeWindows();
        }

        private static string ImagePath()
        {
            try
            {
                var path = Path.Combine(Application.StartupPath, "Images");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return null;

            }
        }
    }
}
