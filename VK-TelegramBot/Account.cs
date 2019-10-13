using System;
using System.Collections;
using Microsoft.Extensions.DependencyInjection;
using VkNet;
using VkNet.AudioBypassService.Extensions;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VK_TelegramBot
{
    public class Account : IAccount
    {
        private VkApi _api;
        private string _login;
        private string _password;

        public Account(string login, string password)
        {
            _login = login;
            _password = password;
        }

        public void Login()
        {
            var service = new ServiceCollection();
            service.AddAudioBypass();

            _api = new VkApi(service);

            _api.Authorize(new ApiAuthParams
            {
                ApplicationId = 7168291,
                Login = "login",
                Password = "password",
                Settings = Settings.All
            });

            _api.Account.SetOffline();
        }
    }
}
