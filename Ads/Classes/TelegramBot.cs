using System;
using System.IO;
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
        public async Task StartSending(TelegramSendType type, string token = "", string chatId = "", string fileName = "")
        {
            var proxy = new HttpToSocks5Proxy("so2.10g2.cf", 8085, "p7", "341") { ResolveHostnamesLocally = false };
            if (type == TelegramSendType.SendBackUp)
            {
                var a = TelegramBotSettingBussines.GetAll();
                var cls = a.Count > 0 ? a[0] : new TelegramBotSettingBussines();
                if (string.IsNullOrEmpty(cls.Token) || string.IsNullOrEmpty(cls.ChanelForAds)) return;
                token = cls.Token;
                bot = new TelegramBotClient(token, proxy);
                var ts = new Thread(new ThreadStart(async () => await Send_(bot, fileName, cls.ChanelForAds)));
                ts.Start();
            }
        }
        private async Task Send_(TelegramBotClient bot, string fileName, string chatId)
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
            catch (BadRequestException)
            {
            }
            catch (ApiRequestException)
            {
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
    }
}
