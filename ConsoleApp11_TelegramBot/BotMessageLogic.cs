using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ConsoleApp11_TelegramBot
{
    public class BotMessageLogic
    {//управлять логикой обработки полученных сообщений 
        private Messenger messanger;

        private Dictionary<long, Conversation> chatList;
        private ITelegramBotClient botClient;
                
        public BotMessageLogic(ITelegramBotClient botClient)
        {
            messanger = new Messenger();
            chatList = new Dictionary<long, Conversation>();
            this.botClient = botClient;
        }
        //функционал хранения чатов и отправки ответа в чат в зависимости от того, какой это чат        
        public async Task Response(MessageEventArgs e)
        {
                var Id = e.Message.Chat.Id;

                if (!chatList.ContainsKey(Id))
                {
                    var newchat = new Conversation(e.Message.Chat);

                    chatList.Add(Id, newchat);
                }

            //добавляем новое сообщение в историю сообщений конкретного чата
            var chat = chatList[Id];
            chat.AddMessage(e.Message);

            //отправляем ответ пользователю
            await SendTextMessage(chat);

        }
        public async Task SendTextMessage(Conversation chat)
        {
            var text = messanger.CreateTextMessage(chat);

            await botClient.SendTextMessageAsync(
            chatId: chat.GetId(), text: text);
        }
    }
}
