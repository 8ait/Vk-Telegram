using System;
using Telegram.Bot;

namespace VK_TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account();
            account.Login("89234333242", "Tishka2015");
            account.GetMessages(118714796);
            Console.WriteLine();
            account.GetConversations(20);
            TgBot.Messages();
        }
    }
}
