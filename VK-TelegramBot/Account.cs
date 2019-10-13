using System;
using System.Collections;
using System.Collections.Generic;
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
        public static bool _auth = true;

        public Account()
        {
        }

        public void Login(string login, string password)
        {
            _login = login;
            _password = password;
            try
            {
                var service = new ServiceCollection();
                service.AddAudioBypass();

                _api = new VkApi(service);

                _api.Authorize(new ApiAuthParams
                {
                    ApplicationId = 7168291,
                    Login = _login,
                    Password = _password,
                    Settings = Settings.All                   
                });

                _api.Account.SetOffline();
                _auth = false;
            } catch (VkNet.Exception.CaptchaNeededException cne)
            {
                var csid = cne.Sid;
                string captchaUrl = cne.Img.AbsoluteUri;
                Console.WriteLine(captchaUrl);
                Console.Write("Введите капчу: ");
                var captchKey = Console.ReadLine();
                _api.Authorize(new ApiAuthParams
                {
                    ApplicationId = 7168291,
                    Login = _login,
                    Password = _password,
                    Settings = Settings.All,
                    CaptchaKey = captchKey,
                    CaptchaSid = csid
                });
            }
        }

        public void GetMessages(long id)
        {
            var mes = _api.Messages.GetHistory(new MessagesGetHistoryParams
            {
                UserId = id,
                Count = 10
            });

            foreach (var item in mes.Messages)
            {
                var us = _api.Users.Get(new long[] { (long)item.FromId }, ProfileFields.All);
                Console.WriteLine(us[0].FirstName + " : " + item.Text);
            }
        }
    }
}
