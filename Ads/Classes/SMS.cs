using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BussinesLayer;
using DataLayer.Enums;
using ErrorHandler;

namespace Ads.Classes
{
    public class SMS
    {
        public static async Task SendSMSList(List<string> recivers, string api, string action, string username, string password, int type,
            long from, string text)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(api);
                    foreach (var reciver in recivers)
                    {
                        var content = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("Action", action),
                            new KeyValuePair<string, string>("username", username),
                            new KeyValuePair<string, string>("password", password),
                            new KeyValuePair<string, string>("type", type.ToString()),
                            new KeyValuePair<string, string>("from", from.ToString()),
                            new KeyValuePair<string, string>("text", text),
                            new KeyValuePair<string, string>("receivers", reciver),
                        });
                        var result = await client.PostAsync(client.BaseAddress, content);
                        var resultContent = await result.Content.ReadAsStringAsync();
                        var num = ChatNumberBussines.Get(reciver);
                        if (num != null)
                        {
                            num.isSendSms = true;
                            await num.SaveAsync();
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        public static async Task SendSMS(string reciver, string text)
        {
            try
            {
                var a = SettingBussines.GetAll();
                var cls = a.Count > 0 ? a[0] : new SettingBussines();
                if (cls.PanelGuid == null || cls.PanelGuid == Guid.Empty) return;
                if (cls.LineNumberGuid == null || cls.LineNumberGuid == Guid.Empty) return;
                var panel = await PanelBussines.GetAsync(cls.PanelGuid ?? Guid.Empty);
                var line = await PanelLineNumberBussines.GetAsync(cls.LineNumberGuid ?? Guid.Empty);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(panel.API);

                    var content = new FormUrlEncodedContent(new[]
                    {
                            new KeyValuePair<string, string>("Action", SMSAction.Send.Value),
                            new KeyValuePair<string, string>("username", panel.UserName),
                            new KeyValuePair<string, string>("password", panel.Password),
                            new KeyValuePair<string, string>("type", ((int)SMSType.SMS).ToString()),
                            new KeyValuePair<string, string>("from", line.LineNumber.ToString()),
                            new KeyValuePair<string, string>("text", text),
                            new KeyValuePair<string, string>("receivers", reciver),
                        });
                    var result = await client.PostAsync(client.BaseAddress, content);
                    var resultContent = await result.Content.ReadAsStringAsync();

                }
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
    }
}
