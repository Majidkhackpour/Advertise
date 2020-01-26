using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Forms.Mains;
using AutoMapper;
using BussinesLayer;

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


            Application.Run(new frmMain());
        }
    }
}
