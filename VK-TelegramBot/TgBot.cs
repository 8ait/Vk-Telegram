using System;
using System.IO;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace VK_TelegramBot
{
    public static class TgBot
    {
        private static ITelegramBotClient telegramBot = new TelegramBotClient("988600637:AAF-dp2osWsa0fmUpcIn2TGTfNRBvjge5gk");

        public static void Messages()
        {
            telegramBot.OnMessage += Bot_OnMessage;
            telegramBot.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine($"Message from {e.Message.Chat.Id} : {e.Message.Text}");
            var answer = "";
            if (e.Message.Text != null)
            {
                answer = Commander.GetAnswer(e.Message.Text, e.Message.Chat.Id);
                await telegramBot.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: answer
                    );
            } else
            {
                if (e.Message.Sticker != null)
                {
                    answer = ".!.";
                    await telegramBot.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: answer
                    );
                }
                else
                {
                    await telegramBot.SendPhotoAsync(
                        chatId: e.Message.Chat,
                        photo: "http://portal.tpu.ru/foto/21377.jpg"
                    );
                }
            }         
        }

    }
}
