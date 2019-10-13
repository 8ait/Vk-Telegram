using System;

namespace VK_TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account("89234333242", "Tishka2015");
            account.Login();
        }
    }
}
