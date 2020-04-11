using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BussinesLayer;
using DataLayer.Enums;
using ErrorHandler;
using MihaZupan;
using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace Ads.Classes
{
    public class TelegramBot
    {
        private TelegramBotClient bot;
        private static TelegramBot _me;
        public static event Action SubmitEvent;
        private static void RaiseEvent()
        {
            var handler = SubmitEvent;
            if (handler != null)
            {
                SubmitEvent();
            }
        }
        public static async Task<TelegramBot> GetInstance()
        {
            return _me ?? (_me = new TelegramBot());
        }
        public async Task StartSending(TelegramSendType type, string token = "", string chatId = "", string fileName = "", string caption = "", string smsReciver = "", string smsText = "")
        {
            var allPrx = await ProxyBussines.GetAllAsync();
            foreach (var item in allPrx)
            {
                try
                {
                    var socks5 = new HttpToSocks5Proxy(item.Server, item.Port, item.UserName, item.Password)
                    { ResolveHostnamesLocally = false };
                    var mtpropto = new WebProxy(Address: item.Server)
                    {
                        Credentials = new NetworkCredential(item.UserName, item.Password)
                    };
                    var a = TelegramBotSettingBussines.GetAll();
                    var cls = a.Count > 0 ? a[0] : new TelegramBotSettingBussines();
                    if (string.IsNullOrEmpty(cls.Token) || string.IsNullOrEmpty(cls.ChanelForAds)) return;
                    token = cls.Token;
                    bot = item.Type == ProxyType.Socks5
                        ? new TelegramBotClient(token, socks5)
                        : new TelegramBotClient(token, mtpropto);
                    if (type == TelegramSendType.SendBackUp)
                        await Send_(bot, fileName, cls.ChanelForAds, 10);
                    else
                        await Send_(bot, chatId: chatId, passage: caption, picPath: fileName, tryCount: 10,
                            smsReciver: smsReciver, smsText: smsText);

                    break;
                }
                catch
                {
                    continue;
                }
            }
        }
        private async Task Send_(TelegramBotClient bot, string fileName, string chatId, short tryCount)
        {
            try
            {
                using (var sendFileStream = File.Open(fileName, FileMode.Open))
                {
                    await bot.SendDocumentAsync(chatId,
                        new Telegram.Bot.Types.InputFiles.InputOnlineFile(sendFileStream, fileName));
                    RaiseEvent();
                }
            }
            catch (BadRequestException ex)
            {
                if (tryCount > 0)
                {
                    await Task.Delay(1000);
                    await Send_(bot, fileName, chatId, --tryCount);
                }

                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
            catch (ApiRequestException ex)
            {
                if (tryCount > 0)
                {
                    await Task.Delay(1000);
                    await Send_(bot, fileName, chatId, --tryCount);
                }

                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
            catch (HttpRequestException ex)
            {
                if (tryCount > 0)
                {
                    await Task.Delay(1000);
                    await Send_(bot, fileName, chatId, --tryCount);
                }
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        private async Task Send_(TelegramBotClient bot, string chatId, string passage, string picPath, short tryCount, string smsReciver, string smsText)
        {
            try
            {
                var picFile = new FileStream(picPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                await bot.SendPhotoAsync(chatId, picFile, passage);
                await SMS.SendSMS(smsReciver, smsText);
            }
            catch (BadRequestException ex)
            {
                if (tryCount > 0)
                {
                    await Task.Delay(1000);
                    await Send_(bot, chatId, passage, picPath, --tryCount, smsReciver, smsText);
                }

                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
            catch (ApiRequestException ex)
            {
                if (tryCount > 0)
                {
                    await Task.Delay(1000);
                    await Send_(bot, chatId, passage, picPath, --tryCount, smsReciver, smsText);
                }

                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
            catch (HttpRequestException ex)
            {
                if (tryCount > 0)
                {
                    await Task.Delay(1000);
                    await Send_(bot, chatId, passage, picPath, --tryCount, smsReciver, smsText);
                }
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
    }
}
