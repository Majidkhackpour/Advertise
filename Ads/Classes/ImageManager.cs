using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ErrorHandler;

//using PacketParser.Entities;

namespace Ads.Classes
{
    public static class ImageManager
    {

        //public static string ModifyImage(string SourceFullPath)
        //{
        //    try
        //    {
        //        var fileInfo = new FileInfo(SourceFullPath);
        //        var ret = Path.Combine(fileInfo.Directory.FullName, $"{Guid.NewGuid()}.jpg");
        //        ret = ModifyImage(SourceFullPath, ret);

        //        if (File.Exists(ret))
        //        {
        //            File.Delete(SourceFullPath);
        //            return ret;
        //        }
        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        //WebErrorLog.ErrorLogInstance.StartLog(ex);
        //        return "";
        //    }
        //}

        public static string ModifyImage(string SourceFullPath)
        {
            try
            {
                var destinationPath = Path.Combine(Application.StartupPath, "temp");
                if (!Directory.Exists(destinationPath))
                    Directory.CreateDirectory(destinationPath);
                destinationPath = Path.Combine(destinationPath, $"{Guid.NewGuid()}.jpg");

                using (var bm = new Bitmap(SourceFullPath))
                {
                    var rnd = new Random();
                    var rnd_w = new Random();
                    var rnd_h = new Random();
                    for (var i = 0; i < 10; i++)
                    {
                        bm.SetPixel(rnd_w.Next(bm.Width - 1), rnd_h.Next(bm.Height - 1), Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255)));
                    }
                    bm.Save(destinationPath);
                }

                return destinationPath;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return "";
            }
        }
        //public static async void AllImageModify(string mainPath)
        //{
        //    try
        //    {
        //        var allAdvertise = await Advertise.GetAllAsync(mainPath);
        //        if (allAdvertise?.Count > 0)
        //            foreach (var adv in allAdvertise)
        //                if (adv.Images?.Count > 0)
        //                    foreach (var img in adv.Images)
        //                    {
        //                        var newPath = ImageManager.ModifyImage(img);
        //                        //Path.Combine(Path.GetDirectoryName(img), Guid.NewGuid() + ".jpg");

        //                        if (File.Exists(newPath))
        //                        {
        //                            File.Delete(img);
        //                        }
        //                    }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ImageModifier:" + ex.Message);
        //    }
        //}

    }
}