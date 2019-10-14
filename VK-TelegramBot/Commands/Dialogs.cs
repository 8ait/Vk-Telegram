using System;
using System.Collections.Generic;

namespace VK_TelegramBot.Commands
{
    public class Dialogs : Command
    {
        private List<string> _catch;
        private const int _countOfArguments = 2;
        private int _count = 0;
        private const int maxCount = 200;
        private const int minCount = 1;

        public Dialogs(string[] arguments, Account account) : base (arguments, account)
        {
            _catch = new List<string>();
            _errors = new Dictionary<int, string>()
            {
                {0, "Количество последних диалогов может быть от 1 до 200" },
                {1, "Аргумент count должен быть целым числом" },
                {2, "Неверное количестов аргументов команды dialogs" }
            };
            _info = "dialogs count";
        }

        public override string Execute()
        {
            string answer = "";
            Validate();
            if (_catch.Count == 0)
            {
                answer = _account.GetConversations(_count);
            }
            else
            {
                foreach (var item in _catch)
                {
                    answer += item + "\n";
                }
            }

            return answer;
        }

        private void Validate()
        {
            if (_args.Length != _countOfArguments)
            {
                _catch.Add(_errors[2]);
            }
            if (!Int32.TryParse(_args[1], out _count))
            {
                _catch.Add(_errors[1]);
            }
            if (!(_count >= minCount && _count <= maxCount))
            {
                _catch.Add(_errors[0]);
            }
        }
        
    }
}
