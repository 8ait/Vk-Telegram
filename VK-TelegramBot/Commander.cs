using System;
using System.Collections.Generic;
using VkNet;

namespace VK_TelegramBot
{
    public static class Commander
    {
        private static Dictionary<long, Account> accounts = new Dictionary<long, Account>();

        public static string GetAnswer(string command, long chatid)
        {
            string answer = "";
            if (!accounts.ContainsKey(chatid) && command.Split(' ')[0] != "login")
            {
                answer = "Неавтоизированы в системе \nВведите логин и пароль через пробел";
            } else if (command.Split(' ')[0] == "login")
            {
                Login(chatid, command.Split(' ')[1], command.Split(' ')[2]);
                answer = accounts[chatid].GetConversations(20);
            }          
            return answer;
        }

        private static void Login(long chatid, string login, string password)
        {
            Account account = new Account();
            account.Login(login, password);
            accounts.Add(chatid, account);
        }
    }
}
