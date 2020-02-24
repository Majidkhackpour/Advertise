using System;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Model;
using System.Windows.Forms;
using Ads.Forms.Mains;
using AutoMapper;
using BussinesLayer;
using DataLayer;
using ErrorHandler;

namespace Ads
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var config = new MapperConfiguration(c => { c.AddProfile(new SqlProfile()); });
            Mappings.Default = new Mapper(config);

            //StartSqlService();

            UpdateMigration();
            Application.Run(new frmMain());
        }

        private static void UpdateMigration()
        {
            try
            {
                var migratorConfig = new DataLayer.Migrations.Configuration();
                var dbMigrator = new DbMigrator(migratorConfig);
                dbMigrator.Update();
            }
            catch 
            {
            }
        }

        private static void StartSqlService()
        {
            try
            {
                var process = new System.Diagnostics.Process {StartInfo = {FileName = "net start \"Sql Server (.)\""}};
                process.Start();
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
    }
}
