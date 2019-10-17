using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using VkNet;
using VkNet.AudioBypassService.Extensions;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VK_TelegramBot
{
    public class Account : IAccount
    {
        private VkApi _api;
        private string _login;
        private string _password;
        private List<long> _dialogIds;

        public Account()
        {
            _dialogIds = new List<long>();
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

        public string GetMessages(int id, int count)
        {
            string answer = "";
            if (_dialogIds.Count != 0)
            {
                string line = "-----------------------------------\n";

                var mes = _api.Messages.GetHistory(new MessagesGetHistoryParams
                {
                    UserId = _dialogIds[id],
                    Count = count
                });

                foreach (var item in mes.Messages)
                {
                    var us = _api.Users.Get(new long[] { (long)item.FromId }, ProfileFields.All);
                    answer = us[0].FirstName + " : " + item.Text + "\n" + answer;
                    answer = line + answer;
                }

                answer += line;
            } else
            {
                answer = "Выведи диалоги";
            }
            return answer;
        }

        public string GetConversations(int count)
        {
            _dialogIds.Clear();

            var con = _api.Messages.GetConversations(new GetConversationsParams
            {
                Count = (ulong?)count
            });

            string answer = "";
            string line = "-----------------------------------\n";
            int index = 0;
            foreach (var item in con.Items)
            {
                answer += line;
                if (item.Conversation.Peer.Type == ConversationPeerType.User)
                { 
                    var us = _api.Users.Get(new long[] { (long)item.Conversation.Peer.Id }, ProfileFields.LastName);
                    if (item.LastMessage.FromId == _api.UserId)
                    {
                        answer += "[" + index + "] " + us[0].LastName + " : " + " (Вы) " + item.LastMessage.Text + "\n";
                    } else
                    {
                        answer += "[" + index + "] " + us[0].LastName + " : " + item.LastMessage.Text + "\n";
                    }
                    
                } else if (item.Conversation.Peer.Type == ConversationPeerType.Chat)
                {

                    long id = item.Conversation.Peer.Id - 2000000000;
                    var ch = _api.Messages.GetChat(id);
                    if (item.LastMessage.FromId == _api.UserId)
                    {
                        answer += "[" + index + "] " + ch.Title + " : " + " (Вы) "+ item.LastMessage.Text + "\n";
                    }
                    else
                    {
                        var us = _api.Users.Get(new long[] { (long)item.LastMessage.FromId }, ProfileFields.LastName);
                        answer += "[" + index + "] " + ch.Title + " : " + "("+ us[0].LastName +") " + item.LastMessage.Text + "\n";
                    }
                }
                _dialogIds.Add(item.Conversation.Peer.Id);
                index++;
            }
            answer += line;
            return answer;
        }
    }
}
