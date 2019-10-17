using System;
using System.Collections.Generic;

namespace VK_TelegramBot.Commands
{
    public class History : Command
    {
        private List<string> _catch;
        private const int _countOfArguments = 3;
        private int _id = 0;
        private int _count = 0;
        private const int maxCount = 200;
        private const int minCount = 1;

        public History(string[] arguments, Account account) : base (arguments, account)
        {
            _catch = new List<string>();
            _errors = new Dictionary<int, string>()
            {
                {0, "Количество последних сообщений должно быть от 1 до 200" },
                {1, "Неверная запись id" },
                {2, "Невреная запись количества" },
                {3, "Неверная структура команды" }
            };
            _info = "history id count";
        }

        public override string Execute()
        {
            string answer = "";
            Validate();
            if (_catch.Count == 0)
            {
                answer = _account.GetMessages(_id, _count);
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
                _catch.Add(_errors[3]);
            }
            if (!Int32.TryParse(_args[1], out _id))
            {
                _catch.Add(_errors[1]);
            }
            if (!Int32.TryParse(_args[2], out _count))
            {
                _catch.Add(_errors[2]);
            }
            if (!(_count >= minCount && _count <= maxCount))
            {
                _catch.Add(_errors[0]);
            }
        }
    }
}
