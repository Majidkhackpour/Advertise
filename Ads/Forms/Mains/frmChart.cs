using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BussinesLayer;
using DataLayer;
using DataLayer.Enums;
using ErrorHandler;

namespace Ads.Forms.Mains
{
    public partial class frmChart : Form
    {
        public frmChart()
        {
            InitializeComponent();
        }
        private async Task FillChart()
        {
            try
            {
                const int dayCount = 7;
                //لیستی از تاریخ های شمسی هفته اخیر
                var lstDate = new List<string>();
                //تعداد کل آگهی ها ارسال شده
                var lstAll = await AdvertiseLogBussines.GetAdvCountInSpecialMounthAsync(dayCount, AdvertiseType.All);
                //تعداد آگهی های ارسال شده در دیوار در هفته اخیر
                var lstAllDivar = await AdvertiseLogBussines.GetAdvCountInSpecialMounthAsync(dayCount, AdvertiseType.Divar);
                //تعداد آگهی های ارسال شده در شیپور در هفته اخیر
                var lstAllSheypoor = await AdvertiseLogBussines.GetAdvCountInSpecialMounthAsync(dayCount, AdvertiseType.Sheypoor);

                //تعداد کل آگهی های منتشر شده
                var lstAllPub = await AdvertiseLogBussines.GetPublishedAdvCountInSpecialMounthAsync(dayCount, AdvertiseType.All);
                //تعداد آگهی های منتشر شده در دیوار
                var lstDivarPublished = await
                    AdvertiseLogBussines.GetPublishedAdvCountInSpecialMounthAsync(dayCount, AdvertiseType.Divar);
                //تعداد آگهی های منتظر شده در شیپور
                var lstSheypoorPublished = await
                    AdvertiseLogBussines.GetPublishedAdvCountInSpecialMounthAsync(dayCount, AdvertiseType.Sheypoor);

                var firstDate = DateTime.Now.AddDays(-dayCount);
                var secondDate = DateTime.Now;
                //پرکردن لیست تاریخ از امروز تا 7 روز پیش به شمسی
                for (var i = firstDate; i <= secondDate; i = i.AddDays(1))
                {
                    var stri = DateConvertor.M2SH(i);
                    lstDate.Add(stri.Substring(5, 5));
                }
                //بدست آوردن درصد آگهی های منتشر شده به کل آگهی های یک روز
                for (var i = 0; i < lstAll.Count; i++)
                {
                    var sub = ((float)lstAllPub[i] / (float)lstAll[i]);
                    var per = 0;
                    if (sub > 0)
                        per = (int)(sub * 100);
                    lstDate[i] = lstDate[i] + "  %" + per;
                }


                chart1.Palette = ChartColorPalette.Grayscale;
                chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
                chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;
                chart1.Titles.Clear();
                chart1.Series.Clear();
                var divarserieAll = new Series { ChartType = SeriesChartType.Column, Name = "تعداد کل آگهی های دیوار" };
                divarserieAll.ChartType = SeriesChartType.Column;
                for (var i = 0; i < lstAllDivar.Count; i++)
                {
                    divarserieAll.Points.AddXY(lstDate[i], lstAllDivar[i]);
                    divarserieAll.IsValueShownAsLabel = true;
                }

                var divarseriePublished = new Series { ChartType = SeriesChartType.Column, Name = "تعداد آگهی های منتشر شده در دیوار" };
                divarseriePublished.ChartType = SeriesChartType.RangeColumn;
                for (var i = 0; i < lstAllDivar.Count; i++)
                {
                    divarseriePublished.Points.AddXY(lstDate[i], lstDivarPublished[i]);
                    divarseriePublished.IsValueShownAsLabel = true;
                }


                var sheyserieAll = new Series { ChartType = SeriesChartType.Column, Name = "تعداد کل آگهی های شیپور" };
                sheyserieAll.ChartType = SeriesChartType.Column;
                for (var i = 0; i < lstAllSheypoor.Count; i++)
                {
                    sheyserieAll.Points.AddXY(lstDate[i], lstAllSheypoor[i]);
                    sheyserieAll.IsValueShownAsLabel = true;
                }

                var sheyseriePublished = new Series { ChartType = SeriesChartType.Column, Name = "تعداد آگهی های منتشر شده در شیپور" };
                sheyseriePublished.ChartType = SeriesChartType.RangeColumn;
                for (var i = 0; i < lstAllSheypoor.Count; i++)
                {
                    sheyseriePublished.Points.AddXY(lstDate[i], lstSheypoorPublished[i]);
                    sheyseriePublished.IsValueShownAsLabel = true;
                }



                chart1.Series.Add(divarserieAll);
                chart1.Series.Add(divarseriePublished);
                chart1.Series.Add(sheyserieAll);
                chart1.Series.Add(sheyseriePublished);
                chart1.ChartAreas[0].BackColor = Color.Transparent;
                chart1.Visible = true;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private async void frmChart_Load(object sender, EventArgs e)
        {
            await FillChart();
        }
    }
}
