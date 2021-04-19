using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ConsoleApp11_TelegramBot
{
    public class BotWorker
    {
        private ITelegramBotClient botClient;
        private BotMessageLogic logic;

        public void Inizalize()
        {
            botClient = new TelegramBotClient(BotCredentials.BotToken);

            logic = new BotMessageLogic(botClient);

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine("Привет! Меня зовут {0}.", me.FirstName);
        }

        public void Start()
        {
            botClient.OnMessage += Bot_OnMessage;
         
            botClient.StartReceiving();
        }

        public void Stop()
        {
            botClient.StopReceiving();
        }

        private async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                //Console.WriteLine($"Получено сообщение в чате: {e.Message.Chat.Id}.");

                //await botClient.SendTextMessageAsync(
                //chatId: e.Message.Chat, text: "Вы написали:\n" + e.Message.Text);
                await logic.Response(e);
            }
        }
    }
}
