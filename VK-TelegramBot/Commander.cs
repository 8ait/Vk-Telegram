using System;
using System.Collections.Generic;
using VK_TelegramBot.Commands;
using VkNet;

namespace VK_TelegramBot
{
    public static class Commander
    {
        private static Dictionary<long, Account> accounts = new Dictionary<long, Account>();

        public static string GetAnswer(string command, long chatid)
        {
            if (accounts.ContainsKey(chatid) && command.Split(' ')[0] != "login")
            {
                string[] arguments = command.Split(" ");
                Initiator initiator = new Initiator();
                return initiator.Build(arguments, accounts[chatid]);
            }
            else if (command.Split(' ')[0] == "login")
            {
                string answer = "";
                try
                {
                    Login(chatid, command.Split(' ')[1], command.Split(' ')[2]);
                    answer = "Залогинился \nИспользуй команду dialog count, где count от 1 до 200";
                } catch (Exception e)
                {
                    answer = "Хуй знает даун не может залогинится";
                }
                return answer;
            }
            else
            {
                return "Залогинься петух";
            }            
        }

        private static void Login(long chatid, string login, string password)
        {
            Account account = new Account();
            account.Login(login, password);
            accounts.Add(chatid, account);
        }
    }
}
