using System;
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
            var rmu = new ReplyKeyboardMarkup();
            rmu.Keyboard =
                new KeyboardButton[][]
                {
                    new KeyboardButton[]
                    {
                        new KeyboardButton("login")
                    },
                    new KeyboardButton[]
                    {
                        new KeyboardButton("info")
                    }
                };
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

                await telegramBot.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: "You said:\n" + e.Message.Text,
                  replyMarkup: rmu                 
                );
            }
        }
    }
}
