using System;
namespace VK_TelegramBot
{
    interface IAccount
    {
        void Login(string login, string password);
    }
}
