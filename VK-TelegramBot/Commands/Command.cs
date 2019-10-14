using System;
using System.Collections.Generic;

namespace VK_TelegramBot.Commands
{
    public abstract class Command
    {
        protected Dictionary<int, string> _errors;
        protected string _info;
        protected string[] _args;
        protected Account _account;

        public abstract string Execute();

        public Command(string[] argumetns, Account account)
        {
            _args = argumetns;
            _account = account;
        }
    }
}
