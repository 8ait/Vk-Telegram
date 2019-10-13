using System;

namespace VK_TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account();
            while (Account._auth)
            {
                string login;
                string password;
                Console.Write("login : ");
                login = Console.ReadLine();
                Console.Write("password : ");
                ConsoleColor origBG = Console.BackgroundColor; // Store original values
                ConsoleColor origFG = Console.ForegroundColor;
                Console.BackgroundColor = ConsoleColor.Red; // Set the block colour (could be anything)
                Console.ForegroundColor = ConsoleColor.Red;
                password = Console.ReadLine();
                Console.BackgroundColor = origBG; // revert back to original
                Console.ForegroundColor = origFG;
                account.Login(login, password);
            }
            account.GetMessages(118714796);
        }
    }
}
