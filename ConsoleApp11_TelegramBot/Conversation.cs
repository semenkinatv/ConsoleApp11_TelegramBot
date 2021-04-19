using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace ConsoleApp11_TelegramBot
{/// <summary>
 /// объект чата
 /// </summary>
    public class Conversation
    {
        private Chat telegramChat;

        private List<Message> telegramMessages;

        public Conversation(Chat chat)
        {
            telegramChat = chat;
            telegramMessages = new List<Message>();
        }

        public void AddMessage(Message message)
        {
            telegramMessages.Add (message);

        }

        public long GetId()
        {
           return telegramChat.Id;
        }

        public List<string> GetTextMessages()
        {
            var textMessages = new List<string>();

            foreach (var message in telegramMessages)
            {
                if (message.Text != null)
                {
                    textMessages.Add(message.Text);
                }
            }

            return textMessages;
        }
        //возвращает последний элемент коллекции telegramMessages
        public string GetLastMessage()
        { return telegramMessages[telegramMessages.Count - 1].Text; }
                
        
    }
}
