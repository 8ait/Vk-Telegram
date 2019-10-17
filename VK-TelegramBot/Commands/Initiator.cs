using System;
using System.Collections.Generic;

namespace VK_TelegramBot.Commands
{
    public class Initiator
    {
        private Dictionary<int, string> _commands = new Dictionary<int, string>()
        {
            {0, "dialog" },
            {1, "history" }
        };

        public Initiator()
        {

        }

        public string Build(string[] arguments, Account account)
        {
            string answer = "";

            Command command = null;

            if (arguments[0] == _commands[0])
            {
                command = new Dialogs(arguments, account);
                answer = command.Execute();
            } else if (arguments[0] == _commands[1])
            {
                command = new History(arguments, account);
                answer = command.Execute();
            }

            if (command == null)
            {
                answer = "Может ты пидр?";
            }

            return answer;
        }
    }
}
